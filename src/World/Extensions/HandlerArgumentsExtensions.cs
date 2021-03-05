using System.Threading.Tasks;
using Classic.Shared.Data;
using Classic.World.Messages;

namespace Classic.World.Extensions
{
    internal static class HandlerArgumentsExtensions
    {
        public static bool IsVanilla(this HandlerArguments args) => args.Client.Build == ClientBuild.Vanilla;
        public static bool IsTBC(this HandlerArguments args) => args.Client.Build == ClientBuild.TBC;

        public static async Task SendPacket<T>(this HandlerArguments args) where T : ServerMessageBase<Opcode>, new()
            => await args.Client.SendPacket(new T());
    }
}
