namespace DW.ELA.Plugin.Inara.Model
{
    using System.Collections.Generic;
    using DW.ELA.Utility.Json;
    using Newtonsoft.Json;

    internal struct ApiOutputBatch
    {
        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("events")]
        public IList<ApiOutputEvent> Events { get; set; }

        public override string ToString() => Serialize.ToJson(this);
    }
}
