using System;
using System.Collections.Generic;
using System.Text;
using Classic.Common;

namespace Classic.World.Character
{
    public class CharacterCreateResponse
    {
        public byte[] Get()
        {
            // TODO: Always returns success atm
            return new PacketWriter().WriteUInt8(46).Build();
        }
    }
}
