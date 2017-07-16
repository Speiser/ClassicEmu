import os
import sys

from classicemu.auth.logonserver import LogonServer

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
        print('Worldserver not ready..')
    else:
        print('Usage: python main.py [auth/world]')
        sys.exit(1)