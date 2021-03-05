using System;
using System.Linq;
using Classic.Shared.Data;
using LiteDB;

namespace Classic.Shared
{
    // TODO: as instance
    public class AccountStore
    {
        private static readonly ILiteDatabase database = new LiteDatabase(new ConnectionString
        {
            Filename = Configuration.DatabaseConnectionString,
            Connection = ConnectionType.Shared,
        });
        private static readonly ILiteCollection<Account> accounts = database.GetCollection<Account>("accounts");
        private static readonly ILiteCollection<AccountSession> sessions = database.GetCollection<AccountSession>("accountSessions");
        private static readonly ILiteCollection<AddressToClientBuildMap> addressBuildMap = database.GetCollection<AddressToClientBuildMap>("addressBuildMap");

        public static Account GetAccount(string identifier)
        {
            return accounts.FindOne(x => x.Identifier == identifier);
        }

        public static void AddAccount(Account account)
        {
            accounts.Insert(account);
            accounts.EnsureIndex(x => x.Identifier);
        }

        public static AccountSession GetSession(string identifier)
        {
            return sessions.FindOne(x => x.Identifier == identifier);
        }

        public static void AddSession(AccountSession session)
        {
            sessions.Insert(session);
            sessions.EnsureIndex(x => x.Identifier);
        }

        public static bool DeleteSession(string identifier)
        {
            var session = GetSession(identifier);
            return GetSession(identifier) is not null && sessions.Delete(session.Id);
        }

        public static void ClearAccountSessions()
        {
            sessions.DeleteAll();
        }

        // This is a hack, since I need to figure out the Client Build before sending the first message from the world client
        public static AddressToClientBuildMap GetClientBuildFromAddress(string ip, int port)
        {
            var addressCorrect = addressBuildMap.Find(x => x.IPAddress == ip).ToList();

            if (addressCorrect.Count == 1)
            {
                return addressCorrect[0];
            }

            var portCorrect = addressCorrect.Where(x => x.Port == port - 1).ToList();
            
            if (portCorrect.Count > 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(portCorrect),
                    $"Found multiple clients from the same address with the same port: {ip}:{port - 1}");
            }

            return portCorrect.SingleOrDefault();
        }

        public static void DeleteAddressToClientBuildMap(AddressToClientBuildMap map)
        {
            addressBuildMap.Delete(map.Id);
        }

        public static void AddClientBuildForAddress(string ip, int port, int build)
        {
            addressBuildMap.Insert(new AddressToClientBuildMap { IPAddress = ip, Port = port, ClientBuild = build });
            addressBuildMap.EnsureIndex(x => x.IPAddress);
        }
    }
}
