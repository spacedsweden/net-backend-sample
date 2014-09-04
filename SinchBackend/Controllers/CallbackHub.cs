using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SinchBackend.Controllers {
    public class CallbackHub : Hub {
        public void Send(string message) {
            Clients.All.broadcastMessage(message);
        }
    }
}