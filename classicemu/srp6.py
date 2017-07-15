""" http://srp.stanford.edu/design.html

SRP Protocol Design

SRP is the newest addition to a new class of strong authentication protocols that resist all the well-known passive and active attacks over the network. SRP borrows some elements from other key-exchange and identification protcols and adds some subtle modifications and refinements. The result is a protocol that preserves the strength and efficiency of the EKE family protocols while fixing some of their shortcomings.
The following is a description of SRP-6 and 6a, the latest versions of SRP:

    N    A large safe prime (N = 2q+1, where q is prime)
         All arithmetic is done modulo N.
    g    A generator modulo N
    k    Multiplier parameter (k = H(N, g) in SRP-6a, k = 3 for legacy SRP-6)
    s    User's salt
    I    Username
    p    Cleartext Password
    H()  One-way hash function
    ^    (Modular) Exponentiation
    u    Random scrambling parameter
    a,b  Secret ephemeral values
    A,B  Public ephemeral values
    x    Private key (derived from p and s)
    v    Password verifier

The host stores passwords using the following formula:

    x = H(s, p)               (s is chosen randomly)
    v = g^x                   (computes password verifier)

The host then keeps {I, s, v} in its password database. The authentication protocol itself goes as follows:

    User -> Host:  I, A = g^a                  (identifies self, a = random number)
    Host -> User:  s, B = kv + g^b             (sends salt, b = random number)
    
            Both:  u = H(A, B)
    
            User:  x = H(s, p)                 (user enters password)
            User:  S = (B - kg^x) ^ (a + ux)   (computes session key)
            User:  K = H(S)
    
            Host:  S = (Av^u) ^ b              (computes session key)
            Host:  K = H(S)

Now the two parties have a shared, strong session key K. To complete authentication, they need to prove to each other that their keys match. One possible way:

    User -> Host:  M = H(H(N) xor H(g), H(I), s, A, B, K)
    Host -> User:  H(A, M, K)

The two parties also employ the following safeguards:
The user will abort if he receives B == 0 (mod N) or u == 0.
The host will abort if it detects that A == 0 (mod N).
The user must show his proof of K first. If the server detects that the user's proof is incorrect, it must abort without showing its own proof of K.

"""
import math
import os

from sha1 import hash_sha1

class SRP6:
    # A is the client's public value.
    A = None

    # B is the server's public value.
    B = None

    # b is the server's secret value.
    b = 0

    g = 7

    # N is a safe-prime.
    N = 0x894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7

    # s is the salt, a random value.
    s = bytearray([0xF4, 0x3C, 0xAA, 0x7B, 0x24, 0x39, 0x81, 0x44, 0xBF, 0xA5, 0xB5, 0x0C, 0x0E, 0x07, 0x8C, 0x41,
         0x03, 0x04, 0x5B, 0x6E, 0x57, 0x5F, 0x37, 0x87, 0x31, 0x9F, 0xC4, 0xF8, 0x0D, 0x35, 0x94, 0x29])
    
    # v is the SRP6 Verification value.
    v = None

    # x = SHA1(s | SHA1(I | ":" | P))
    x = None

    # u is the so called "Random scrambling parameter".
    u = None

    # Identifier (username).
    I = ''

    # Cleartext password.
    P = ''

    # Sessionkey
    S = None

    # K is the hashed session key, hashed with SHA1.
    K = None

    # M = H(H(N) xor H(g), H(I), s, A, B, K)
    M = None

    def __init__(self, username, password):
        self.I = username
        self.P = password
        self.b = int.from_bytes(os.urandom(152), "little") % self.N
        self._x()
        self.v = pow(self.g, int.from_bytes(self.x, byteorder='little'), self.N)

        gmod = pow(self.g, self.b, self.N)
        B = ((3 * self.v) + gmod) % self.N
        self.B = int.to_bytes(B, 32, byteorder='little')

    def _x(self):
        temp = hash_sha1(self.I + b':' + self.P)
        self.x = hash_sha1(self.s + temp)

    def getM(self, M1):
        """
        A = g^a
        B = kv + g^b
        u = H(A, B)
        x = H(s, p)
        S = (B - kg^x) ^ (a + ux)       -> S = (A * v^u) ^ b % N (https://www.ietf.org/rfc/rfc2945.txt)
        K = H(S)
        M = H(H(N) xor H(g), H(I), s, A, B, K)

            A is the client public value.
            B is the server public value.
            u is the so called "Random scrambling parameter".
            x is the Private key.
            S is the Session key.
            K is the hashed session key, hashed with H hash function ( H is SHA1 in our case ).
        """
        # u = H(A, B)
        self.u = hash_sha1(self.A + self.B)

        # S = (A * (v^u) % N) ^ b % N
        temp_A = int.from_bytes(self.A, byteorder='little')
        temp_u = int.from_bytes(self.u, byteorder='little')
        self.S = pow((temp_A * pow(self.v, temp_u, self.N)), self.b, self.N)
        S_bytes = self.S.to_bytes(40, byteorder='little')
        self.K = hash_sha1(S_bytes)

        check = self._getM(M1)
        return check


    def _getM(self, M1):
        # https://github.com/arcemu/arcemu/blob/master/src/arcemu-logonserver/AuthSocket.cpp#L345
        t = self.S.to_bytes(40, byteorder='little')
        t1 = []
        vK = []
        for i in range(40):
            vK.append(0)

        for i in range(16):
            t1.append(t[i * 2])

        t1_hash = hash_sha1(bytes(t1))

        for i in range(20):
            vK[i * 2] = t1_hash[i]

        for i in range(16):
            t1[i] = t[i * 2 + 1]

        t1_hash = hash_sha1(bytes(t1))

        for i in range(20):
            vK[i * 2 + 1] = t1_hash[i]

        sessionkey = bytes(vK)

        N_hash = hash_sha1(self.N.to_bytes(32, byteorder='little').rstrip(b'\x00'))
        g_hash = hash_sha1(self.g.to_bytes(32, byteorder='little').rstrip(b'\x00'))
        t3 = []
        for i in range(20):
            t3.append(N_hash[i] ^ g_hash[i])

        t4 = hash_sha1(self.I)
        
        c_proof = hash_sha1(bytes(t3) + t4 + self.s + self.A + self.B + sessionkey)

        if M1 != c_proof:
            return False

        self.M = hash_sha1(self.A + c_proof + sessionkey + bytes(0))
        return True
