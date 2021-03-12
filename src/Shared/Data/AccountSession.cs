using LiteDB;

namespace Classic.Shared.Data
{
    public class AccountSession
    {
        public AccountSession() { }
        public AccountSession(string identifier, byte[] sessionKey)
        {
            this.SessionKey = sessionKey;
            this.Identifier = identifier;
        }

        public ObjectId Id { get; set; }
        public string Identifier { get; set; }
        public byte[] SessionKey { get; set; }
    }
}
