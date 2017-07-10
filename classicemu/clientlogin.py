import socket

class ClientLogin:
    def __init__(self, connection, address):
        self.connection = connection
        self.address = address

    def handle_connection(self):
        while True:
            try:
                packet = self.connection.recv(1024)
                if not packet:
                    print('Connection closed: ' + self.address)
                    break
                print(packet)
            except ConnectionError:
                print('Lost connection: ' + self.address)
                break
            except socket.timeout:
                pass