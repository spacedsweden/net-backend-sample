using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using SinchBackend.Models;

namespace SinchBackend.Controllers {
    /// <summary>
    /// this code is from the tutorial tutorial.sinch.com/net-backend-sample
    /// </summary>
    public class AuthController : ApiController {

        private const string applicationKey = "<your key>";
        private const string applicationSecret = "<your secret>";
        public string Signature(string userId) {
            UserTicket userTicket = new UserTicket();
            userTicket.Identity = new Identity { Type = "username", Endpoint = userId };
            userTicket.ApplicationKey = applicationKey;
            userTicket.Created = DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture);
            Debug.WriteLine(DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture));
            var json = JsonConvert.SerializeObject(userTicket);
            var ticketData = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(json));
            var sha256 = new HMACSHA256(Convert.FromBase64String(applicationSecret));
            var signature = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(json)));
            Debug.WriteLine(json);
            return ticketData + ":" + signature;
        }

        public async Task<LoginObject> AuthUser(string username, string password) {
            ///1. Verfiy user
            var signinManager = Request.GetOwinContext().Get<ApplicationSignInManager>();
            var result = await signinManager.PasswordSignInAsync(username, password, false, shouldLockout: false);
            if (result == SignInStatus.Success) {
                /// 2. Create return type and sign the request
                LoginObject loginObject = new LoginObject();
                loginObject.UserTicket = Signature(username);
                return loginObject;
            } else {
                ///wrong username and password
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }
    }
}
