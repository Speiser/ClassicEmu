import math

from sha1 import hash_sha1

class ServerLogonChallenge:
    cmd = 0
    error = 0x00
    unk2 = 0
    B = None
    g_len = 1
    g = 7
    N_len = 32
    N = 0x894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7
    s = bytearray([0xF4, 0x3C, 0xAA, 0x7B, 0x24, 0x39, 0x81, 0x44, 0xBF, 0xA5, 0xB5, 0x0C, 0x0E, 0x07, 0x8C, 0x41,
         0x03, 0x04, 0x5B, 0x6E, 0x57, 0x5F, 0x37, 0x87, 0x31, 0x9F, 0xC4, 0xF8, 0x0D, 0x35, 0x94, 0x29])
    unk3 = [0x2A, 0xD5, 0x48, 0xCC, 0x9B, 0x9D, 0xA1, 0x99, 0xCC, 0x04, 0x7A, 0x60, 0x91, 0x15, 0x6C, 0x51]
    unk4 = 0

    def __init__(self, username, password):
        x = self._x(username, password)
        ex = int.from_bytes(x, byteorder='little')
        v = pow(self.g, ex, self.N)
        b = [
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        ]
        ib = int.from_bytes(b, byteorder='little')
        B = 3 * v + (pow(self.g, ib, self.N))
        self.B = B.to_bytes(32, byteorder='little')

    def _x(self, I, P):
        temp = hash_sha1(I.upper() + b":" + P.upper())
        return hash_sha1(self.s + temp)

    def get(self):
        data = []
        data.append(self.cmd)
        data.append(self.error)
        data.append(self.unk2)
        for b in self.B:
            data.append(b)
        data.append(self.g_len)
        data.append(self.g)
        data.append(self.N_len)
        for b in self.N.to_bytes(32, byteorder='little'):
            data.append(b)
        for b in self.s:
            data.append(b)
        for b in self.unk3:
            data.append(b)
        data.append(self.unk4)
        return data
