using System;
using Classic.Common;
using Classic.Cryptography;

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
