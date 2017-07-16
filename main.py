import os
import sys

from classicemu.auth.logonserver import LogonServer
from classicemu.world.worldserver import WorldServer

if __name__ == '__main__':
    os.system('cls')
    if len(sys.argv) < 2:
        print('Usage: python main.py [auth/world]')
        sys.exit(1)

    if sys.argv[1].lower() == 'auth':
        ls = LogonServer()
        try:
            ls.start()
        except KeyboardInterrupt:
            sys.exit()
    elif sys.argv[1].lower() == 'world':
        ws = WorldServer('127.0.0.1', 5001)
        ws.start()
    else:
        print('Usage: python main.py [auth/world]')
        sys.exit(1)