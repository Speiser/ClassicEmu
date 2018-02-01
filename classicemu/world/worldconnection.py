import socket

from classicemu.common.helper import print_packet


class WorldConnection:
    def __init__(self, connection, address):
        self.connection = connection
        self.address = address
        self.connected = True

    def handle_connection(self):
        while self.connected:
            try:
                packet = self.connection.recv(1024)
                if packet is None:
                    print("!! CLOSED")
                    break
                if not packet:
                    continue
                self._handle_packet(packet)
            except ConnectionError:
                print("!! CONNECTION ERROR")
                break
            except socket.timeout:
                pass

    def _handle_packet(self, packet):
        print_packet(packet)