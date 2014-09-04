using Newtonsoft.Json;

namespace SinchBackend.Models {
    public class Identity {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }
    }
}