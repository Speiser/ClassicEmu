import hashlib

def sha1(data):
    return hashlib.sha1(data).digest()

