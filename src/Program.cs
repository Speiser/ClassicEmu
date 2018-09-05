using Classic.Auth;

namespace Classic
{
    class Program
    {
        static void Main(string[] args)
        {
            new AuthenticationServer().Start();
        }
    }
}