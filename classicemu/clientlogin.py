import socket

from clientstate import ClientState
from clientlogonchallenge import ClientLogonChallenge
from clientlogonproof import ClientLogonProof
from helper import print_packet
from srp6 import SRP6

class ClientLogin:
    def __init__(self, connection, address):
        self.connection = connection
        self.address = address
        self.state = ClientState.Init
        self.srp = None
        self.connected = True

    def handle_connection(self):
        while self.connected:
            try:
                packet = self.connection.recv(1024)
                if packet is None:
                    print(f'!! [{self.address}] - Connection Closed')
                    break
                if not packet:
                    continue
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
            print_packet(packet)
            clc = ClientLogonChallenge(packet, self.connection)
            self.srp = clc.srp
            self.state = ClientState.ServerLogonChallenge
            print(f'<- [{self.address}] - Server Logon Challenge')

        elif self.state == ClientState.ServerLogonChallenge:
            self.state = ClientState.ClientLogonProof
            print(f'-> [{self.address}] - Client Logon Proof')
            print_packet(packet)
            clp = ClientLogonProof(packet, self.connection, self.srp)
            print(f'<- [{self.address}] - Server Logon Proof')
            if clp.failed:
                self.connected = False
                self.state = ClientState.Disconnected
                print(f'<- [{self.address}] - Client Authentification Failed')
            else:
                self.state = ClientState.Authenticated
                print(f'!! [{self.address}] - Client Authenticated')

        elif self.state == ClientState.Authenticated:
            print(f'-> [{self.address}] - Packet Received')
            print_packet(packet)
            fake_realm = bytes([16, 7, 0, 0, 0, 0, 0, 0, 2, 0])
            self.connection.sendall(fake_realm)
