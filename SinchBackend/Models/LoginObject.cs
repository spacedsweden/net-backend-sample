using Newtonsoft.Json;

namespace SinchBackend.Models {
    public class LoginObject {
        [JsonProperty("userTicket")]
        public string UserTicket { get; set; }

    }
}