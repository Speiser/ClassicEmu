﻿using Classic.Shared;

namespace Classic.World.Packets.Client;

public class CMSG_PLAYER_LOGIN
{
    public CMSG_PLAYER_LOGIN(byte[] data)
    {
        using (var reader = new PacketReader(data))
        {
            CharacterID = reader.ReadUInt32();
        }
    }

    public uint CharacterID { get; }
}
