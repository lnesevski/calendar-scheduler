using CalendarScheduler.Domain.Models;
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
    public class CalendarEventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AllEvents()
        {
            var allEvents = new List<Event>();
            var tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "token.json");
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenPath));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            request.AddQueryParameter("key", "AIzaSyAm8a-jvYiW3ZtNLHS6w9DK6PTZpouEysc");
            request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");
            foreach (Calendar calendar in AllCalendars())
            {
                var calendar_id = calendar.Id;

                var uri = "https://www.googleapis.com/calendar/v3/calendars/" + calendar_id + "/events";

                uri = uri.Replace("#", "%23").Replace("@", "%40");

                restClient.BaseUrl = new System.Uri(uri);

                var response = restClient.Get(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    JObject calendarEvents = JObject.Parse(response.Content);
                    allEvents.AddRange(calendarEvents["items"].ToObject<IEnumerable<Event>>());
                }
            }

            return View(allEvents);

        }

        public List<Calendar> AllCalendars()
        {
            var tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "token.json");
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenPath));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            request.AddQueryParameter("key", "AIzaSyAm8a-jvYiW3ZtNLHS6w9DK6PTZpouEysc");
            request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");

            restClient.BaseUrl = new System.Uri("https://www.googleapis.com/calendar/v3/users/me/calendarList");

            var response = restClient.Get(request);



            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject calendars = JObject.Parse(response.Content);
                var allCalendars = calendars["items"].ToObject<IEnumerable<Calendar>>();
                return (List<Calendar>)allCalendars;
            }

            return new List<Calendar>();
        }
    }
}
