using System;
using System.Linq;
using LiteDB;

namespace Classic.Shared.Data.Repositories
{
    public class AccountSessionRepository
    {
        private readonly ILiteCollection<AccountSession> sessions;
        private readonly ILiteCollection<AddressToClientBuildMap> addressBuildMap;

        public AccountSessionRepository(ILiteDatabase db)
        {
            this.sessions = db.GetCollection<AccountSession>("accountSessions");
            this.addressBuildMap = db.GetCollection<AddressToClientBuildMap>("addressBuildMap");
        }

        public AccountSession GetSession(string identifier) => this.sessions.FindOne(x => x.Identifier == identifier);

        public void AddSession(AccountSession session)
        {
            this.sessions.Insert(session);
            this.sessions.EnsureIndex(x => x.Identifier);
        }

        public bool DeleteSession(string identifier)
        {
            var session = this.GetSession(identifier);
            return session is not null && this.sessions.Delete(session.Id);
        }

        public void ClearAccountSessions() => this.sessions.DeleteAll();

        // This is a hack, since I need to figure out the Client Build before sending the first message from the world client
        public AddressToClientBuildMap GetClientBuildFromAddress(string ip, int port)
        {
            var addressCorrect = this.addressBuildMap.Find(x => x.IPAddress == ip).ToList();

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

        public void DeleteAddressToClientBuildMap(AddressToClientBuildMap map) => this.addressBuildMap.Delete(map.Id);

        public void AddClientBuildForAddress(string ip, int port, int build)
        {
            this.addressBuildMap.Insert(new AddressToClientBuildMap { IPAddress = ip, Port = port, ClientBuild = build });
            this.addressBuildMap.EnsureIndex(x => x.IPAddress);
        }
    }
}
