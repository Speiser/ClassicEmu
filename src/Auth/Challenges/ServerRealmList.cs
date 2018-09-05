using Classic.Common;
using System.Runtime.InteropServices;
using System.Text;

namespace Classic.Auth.Challenges
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
    internal struct S_RealmHeader
    {
        public byte cmd;
        public ushort size;
        public uint unk1;
        public byte num_realms;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 40)] // Hardcoded size for now...
    internal struct S_RealmInfo
    {
        public uint type;
        public byte flags;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)] // Hardcoded size for now...
        public byte[] name;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)] // Hardcoded size for now...
        public byte[] addr_port;
        public uint population;
        public byte num_chars;
        public byte time_zone;
        public byte unk1;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 2)]
    internal struct S_RealmFooter
    {
        public ushort unk1;
    }

    public class ServerRealmList
    {
        public void Send(ClientBase client)
        {
            var info = new S_RealmInfo
            {
                type = 1,
                flags = 0,
                name = Encoding.ASCII.GetBytes("Test Server\0"),
                addr_port = Encoding.ASCII.GetBytes("127.0.0.1:13250\0"),
                population = 0,
                num_chars = 0,
                time_zone = 0,
                unk1 = 0
            };

            var footer = new S_RealmFooter
            {
                unk1 = 0
            };

            var header = new S_RealmHeader
            {
                cmd = 16,
                size = (ushort)(Marshal.SizeOf(info) + 7),
                unk1 = default(uint),
                num_realms = 1
            };

            client.Send(ByteSerializer.Serialize(header));
            client.Send(ByteSerializer.Serialize(info));
            client.Send(ByteSerializer.Serialize(footer));
        }
    }
}
