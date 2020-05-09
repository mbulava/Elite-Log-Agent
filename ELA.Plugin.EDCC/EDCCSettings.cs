using Newtonsoft.Json;

namespace ELA.Plugin.EDCC
{
    public class EDCCSettings
    {
       

        [JsonProperty("Edcc.ConnectionString")]
        public string ConnectionString { get; set; }
    }
}