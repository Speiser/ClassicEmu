import socket

from clientstate import ClientState
from clientlogonchallenge import ClientLogonChallenge
from clientlogonproof import ClientLogonProof

class ClientLogin:
    def __init__(self, connection, address):
        self.connection = connection
        self.address = address
        self.state = ClientState.Init

    def handle_connection(self):
        while True:
            try:
                packet = self.connection.recv(1024)
                if not packet:
                    print('Connection closed: ' + self.address)
                    break
                print(packet)
                self._handle_packet(packet)
            except ConnectionError:
                print('Lost connection: ' + self.address)
                break
            except socket.timeout:
                pass

    def _handle_packet(self, packet):
        if self.state == ClientState.Init:
            self.state = ClientState.ClientLogonChallenge
            ClientLogonChallenge(packet, self.connection)
            self.state = ClientState.ServerLogonChallenge

        elif self.state == ClientState.ServerLogonChallenge:
            self.state = ClientState.ClientLogonProof
            ClientLogonProof(packet, self.connection)
            self.state = ClientState.Authenticated

        elif self.state == ClientState.Authenticated:
            pass
