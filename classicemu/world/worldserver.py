import socket

from classicemu.serverbase import ServerBase
from classicemu.world.worldconnection import WorldConnection
from classicemu.common.helper import run_thread


class WorldServer(ServerBase):
    
    def __init__(self):
        super().__init__("WorldServer")

    def process_client(self, connection, address):
        world_conn = WorldConnection(connection, f'{address[0]}:{address[1]}')
        run_thread(world_conn.handle_connection)
