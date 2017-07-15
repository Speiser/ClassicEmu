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
    Summary.
    :param name: text
    :returns: returns
"""
