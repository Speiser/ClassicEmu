﻿using System.Threading.Tasks;
using Classic.World.Packets;
using Classic.World.Packets.Client;

namespace Classic.World.Handler;

public class AccountHandler
{
    [OpcodeHandler(Opcode.CMSG_UPDATE_ACCOUNT_DATA)]
    public static Task UpdateAccountData(PacketHandlerContext c)
    {
        var request = new CMSG_UPDATE_ACCOUNT_DATA(c.Packet);
        return Task.CompletedTask;
    }
}
