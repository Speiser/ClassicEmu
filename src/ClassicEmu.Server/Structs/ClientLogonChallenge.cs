using System;
using System.Text;
using ClassicEmu.Shared;

namespace ClassicEmu.Server.Structs
{
    /// <summary>
    /// REFERENCE: http://arcemu.org/wiki/Client_Logon_Challenge
    /// This message initiates the authentication with the Logon Server. 
    /// This is the first step of the Logon Process, which sends the I (username) 
    /// SRP6 protocol variable. The server uses it to generate the variables that 
    /// are sent to the client in the Server Logon Challenge message.
    /// </summary>
    [Status("Finished")]
    public struct ClientLogonChallenge
    {
        /// <summary>
        /// Cmd is the command/operation code of the packet. 
        /// Always 0 for this message.
        /// </summary>
        public byte cmd;

        /// <summary>
        /// Unknown.
        /// </summary>
        public byte error;

        /// <summary>
        /// Size of the remaining part of the message.
        /// </summary>
        public UInt16 size;

        /// <summary>
        /// 4 byte C-String, containing the String "WoW\0".
        /// </summary>
        public byte[] gamename;

        /// <summary>
        /// Major version number of the client ( 3 for 3.3.5 ).
        /// </summary>
        public byte version1;

        /// <summary>
        /// Minor version number of the client ( 3 for 3.3.5 ).
        /// </summary>
        public byte version2;

        /// <summary>
        /// Patchlevel version number of the client ( 5 for 3.3.5 ).
        /// </summary>
        public byte version3;

        /// <summary>
        /// Build number of the client. ( 12340 for 3.3.5a ).
        /// </summary>
        public UInt16 build;

        /// <summary>
        /// Platform the client is running on, reversed C-String ( "68x\0" for x86 ).
        /// </summary>
        public byte[] platform;

        /// <summary>
        /// OS the client is running on, reversed C-String ( "niW\0" for Windows ).
        /// </summary>
        public byte[] os;

        /// <summary>
        /// Locale of the client, reversed C-String ( "SUne" for enUS ).
        /// </summary>
        public byte[] country;

        /// <summary>
        /// Unknown.
        /// </summary>
        public UInt32 timezone_bias;

        /// <summary>
        /// IP address of the client in binary format.
        /// </summary>
        public UInt32 ip;

        /// <summary>
        /// Length of the Identity ( user name ) in characters.
        /// </summary>
        public byte I_len;

        /// <summary>
        /// The Identity string ( user name ).
        /// </summary>
        public byte[] I;

        public override string ToString()
        {
            return $"cmd\t\t{cmd}\nerror\t\t{error}\nsize\t\t{size}\ngamename\t{Encoding.ASCII.GetString(gamename)}\nversion1\t{version1}\nversion2\t{version2}\nversion3\t{version3}\n" +
                   $"build\t\t{build}\nplatform\t{Encoding.ASCII.GetString(platform)}\nos\t\t{Encoding.ASCII.GetString(os)}\ncountry\t\t{Encoding.ASCII.GetString(country)}\n" +
                   $"timezone\t{timezone_bias}\nip\t\t{ip}\nI_len\t\t{I_len}\nI\t\t{Encoding.ASCII.GetString(I)}";
        }
    }
}
