class ServerLogonProof:
    cmd = 1
    error = 0
    M2 = None

    def __init__(self, srp):
        self.srp = srp
        
    def get(self, M1):
        data = []
        if self.srp.getM(M1):
            data.append(1)
            data.append(0)
            for m in self.srp.M:
                data.append(m)
            data.append(1)
            data.append(0)
            data.append(0)
            data.append(0)
            data.append(0)
            data.append(0)
            data.append(0)
            data.append(0)
            data.append(0)
            data.append(0)
        else:
            data = [0, 0, 4]
        return data