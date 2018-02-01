import socket

from classicemu.common.config import CONFIG


class ServerBase:
    def __init__(self, config_string):
        """ Initializes a new instance of the ServerBase class.
        This is the base implementation for every ClassicEmu server.
        For configuration change "config.py".
        """
        self.active = False

        self.addr = CONFIG[config_string]['Address']
        self.port = CONFIG[config_string]['Port']
        self.backlog = CONFIG[config_string]['Backlog']
        self.debug_string = (f'{self.addr}:{self.port}')

        self.ep = (self.addr, self.port)

    def start(self):
        """ Starts the server. """
        self._listen()
        self.active = True
        self._accept()

    def close(self):
        """ Shuts the server down. """
        self.active = False

    def _listen(self):
        """ Initializes and binds the socket.
        Starts listening.
        """
        self.socket = socket.socket()
        self.socket.settimeout(1)
        self.socket.bind(self.ep)
        self.socket.listen(self.backlog)
        print(f'!! [{self.debug_string}] - Socket Initialized')

    def _accept(self):
        """ Accepts incoming client connections and calls 
        self.process_client(conn, addr) which will be overwritten
        in child classes.
        """
        print(f'!! [{self.debug_string}] - Accepting Clients')
        while self.active:
            try:
                conn, addr = self.socket.accept()
                self.process_client(conn, addr)
            except socket.timeout:
                pass
        
    def process_client(self, connection, address):
        """ Overwrite me please. """
        pass