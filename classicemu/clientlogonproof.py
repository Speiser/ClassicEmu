from serverlogonproof import ServerLogonProof

class ClientLogonProof:
    cmd = None
    A = None
    M1 = None
    crc_hash = None
    number_of_keys = None
    unk = None

    def __init__(self, packet, connection):
        self.packet = packet
        self.connection = connection
        self._parse()
        self._work()

    def _parse(self):
        # Dont care about the rest.
        # Just pretending to have the same password on the server.
        self.M1 = bytes(self.packet[33:53])

    def _work(self):
        if self.M1 is None:
            self._parse()

        self.connection.sendall(bytearray(ServerLogonProof(self.M1).get()))
    