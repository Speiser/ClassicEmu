using System;
using Classic.Shared.Data;
using Classic.World.Cryptography.HeaderCrypts;

namespace Classic.World.Cryptography;

public class HeaderCryptFactory
{
    public static IHeaderCrypt Create(byte[] sessionKey, int build) => build switch
    {
        ClientBuild.Vanilla => new VanillaHeaderCrypt(sessionKey),
        ClientBuild.TBC => new TBCHeaderCrypt(sessionKey),
        ClientBuild.WotLK => new WotLKHeaderCrypt(sessionKey),
        _ => throw new NotImplementedException($"HeaderCryptFactory.Create(build: {build})"),
    };
}
