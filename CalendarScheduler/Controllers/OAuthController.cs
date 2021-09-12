using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarScheduler.Web.Controllers
{
    public class OAuthController : Controller
    {
        public void Callback(string code, string error, string state)
        {
            if(string.IsNullOrWhiteSpace(error))
            {
                GetTokens(code);
            }
        }

        public ActionResult GetTokens(string code)
        {
            var tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "token.json");
            var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "credentials.json");
            var credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsPath));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            request.AddQueryParameter("client_id", credentials["client_id"].ToString());
            request.AddQueryParameter("client_secret", credentials["client_secret"].ToString());
            request.AddQueryParameter("code", code);
            request.AddQueryParameter("grant_type", "authorization_code");
            request.AddQueryParameter("redirect_uri", "https://localhost:44351/oauth/callback");

            restClient.BaseUrl = new System.Uri("https://oauth2.googleapis.com/token");
            var response = restClient.Post(request);
        
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                System.IO.File.WriteAllText(tokenPath, response.Content);
                return RedirectToAction("Index", "Home");
            }

            return View("Error");
            
        }
    }
}
