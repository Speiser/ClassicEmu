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
                    print(f'!! [{self.address}] - Connection Closed')
                    break
                # print(packet)
                self._handle_packet(packet)
            except ConnectionError:
                print(f'!! [{self.address}] - Lost Connection')
                break
            except socket.timeout:
                pass

    def _handle_packet(self, packet):
        if self.state == ClientState.Init:
            self.state = ClientState.ClientLogonChallenge
            print(f'-> [{self.address}] - Client Logon Challenge')
            ClientLogonChallenge(packet, self.connection)
            self.state = ClientState.ServerLogonChallenge
            print(f'<- [{self.address}] - Server Logon Challenge')

        elif self.state == ClientState.ServerLogonChallenge:
            self.state = ClientState.ClientLogonProof
            print(f'-> [{self.address}] - Client Logon Proof')
            ClientLogonProof(packet, self.connection)
            print(f'<- [{self.address}] - Server Logon Proof')
            self.state = ClientState.Authenticated
            print(f'!! [{self.address}] - Client Authenticated')

        elif self.state == ClientState.Authenticated:
            pass
