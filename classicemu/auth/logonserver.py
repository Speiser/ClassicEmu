import socket

from classicemu.serverbase import ServerBase
from classicemu.auth.clientlogin import ClientLogin
from classicemu.common.helper import run_thread


class LogonServer(ServerBase):

    def __init__(self):
        super().__init__("LogonServer")

    def process_client(self, connection, address):
        """ Initializes a ClientLogin instance and
        handles the connection in a new thread.
        """
        login = ClientLogin(connection, f'{address[0]}:{address[1]}')
        run_thread(login.handle_connection)
