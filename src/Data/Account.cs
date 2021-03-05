using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Classic.Data
{
    public class Account
    {
        public string Identifier { get; init; }
        public List<ulong> Characters { get; init; } = new List<ulong>();

        // Holds the account data received by CMSG_UPDATE_ACCOUNT_DATA
        // First int is Build Version, second int is DataType.
        public Dictionary<int, AccountData> AccountData { get; init; } = new Dictionary<int, AccountData>();
    }
}
