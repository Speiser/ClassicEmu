using ClassicEmu.Server.Structs;
using ClassicEmu.Shared;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
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
            SHA1 sha = SHA1.Create();

            BigInteger _N = BigInteger.Parse("894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7", NumberStyles.HexNumber);
            BigInteger _s = BigInteger.Parse("4D66C5A48659D395362CCC29F879431", NumberStyles.HexNumber);
            BigInteger _b = BigInteger.Parse("7B798871F29F435533227B798871F29F43553322", NumberStyles.HexNumber); // Server Private
            BigInteger _g = new BigInteger(7);
            BigInteger _k = new BigInteger(3);

            BigInteger _x;
            BigInteger _v; // Verifier
            

            string pw = (clc.I + ":" + "password").ToUpper();
            byte[] pwHash = sha.ComputeHash(Encoding.UTF8.GetBytes(pw));
            byte[] x = sha.ComputeHash((_s.ToByteArray().Reverse().Concat(pwHash)).ToArray());
            _x = new BigInteger(x);
            _v = BigInteger.ModPow(_g, BigInteger.Abs(_x), BigInteger.Abs(_N));
            BigInteger _B = (_v * _k + BigInteger.ModPow(_g, _b, _N)) % _N; ;  // server public value

            ServerLogonChallenge slc = new ServerLogonChallenge()
            {
                cmd = 0,
                error = 0x00,
                unk2 = 0,
                B = _B.ToByteArray(),
                g_len = 1,
                g = 7,
                N_len = 32,
                N = _N.ToByteArray(),
                s = _s.ToByteArray(),
                unk3 = _s.ToByteArray(),
                unk4 = 0
            };

            return slc.ToByteArray();
        }
    }
}
