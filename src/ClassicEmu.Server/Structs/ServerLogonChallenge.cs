using ClassicEmu.Shared;
using System.Collections.Generic;

namespace ClassicEmu.Server.Structs
{
    /// <summary>
    /// REFERENCE: http://arcemu.org/wiki/Server_Logon_Challenge
    /// This message is the response to the Client_Logon_Challenge message sent by 
    /// the client, when initiating the logon process. This is the second step in 
    /// the Logon_Process This messages gives the client some SRP6 values that were 
    /// generated using the username the client sent and the password found in the 
    /// server database.
    /// </summary>
    [Status("Finished")]
    public struct ServerLogonChallenge
    {
        /// <summary>
        /// Cmd is the command/operation code of the packet. Always 0 for this message.
        /// </summary>
        public byte cmd;

        /// <summary>
        /// Error is the code identifying the error that occured during processing the 
        /// Client_Logon_Challenge message.
        /// </summary>
        public byte error;

        /// <summary>
        /// Unknown, always 0 in Arcemu.
        /// </summary>
        public byte unk2;

        /// <summary>
        /// B is an SRP6 value. It is the server's public value.
        /// </summary>
        public byte[] B;

        /// <summary>
        /// Length of the SRP6 g value we send the client in bytes. 
        /// Always 1 in Arcemu.
        /// </summary>
        public byte g_len;

        /// <summary>
        /// The SRP6 g value we send the client. Always 7 in Arcemu.
        /// </summary>
        public byte g;

        /// <summary>
        /// Lenght of the SRP6 N value we send the client. Always 32 in Arcemu.
        /// </summary>
        public byte N_len;

        /// <summary>
        /// The SRP6 N value we send the client. Always 
        /// 894B645E89E1535BBDAD5B8B290650530801B18EBFBF5E8FAB3C82872A3E9BB7 
        /// in Arcemu.
        /// </summary>
        public byte[] N;

        /// <summary>
        /// The SRP6 s value.
        /// </summary>
        public byte[] s;

        /// <summary>
        /// Unknown. Arcemu sends a randomly generated 16 byte value.
        /// </summary>
        public byte[] unk3;

        /// <summary>
        /// Unknown. Arcemu sends a single byte 0.
        /// </summary>
        public byte unk4;

        public byte[] ToByteArray()
        {
            var bytes = new List<byte>();
            bytes.Add(cmd);
            bytes.Add(unk2);
            bytes.Add(error);
            bytes.AddRange(B);
            bytes.Add(g_len);
            bytes.Add(g);
            bytes.Add(N_len);
            bytes.AddRange(N);
            bytes.AddRange(s);
            bytes.AddRange(unk3);
            bytes.Add(unk4);
            return bytes.ToArray();
        }
    }
}
