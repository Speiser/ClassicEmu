from serverlogonproof import ServerLogonProof
from srp6 import SRP6


class ClientLogonProof:
    cmd = None
    A = None
    M1 = None
    crc_hash = None
    number_of_keys = None
    unk = None

    def __init__(self, packet, connection, srp):
        self.packet = packet
        self.connection = connection
        self.srp = srp
        self._parse()
        self._work()

    def _parse(self):
        # Rest can be ignored.
        self.A = bytes(self.packet[1:33])
        self.M1 = bytes(self.packet[33:53])

    def _work(self):
        self.srp.A = self.A
        data = bytearray(ServerLogonProof(self.srp).get(self.M1))
        self.connection.sendall(data)
        self.failed = (len(data) == 3)
