import binascii
import string
import threading

def print_packet(data):
    dump = ''
    index = 0
    while index < len(data):
        temp = data[index:(index + 16)]
        data_str = _get_hex(temp)
        ascii_str = _get_ascii(temp)
        dump += '{:<47} {}\n'.format(data_str, ascii_str)
        index += 16

    print('\n' + dump)

def run_thread(action):
    thread = threading.Thread(target = action)
    thread.start()

def _get_hex(data):
    hexdump = binascii.hexlify(data).decode('ascii')
    spaced_hexdump = ''
    index = 0
    while index < len(hexdump):
        spaced_hexdump += hexdump[index:(index + 2)] + ' '
        index += 2
    return spaced_hexdump.strip()

def _get_ascii(data):
    asciidump = ''
    for char in data:
        char = chr(char)
        if char in string.printable and not char in '\n\r\t':
            asciidump += char
        else:
            asciidump += '.'
    return asciidump