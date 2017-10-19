import os
import sys

from classicemu.auth.logonserver import LogonServer

if __name__ == '__main__':
    os.system('cls')
    try:
        LogonServer().start()
    except KeyboardInterrupt:
        sys.exit()
