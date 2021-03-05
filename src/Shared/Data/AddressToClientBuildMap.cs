using LiteDB;

namespace Classic.Shared.Data
{
    public class AddressToClientBuildMap
    {
        public ObjectId Id { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public int ClientBuild { get; set; }
    }
}
