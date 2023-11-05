using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Classic.World.Cryptography;
using Classic.World.Packets;
using Classic.World.Packets.Client;
using Classic.World.Packets.Server;
using Microsoft.Extensions.Logging;

namespace Classic.World.Handler;

public class AuthenticationHandler
{
    [OpcodeHandler(Opcode.CMSG_AUTH_SESSION)]
    public static async Task OnClientAuthenticationSession(PacketHandlerContext c)
    {
        var request = CMSG_AUTH_SESSION.Read(c.Packet);
        var build = (int)request.Build;

        if (c.Client.Build != build)
        {
            c.Client.Log($"Expected build {c.Client.Build} but is {build}.", LogLevel.Warning);
            c.Client.Build = build;
        }

        var account = await c.AccountService.PGetAccount(request.Identifier);

        if (account is null || account.SessionKey is null)
        {
            // return [SMSG_AUTH_RESPONSE, 21]
            throw new ArgumentException($"No user with name {request.Identifier} found in db.");
        }

        ////: if server is full and NOT GM return [SMSG_AUTH_RESPONSE, 21]
        ////: if player is already connected return [SMSG_AUTH_RESPONSE, 13]

        using var sha = SHA1.Create();
        var calculatedDigest = sha.ComputeHash(
            Encoding.ASCII.GetBytes(request.Identifier)
                .Concat(new byte[] { 0, 0, 0, 0 })
                .Concat(BitConverter.GetBytes(request.Seed))
                .Concat(SMSG_AUTH_CHALLENGE.AuthSeed)
                .Concat(account.SessionKey)
                .ToArray());

        if (!calculatedDigest.SequenceEqual(request.Digest))
        {
            //return [SMSG_AUTH_RESPONSE, 21]
            throw new InvalidOperationException("Wrong digest SMSG_AUTH_RESPONSE");
        }

        c.Client.HeaderCrypt = HeaderCryptFactory.Create(account.SessionKey, build);
        c.Client.Identifier = request.Identifier;
        await c.Client.SendPacket(new SMSG_AUTH_RESPONSE(build));
    }
}
