import os
import socket

from classicemu.common.helper import print_packet
from classicemu.common.opcodes import OPCODE


class ClientHandler:
    def __init__(self, connection, address_str):
        print(f'!! [{address_str}] - Client Connected')
        self.connection = connection
        self.address_str = address_str

    def handle(self):
        self._send_authchallenge()
        while True:
            try:
                packet = self.connection.recv(1024)
                if packet is None:
                    print(f'!! [{self.address_str}] - Connection Closed')
                    break
                if not packet:
                    continue
                self._handle_packet(packet)
            except ConnectionError:
                print(f'!! [{self.address_str}] - Lost Connection')
                break
            except socket.timeout:
                pass

    def _handle_packet(self, packet):
        print(f'-> [{self.address_str}] - Packet Received')
        print_packet(packet)

    def _send_authchallenge(self):
        seed = os.urandom(4)
        enum = int.to_bytes(OPCODE.SMSG_AUTH_CHALLENGE.value, 2, byteorder='little')
        data = enum + seed
        packet = int.to_bytes(len(data), 2, byteorder='little') + data
        self.connection.sendall(packet)
