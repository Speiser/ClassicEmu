class ServerLogonProof:
    cmd = 1
    error = 0
    M2 = None
    accountflags = [0, 0, 0, 0]

    def __init__(self, clientM1):
        self.M2 = clientM1
        
    def get(self):
        data = [self.cmd, self.error]
        for m in self.M2:
            data.append(m)
        for f in self.accountflags:
            data.append(f)
        return data
        