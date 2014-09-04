using Newtonsoft.Json;

namespace SinchBackend.Models {
public class UserTicket {
    [JsonProperty("identity")]
    public Identity Identity { get; set; }
    [JsonProperty("applicationkey")]
    public string ApplicationKey { get; set; }
    [JsonProperty("created")]
    public string Created { get; set; }
}
}