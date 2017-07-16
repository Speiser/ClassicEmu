class ServerLogonProof:
    cmd = 1
    error = 0
    M2 = None

    def __init__(self, srp):
        """ Initializes a new instance of the ServerLogonProof class.
        :param srp: The client´s srp instance.
        """
        self.srp = srp

    def get(self, M1):
        """ Returns the serverlogonproof.
        :param M1: Client proof from the received clientlogonproof.
        :returns: The serverlogonproof.
        """
        if self.srp.getM(M1):
            data = [1, 0]
            for m in self.srp.M:
                data.append(m)
            data.append(0x00)
            data.append(0x00)
            data.append(0x00)
            data.append(0x00)
        else:
            data = [0, 0, 4]
        return data
