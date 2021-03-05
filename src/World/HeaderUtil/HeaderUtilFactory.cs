using System;
using Classic.Shared.Data;
using Classic.World.Cryptography;

namespace Classic.World.HeaderUtil
{
    public class HeaderUtilFactory
    {
        public static IHeaderUtil Create(int build, AuthCrypt crypt) => build switch
        {
            ClientBuild.Vanilla or ClientBuild.TBC => new VanillaTBCHeaderUtil(crypt),
            _ => throw new NotImplementedException($"HeaderUtilFactory(build: {build})"),
        };
    }
}
