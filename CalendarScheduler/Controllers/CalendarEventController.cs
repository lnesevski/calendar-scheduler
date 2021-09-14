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

            var keyPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "apikey.json");
            var key = JObject.Parse(System.IO.File.ReadAllText(keyPath))["key"].ToString();

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            request.AddQueryParameter("key", key);
            request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");
            foreach (Calendar calendar in AllCalendarsList())
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

        public IActionResult AllCalendars()
        {
            return View(AllCalendarsList());
        }

        private List<Calendar> AllCalendarsList()
        {
            var tokenPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "token.json");
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenPath));


            var keyPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "apikey.json");
            var key = JObject.Parse(System.IO.File.ReadAllText(keyPath))["key"].ToString();

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            request.AddQueryParameter("key", key);
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
