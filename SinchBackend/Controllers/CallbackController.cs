using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SinchBackend.Models;
using Action = SinchBackend.Models.Action;

namespace SinchBackend.Controllers {
    public class CallbackController : ApiController {
        [HttpPost]
        public async Task<Svaml> Post(HttpRequestMessage message) {
            //read the response
            String body = await message.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JObject>(body);
            //create a new respone object
            var svaml = new Svaml();
            if (json["event"].ToString() == "ice") {
                //only calls to my personal phone is allowed with this app
                if (json["to"]["endpoint"].ToString() == "+15612600684") {
                    svaml.Action = new SimpleICEAction() {
                        Name = "ConnectPSTN",
                        callback = true,
                        maxDuration = 600
                    };
                } else {
                    //else hangup
                    svaml.Action = new Action {
                        Name = "hangup"
                    };
                }
            }
            if (json["event"].ToString() == "ace") {
                //respo.Content = new HttpContent "{Action:{\"name\" : \"ConnectPSTN\"}";
                svaml.Action = new Action() {
                    Name = "continue",
                };
            }
            if (json["event"].ToString() == "dice") {
                //respo.Content = new HttpContent "{Action:{\"name\" : \"ConnectPSTN\"}";
                svaml.Action = new Action() {
                    Name = "dice",
                };
            }
            var context = GlobalHost.ConnectionManager.GetHubContext<CallbackHub>();
            JsonSerializer _jsonWriter = new JsonSerializer {
                NullValueHandling = NullValueHandling.Ignore,
            };

            var svamljson = JsonConvert.SerializeObject(svaml, Formatting.Indented,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            context.Clients.All.broadcastMessage(DateTime.Now + " <b>Request:</b>" + body + "\n<br/>" + "<b>Response:</b>" + svamljson);
            return svaml;
        }
    }


}
