class ClientLogonProof:
    def __init__(self, packet, connection):
        print('ClientLogonProof Received.')
        self.packet = packet
        self.connection = connection
        self._parse()
        self._work()

    def _parse(self):
        pass

    def _work(self):
        data = bytearray([
            1, 0, 89, 203, 168, 108, 49, 241, 164, 185, 113, 162, 224, 148, 77, 148, 245, 153, 143, 81, 106, 167, 0, 0, 0, 0
        ])
        print(data)

        self.connection.sendall(data)
        print('ServerLogonProof Sent.')
    