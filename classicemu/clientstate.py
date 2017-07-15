from enum import Enum


class ClientState(Enum):
    Init = 0
    ClientLogonChallenge = 1
    ServerLogonChallenge = 2
    ClientLogonProof = 3
    ServerLogonProof = 4
    Authenticated = 5
    Disconnected = 6
