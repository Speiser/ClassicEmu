import hashlib

def hash_sha1(data):
    h = hashlib.sha1()
    h.update(data)
    return h.digest()

