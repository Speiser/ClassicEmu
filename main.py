import os
import sys

from classicemu.auth.logonserver import LogonServer
from classicemu.world.worldserver import WorldServer


if __name__ == '__main__':
    os.system('cls')
    try:
        if (sys.argv[1] == "login"): LogonServer().start()
        if (sys.argv[1] == "world"): WorldServer().start()
    except KeyboardInterrupt:
        sys.exit()
