import socket
from config import CONFIG

class LoginServer:
    def __init__(self):
        login_addr = CONFIG['LoginServer']['Address']
        login_port = CONFIG['LoginServer']['Port']
        realm_addr = CONFIG['RealmServer']['Address']
        realm_port = CONFIG['RealmServer']['Port']
        self.loginserver_ep = (login_addr, login_port)
        self.realm_ep = (realm_addr, realm_port)
        self.loginserver_backlog = CONFIG['RealmServer']['Backlog']
        self.realm_backlog = CONFIG['RealmServer']['Backlog']

    def start(self):
        self._client_listen()
        self._realm_listen()
        self.active = True
        pass

    def close(self):
        self.active = False
        pass

    def _client_listen(self):
        self.client_socket = socket.socket()
        self.client_socket.settimeout(1)
        self.client_socket.bind(self.loginserver_ep)
        self.client_socket.listen(self.loginserver_backlog)

    def _realm_listen(self):
        self.realm_socket = socket.socket()
        self.realm_socket.settimeout(1)
        self.realm_socket.bind(self.realm_ep)
        self.realm_socket.listen(self.realm_backlog)

    def _accept_clients(self):
        while self.active:
            pass

    def _client_work(self):
        pass