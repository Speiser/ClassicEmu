import os
import sys

from loginserver import LoginServer

os.system('cls')
ls = LoginServer()
try:
    ls.start()
except KeyboardInterrupt:
    sys.exit()