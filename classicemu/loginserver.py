import socket

from config import CONFIG
from helper import run_thread
from clientlogin import ClientLogin

class LoginServer:

    login_addr = CONFIG['LoginServer']['Address']
    login_port = CONFIG['LoginServer']['Port']
    realm_addr = CONFIG['RealmServer']['Address']
    realm_port = CONFIG['RealmServer']['Port']
    loginserver_backlog = CONFIG['RealmServer']['Backlog']
    realm_backlog = CONFIG['RealmServer']['Backlog']

    def __init__(self):
        self.active = False
        self.realms = {}

        self.loginserver_ep = (self.login_addr, self.login_port)
        self.realmserver_ep = (self.realm_addr, self.realm_port)

        self.loginserver_backlog = self.loginserver_backlog
        self.realmserver_backlog = self.realm_backlog

    def start(self):
        self._client_listen()
        # self._realm_listen()
        self.active = True
        self._accept_clients()

    def close(self):
        self.active = False
        pass

    def _client_listen(self):
        self.client_socket = socket.socket()
        self.client_socket.settimeout(1)
        self.client_socket.bind(self.loginserver_ep)
        self.client_socket.listen(self.loginserver_backlog)
        print('Client socket initialized.')

    def _realm_listen(self):
        self.realm_socket = socket.socket()
        self.realm_socket.settimeout(1)
        self.realm_socket.bind(self.realmserver_ep)
        self.realm_socket.listen(self.realmserver_backlog)
        print('Realm socket initialized.')

    def _accept_clients(self):
        print('Accepting clients.')
        while self.active:
            try:
                conn, addr = self.client_socket.accept()
                self._client_work(conn, addr)
            except socket.timeout:
                pass

    def _client_work(self, connection, address):
        address_string = f'{address[0]}:{address[1]}'
        print('Handling connection: ' + address_string)
        login = ClientLogin(connection, address_string)
        run_thread(login.handle_connection)
