using ClassicEmu.Server.Structs;
using ClassicEmu.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicEmu.Server
{
    public static class LogonMessageCreator
    {
        [Status("Finished")]
        public static ClientLogonChallenge CreateClientLogonChallenge(byte[] data)
        {
            ClientLogonChallenge clc = new ClientLogonChallenge()
            {
                cmd = data[0],
                error = data[1],
                size = BitConverter.ToUInt16(new byte[2] { data[2], data[3] }, 0),
                gamename = new byte[4] { data[4], data[5], data[6], data[7] },
                version1 = data[8],
                version2 = data[9],
                version3 = data[10],
                build = BitConverter.ToUInt16(new byte[2] { data[11], data[12] }, 0),
                platform = new byte[4] { data[13], data[14], data[15], data[16] },
                os = new byte[4] { data[17], data[18], data[19], data[20] },
                country = new byte[4] { data[21], data[22], data[23], data[24] },
                timezone_bias = BitConverter.ToUInt32(new byte[4] { data[25], data[26], data[27], data[28] }, 0),
                ip = BitConverter.ToUInt32(new byte[4] { data[29], data[30], data[31], data[32] }, 0),
                I_len = data[33],
                I = new byte[data[33]]
            };

            for (int i = 0; i < clc.I_len; i++)
            {
                clc.I[i] = data[i + 34];
            }

            return clc;
        }

        [Status("WIP")]
        public static byte[] CreateServerLogonChallenge(ClientLogonChallenge clc)
        {
            ServerLogonChallenge slc = new ServerLogonChallenge()
            {
                cmd = 0,
                error = 0x00,
                unk2 = 0,
                B = new byte[32] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                   0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                g_len = 1,
                g = 7,
                N_len = 32,
                N = Encoding.ASCII.GetBytes("894B645E89E1535BBDAD5B8B29065053"),    // changed size to 32
                s = Encoding.ASCII.GetBytes("0801B18EBFBF5E8FAB3C82872A3E9BB7"),    // should be random
                unk3 = Encoding.ASCII.GetBytes("0801B18EBFBF5E8FAB3C82872A3E9BB7"), // should be random
                unk4 = 0
            };

            return slc.ToByteArray();
        }
    }
}
