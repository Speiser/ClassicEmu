import hashlib


def hash_sha1(data):
    """ Hashes the given data.
    :param data: Data which will be hashed.
    :returns: The hashed data.
    """
    h = hashlib.sha1()
    h.update(data)
    return h.digest()
