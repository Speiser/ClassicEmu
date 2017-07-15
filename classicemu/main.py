import os
import sys

from logonserver import LogonServer

os.system('cls')
ls = LogonServer()
try:
    ls.start()
except KeyboardInterrupt:
    sys.exit()


"""
Sent: 119 bytes.
Received: 42 bytes.
Sent: 26 bytes.
Received: 75 bytes.
Sent: 10 bytes.
Received: 5 bytes.
Sent: 10 bytes.
Received: 5 bytes.
"""