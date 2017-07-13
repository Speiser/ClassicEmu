class ServerLogonProof:
    cmd = 1
    error = 0
    M2 = None

    def __init__(self, clientM1):
        self.M2 = clientM1
        
    def get(self):
        data = [self.cmd, self.error]
        for m in self.M2:
            data.append(m)
        data.append(0)
        data.append(0)
        data.append(0)
        data.append(0)
        return data