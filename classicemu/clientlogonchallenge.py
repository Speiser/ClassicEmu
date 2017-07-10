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
        print('ClientLogonChallenge Received.')
        self.packet = packet
        self.connection = connection
        self._parse()
        self._work()

    def __repr__(self):
        p = self.packet
        result = ''
        result += 'cmd: ' + str(p[0]) + '\n'
        result += 'error: ' + str(p[1]) + '\n'
        result += 'size: ' + str(p[2:4]) + '\n'
        result += 'gamename: ' + str(p[4:8]) + '\n'
        result += 'version1: ' + str(p[8]) + '\n'
        result += 'version2: ' + str(p[9]) + '\n'
        result += 'version3: ' + str(p[10]) + '\n'
        result += 'build: ' + str(p[11:13]) + '\n'
        result += 'platform: ' + str(p[13:17]) + '\n'
        result += 'os: ' + str(p[17:21]) + '\n'
        result += 'country: ' + str(p[21:25]) + '\n'
        result += 'timezone_bias: ' + str(p[25:29]) + '\n'
        result += 'ip: ' + str(p[29:33]) + '\n'
        result += 'I_len: ' + str(p[33]) + '\n'
        result += 'I: ' + str(p[34:(34 + int(p[33]))]) + '\n'
        return result

    def __str__(self):
        return self.__repr__()

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

    def _work(self):
        data = bytearray([
            0, 0, 0, 175, 237, 70, 1, 54, 143, 6, 11, 41, 212, 191, 57, 14, 185, 18, 157, 234, 71, 59, 136, 26, 121, 72, 171, 34, 72, 107, 149, 215, 31, 173, 57, 1, 7, 32, 183, 155, 62, 42, 135, 130, 60, 171, 143, 94, 191, 191, 142, 177, 1, 8, 83, 80, 6, 41, 139, 91, 173, 189, 91, 83, 225, 137, 94, 100, 75, 137, 13, 88, 110, 73, 181, 89, 160, 168, 240, 35, 114, 102, 252, 103, 166, 34, 45, 31, 88, 131, 181, 174, 14, 165, 65, 28, 184, 81, 248, 101, 124, 107, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        ])

        self.connection.sendall(data)
        print('ServerLogonChallenge Sent.')