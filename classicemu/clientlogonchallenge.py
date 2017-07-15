from serverlogonchallenge import ServerLogonChallenge
from srp6 import SRP6

class ClientLogonChallenge:
    cmd = None
    error = None
    size = None
    gamename = None
    version1 = None
    version2 = None
    version3 = None
    build = None
    platform = None
    os = None
    country = None
    timezone_bias = None
    ip = None
    I_len = None
    I = None

    def __init__(self, packet, connection):
        self.packet = packet
        self.connection = connection
        self.srp = None
        self._parse()
        self._work()

    def _parse(self):
        p = self.packet
        self.cmd = bytes(p[0])
        self.error = bytes(p[1])
        self.size = bytes(p[2:4])
        self.gamename = bytes(p[4:8])
        self.version1 = bytes(p[8])
        self.version2 = bytes(p[9])
        self.version3 = bytes(p[10])
        self.build = bytes(p[11:13])
        self.platform = bytes(p[13:17])
        self.os = bytes(p[17:21])
        self.country = bytes(p[21:25])
        self.timezone_bias = bytes(p[25:29])
        self.ip = bytes(p[29:33])
        self.I_len = bytes(p[33])
        self.I = bytes(p[34:(34 + int(p[33]))])
        self.srp = SRP6(self.I, self.I)

    def _work(self):
        # Currently the password == username.
        self.connection.sendall(bytearray(ServerLogonChallenge(self.srp).get()))
