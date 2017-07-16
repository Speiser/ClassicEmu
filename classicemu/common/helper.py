import binascii
import string
import threading


def print_packet(data):
    """ Prints a received packet in a nice "wireshark" style. """
    dump = ''
    index = 0
    while index < len(data):
        temp = data[index:(index + 16)]
        data_str = _get_hex(temp)
        ascii_str = _get_ascii(temp)
        dump += '    {:<47} {}\n'.format(data_str, ascii_str)
        index += 16

    print('\n  Packet length: ' + str(len(data)))
    print(dump)


def run_thread(action):
    """ Runs a given action in a new thread.
    :param action: The given action.
    """
    thread = threading.Thread(target=action)
    thread.start()


def _get_hex(data):
    """ print_packet() helper. """
    hexdump = binascii.hexlify(data).decode('ascii')
    spaced_hexdump = ''
    index = 0
    while index < len(hexdump):
        spaced_hexdump += hexdump[index:(index + 2)] + ' '
        index += 2
    return spaced_hexdump.strip()


def _get_ascii(data):
    """ print_packet() helper. """
    asciidump = ''
    for char in data:
        char = chr(char)
        if char in string.printable and char not in '\n\r\t':
            asciidump += char
        else:
            asciidump += '.'
    return asciidump
