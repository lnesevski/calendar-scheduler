﻿using CalendarScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult OauthRedirect()
        {
            var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "credentials.json");

            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsPath));
            var client_id = credentials["client_id"];

            var redirectUrl = "https://accounts.google.com/o/oauth2/v2/auth?" +
                              "scope=https://www.googleapis.com/auth/calendar+https://www.googleapis.com/auth/calendar.events&" + 
                              "access_type=online&" +
                              "include_granted_scopes=true&" + 
                              "response_type=code&" +
                              "state=hellothere&" +
                              "redirect_uri=https://localhost:44351/oauth/callback&" + 
                              "client_id=" + client_id;

            return Redirect(redirectUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}