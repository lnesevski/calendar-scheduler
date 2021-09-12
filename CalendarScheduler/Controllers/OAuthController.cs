using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CalendarScheduler.Domain.Models;
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

        public void RefreshToken()
        {
            var tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "token.json");
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenPath));

            var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "credentials.json");
            var credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsPath));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            request.AddQueryParameter("client_id", credentials["client_id"].ToString());
            request.AddQueryParameter("client_secret", credentials["client_secret"].ToString());
            request.AddQueryParameter("grant_type", "refresh_token");
            request.AddQueryParameter("refresh_token", tokens["refresh_token"].ToString());

            restClient.BaseUrl = new System.Uri("https://oauth2.googleapis.com/token");
            var response = restClient.Post(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject newTokens = JObject.Parse(response.Content);
                newTokens["refresh_token"] = tokens["refresh_token"].ToString();
                System.IO.File.WriteAllText(tokenPath, newTokens.ToString());                
            }
        }

        public ActionResult Revoke()
        {
            this.RefreshToken();

            var tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "token.json");
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenPath));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            request.AddQueryParameter("token", tokens["access_token"].ToString());

            restClient.BaseUrl = new System.Uri("https://oauth2.googleapis.com/revoke");

            var response = restClient.Post(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Home", new { status = "success" });

            }

            return RedirectToAction("Index", "Home", new { status = "failed" });

        }
    }
}
