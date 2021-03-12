using System.Collections.Generic;
using LiteDB;

namespace Classic.Shared.Data
{
    public class Account
    {
        public ObjectId Id { get; set; }
        public string Identifier { get; init; }
        public List<ulong> Characters { get; init; } = new List<ulong>();

        // Holds the account data received by CMSG_UPDATE_ACCOUNT_DATA
        // int is Build Version
        public Dictionary<int, AccountData> AccountData { get; init; } = new Dictionary<int, AccountData>();
    }
}
