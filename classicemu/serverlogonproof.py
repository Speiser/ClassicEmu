class ServerLogonProof:
    cmd = 1
    error = 0
    M2 = None

    def __init__(self, srp):
        self.srp = srp
        
    def get(self):
        data = [self.cmd, self.error]
        self.M2 = self.srp.getM()
        for m in self.M2:
            data.append(m)
        data.append(1)
        data.append(0)
        data.append(0)
        data.append(0)
        return data