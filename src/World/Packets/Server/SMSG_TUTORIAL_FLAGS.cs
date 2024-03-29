﻿namespace Classic.World.Packets.Server;

public class SMSG_TUTORIAL_FLAGS : ServerPacketBase<Opcode>
{
    public SMSG_TUTORIAL_FLAGS() : base(Opcode.SMSG_TUTORIAL_FLAGS)
    {
    }

    public override byte[] Get()
    {
        for (var i = 0; i < 32; i++)
        {
            this.Writer.WriteUInt8(0xFF);
        }

        return this.Writer.Build();
    }
}
