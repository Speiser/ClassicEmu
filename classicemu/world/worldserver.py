import socket

from classicemu.common.helper import run_thread
from classicemu.world.clienthandler import ClientHandler


class WorldServer:
    def __init__(self, ip, port):
        self.worldserver_ep = (ip, port)
        self.worldserver_backlog = 8
        self.worldserver_string = (f'{ip}:{port}')

    def start(self):
        """ Starts the authentication server. """
        self._world_listen()
        self.active = True
        self._accept()

    def _world_listen(self):
        self.world_socket = socket.socket()
        self.world_socket.settimeout(1)
        self.world_socket.bind(self.worldserver_ep)
        self.world_socket.listen(self.worldserver_backlog)
        print(f'!! [{self.worldserver_string}] - World Socket Initialized')

    def _accept(self):
        """ Accepts incoming client connections. """
        print(f'!! [{self.worldserver_string}] - Accepting Clients')
        while self.active:
            try:
                conn, addr = self.world_socket.accept()
                self._handle_client(conn, addr)
            except socket.timeout:
                pass

    def _handle_client(self, connection, address):
        """ Initializes a ClientLogin instance and
        handles the connection in a new thread.
        """
        handle = ClientHandler(connection, f'{address[0]}:{address[1]}')
        run_thread(handle.handle)