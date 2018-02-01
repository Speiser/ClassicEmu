import socket

from classicemu.auth.clientstate import ClientState
from classicemu.auth.clientlogonchallenge import ClientLogonChallenge
from classicemu.auth.clientlogonproof import ClientLogonProof
from classicemu.common.helper import print_packet
from classicemu.crypto.srp6 import SRP6


class ClientLogin:
    def __init__(self, connection, address):
        """ Initalizes a new instance of the ClientLogin class.
        :param connection: The connection socket.
        :param address: The client address.
        """
        self.connection = connection
        self.address = address
        self.state = ClientState.Init
        self.srp = None
        self.connected = True

    def handle_connection(self):
        """ Reads clients packets and calls self._handle_packet. """
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
        """ Handles incoming packets based on the current ClientState.
        :param packet: Incoming packet.
        """
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
            self.send_realm_packet()

    def send_realm_packet(self):
        """ Creates the RealmInfo and sends it to the client. """
        # get all realms from config...
        # ...

        """ 2: RealmInfo_Server """
        type_b = int.to_bytes(0, 4, byteorder='little')
        flags = 0x00
        name = b'Test Server\0'
        addr_port = b'127.0.0.1:13250\0'
        population = b'\x00\x00\x00\x00'
        num_chars = 0x00
        time_zone = 0x00
        unknown = 0x00
        RealmInfo_Server = []
        for i in type_b:
            RealmInfo_Server.append(i)
        RealmInfo_Server.append(flags)
        for i in name:
            RealmInfo_Server.append(i)
        for i in addr_port:
            RealmInfo_Server.append(i)
        for i in population:
            RealmInfo_Server.append(i)
        RealmInfo_Server.append(num_chars)
        RealmInfo_Server.append(time_zone)
        RealmInfo_Server.append(unknown)

        """ 3: RealmFooter_Server """
        unk_ = int.to_bytes(0, 2, byteorder='little')
        RealmFooter_Server = [unk_[0], unk_[1]]

        """ 1: RealmHeader_Server """
        cmd = 0x10
        length = 7 + len(RealmInfo_Server)
        length_b = int.to_bytes(length, 2, byteorder='little')
        unk = int.to_bytes(0, 4, byteorder='little')
        num_realms = 0x01
        RealmHeader_Server = [cmd]
        for i in length_b:
            RealmHeader_Server.append(i)
        for i in unk:
            RealmHeader_Server.append(i)
        RealmHeader_Server.append(num_realms)

        self.connection.sendall(bytes(RealmHeader_Server))
        self.connection.sendall(bytes(RealmInfo_Server))
        self.connection.sendall(bytes(RealmFooter_Server))
