using System.Collections.Generic;

namespace Project.Core
{
    public sealed class ThirdPartyConnectionOptions
    {
        public const string OptionsKey = "ThirdPartyConnections";
        public IDictionary<string, ThirdPartyConnectionModel> ThirdPartyConnections { get; set; }
    }
}
