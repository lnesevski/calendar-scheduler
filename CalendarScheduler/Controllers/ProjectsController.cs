using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CalendarScheduler.Domain.Models;
using CalendarScheduler.Repository;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using CalendarScheduler.Service.Implementation;
using CalendarScheduler.Service.Interface;
using System.IO;
using Newtonsoft.Json.Linq;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using CalendarScheduler.Domain.DTO;
using NodaTime;

namespace CalendarScheduler.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private UserManager<IdentityUser> _userManager;

        public ProjectsController(IProjectService projectService, UserManager<IdentityUser> userManager)
        {
            _projectService = projectService;
            _userManager = userManager;
        }

        // GET: Projects
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_projectService.GetAllProjectForUser(userId));
        }

        // GET: Projects/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _projectService.GetProject((Guid)id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Description,Hours,WorkOnSaturdays,WorkOnSundays,Deadline,WorkStart,WorkEnd")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Id = Guid.NewGuid();
                project.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                this._projectService.Insert(project);
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _projectService.GetProject((Guid)id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,UserId,Title,Description,Hours,WorkOnSaturdays,WorkOnSundays,Deadline,WorkStart,WorkEnd")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _projectService.Update(project);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _projectService.GetProject((Guid)id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var project = _projectService.GetProject(id);
            _projectService.Delete(project);
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(Guid id)
        {
            return _projectService.GetProject(id) != null;
        }

        public IActionResult ScheduleProject(Guid id)
        {
            var TimeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault().ToString();
            var project = _projectService.GetProject(id);
            var events = GetAllEvents().Where(z =>
            {
                if (z.Start.dateTime.ToString() == "01.1.0001 00:00:00")
                {
                    return z.Start.date <= project.Deadline && z.Start.date >= DateTime.Now;
                }
                else if (z.Start.date.ToString() == "01.1.0001. 00:00:00")
                {
                    return z.Start.dateTime <= project.Deadline && z.Start.dateTime >= DateTime.Now;
                }
                return true;

            }).ToList();
            var message = this.ProjectScheduled(project, events) ? "Success" : "Failed to update, please check hours and dates and deadlines";
            return View("ScheduleProject", message);
        }

        private List<Event> GetAllEvents()
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

            return allEvents;

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

        private bool ProjectScheduled(Project project, List<Event> events)
        {
            var conflictingEvents = new List<Event>();
            TimeSpan time = project.Deadline - DateTime.Now;
            var dailyHours = (project.WorkEnd - project.WorkStart).Hours;
            var days = (long)Math.Ceiling(time.TotalDays);
            long availableHours = days * dailyHours;
            foreach (var item in events)
            {
                if (item.Start.date.ToString() != "01.1.0001 00:00:00") //all-day event
                {
                    availableHours -= dailyHours;
                    conflictingEvents.Add(item);
                }
                else
                {
                    if (overlap(item.Start.dateTime, item.End.dateTime,
                        new DateTime(item.Start.dateTime.Year,
                                    item.Start.dateTime.Month,
                                    item.Start.dateTime.Day,
                                    project.WorkStart.Hour,
                                    project.WorkStart.Minute,
                                    project.WorkStart.Second),
                        new DateTime(item.End.dateTime.Year,
                                    item.End.dateTime.Month,
                                    item.End.dateTime.Day, project.WorkEnd.Hour,
                                    project.WorkEnd.Minute,
                                    project.WorkEnd.Second)))
                    {
                        conflictingEvents.Add(item);
                    }
                }

                if (availableHours < project.Hours || availableHours <= 0)
                {
                    return false;
                }
            }
           return CreateEvents(project, conflictingEvents);
        }

        private bool CreateEvents(Project project, List<Event> conflictingEvents)
        {
            var eventsToAdd = new List<Event>();
            long hoursLeft = project.Hours;

            for (DateTime currentDate = DateTime.Now; currentDate < project.Deadline && hoursLeft > 0; currentDate = currentDate.AddDays(1))
            {
                Event conflictingEvent = null;

                // If today's free time slot for projects has passed, continue to tomorrow
                if(currentDate.Date == project.WorkStart.Date && currentDate >= project.WorkStart)
                {
                    continue;
                }

                //is it saturday and is the project good for saturday?
                if (currentDate.DayOfWeek == DayOfWeek.Saturday && !project.WorkOnSaturdays)
                {
                    continue;
                }

                //is it sunday and is the project good for sunday?
                if (currentDate.DayOfWeek == DayOfWeek.Sunday && !project.WorkOnSundays)
                {
                    continue;
                }

                //all day events 
                conflictingEvent = conflictingEvents.Find(z => z.Start.date.Date == currentDate.Date);
                if (conflictingEvent != null)
                {
                    continue;
                }
                conflictingEvent = null;


                //event covers timeslot fully
                conflictingEvent = conflictingEvents.Find(z => z.Start.dateTime <= (currentDate.Date + project.WorkStart.TimeOfDay)
                                                && z.End.dateTime >= (currentDate.Date + project.WorkEnd.TimeOfDay));
                if (conflictingEvent != null)
                {
                    continue;
                }

                //event covers start of timeslot, add from end of event to end of timeslot
                conflictingEvent = conflictingEvents.Find(z => z.Start.dateTime <= (currentDate.Date + project.WorkStart.TimeOfDay)
                                      && z.End.dateTime >= currentDate.Date + project.WorkStart.TimeOfDay
                                      && z.End.dateTime < currentDate.Date + project.WorkEnd.TimeOfDay);
                if (conflictingEvent != null)
                {
                    var newEvent = new Event()
                    {
                        Summary = project.Title,
                        Description = project.Description,
                        Start = new EventDateTime()
                        {
                            dateTime = conflictingEvent.End.dateTime
                        },
                        End = new EventDateTime()
                        {
                            dateTime = (currentDate.Date + project.WorkEnd.TimeOfDay)
                        }
                    };
                    hoursLeft -= newEvent.End.dateTime.Hour - newEvent.Start.dateTime.Hour;
                    eventsToAdd.Add(newEvent);
                    continue;
                }

                //event covers end of timeslot, add from start of timeslot till end of event
                conflictingEvent = conflictingEvents.Find(z => z.Start.dateTime >= (currentDate.Date + project.WorkStart.TimeOfDay)
                                     && z.Start.dateTime < currentDate.Date + project.WorkEnd.TimeOfDay
                                     && z.End.dateTime >= currentDate.Date + project.WorkEnd.TimeOfDay);
                if (conflictingEvent != null)
                {
                    var newEvent = new Event()
                    {
                        Summary = project.Title,
                        Description = project.Description,
                        Start = new EventDateTime()
                        {
                            dateTime = (currentDate.Date + project.WorkStart.TimeOfDay)
                        },
                        End = new EventDateTime()
                        {
                            dateTime = conflictingEvent.End.dateTime
                        }
                    };
                    hoursLeft -= newEvent.End.dateTime.Hour - newEvent.Start.dateTime.Hour;
                    eventsToAdd.Add(newEvent);
                    continue;
                }

                //event is in the timeslot but more than start less than end, Create TWO events before and after conflicting event
                conflictingEvent = conflictingEvents.Find(z => z.Start.dateTime > (currentDate.Date + project.WorkStart.TimeOfDay)
                                    && z.End.dateTime < currentDate.Date + project.WorkEnd.TimeOfDay);
                if (conflictingEvent != null)
                {
                    var newEvent = new Event()
                    {
                        Summary = project.Title,
                        Description = project.Description,
                        Start = new EventDateTime()
                        {
                            dateTime = (currentDate.Date + project.WorkStart.TimeOfDay)
                        },
                        End = new EventDateTime()
                        {
                            dateTime = conflictingEvent.Start.dateTime
                        }
                    };
                    hoursLeft -= newEvent.End.dateTime.Hour - newEvent.Start.dateTime.Hour;
                    eventsToAdd.Add(newEvent);


                    var newEvent2 = new Event()
                    {
                        Summary = project.Title,
                        Description = project.Description,
                        Start = new EventDateTime()
                        {
                            dateTime = conflictingEvent.End.dateTime
                        },
                        End = new EventDateTime()
                        {
                            dateTime = (currentDate.Date + project.WorkEnd.TimeOfDay)
                        }
                    };
                    hoursLeft -= newEvent2.End.dateTime.Hour - newEvent2.Start.dateTime.Hour;
                    eventsToAdd.Add(newEvent2);
                    continue;
                }


                //timeslot is fully empty, fill it with event
                else
                {
                    var newEvent = new Event()
                    {
                        Summary = project.Title,
                        Description = project.Description,
                        Start = new EventDateTime()
                        {
                            dateTime = (currentDate.Date + project.WorkStart.TimeOfDay)
                },
                        End = new EventDateTime()
                        {
                            dateTime = (currentDate.Date + project.WorkEnd.TimeOfDay)
                        }
                    };
                    hoursLeft -= newEvent.End.dateTime.Hour - newEvent.Start.dateTime.Hour;
                    eventsToAdd.Add(newEvent);
                }

                conflictingEvent = null;

            }

            return CreateEventsOnCalendar(eventsToAdd);
        }


        private bool CreateEventsOnCalendar(List<Event> events)
        {
            var temp = true;
            foreach (Event item in events)
            {
                temp = temp && CreateCalendarEvent(item);
            }

            return temp;
        }


        private bool CreateCalendarEvent(Event eventToAdd)
        {
            var TimeZone = DateTimeZoneProviders.Tzdb.GetSystemDefault().ToString();
            eventToAdd.Start.TimeZone = TimeZone;
            eventToAdd.End.TimeZone = TimeZone;
            var eventDto = new EventDTO()
            {
                Summary = eventToAdd.Summary,
                Description = eventToAdd.Description,
                Start = new EventDateTimeString()
                {
                    timeZone = TimeZone,
                    dateTime = eventToAdd.Start.dateTime.ToString()
                },
                End = new EventDateTimeString()
                {
                    timeZone = TimeZone,
                    dateTime = eventToAdd.End.dateTime.ToString()
                }

            };
            var tokenFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "token.json");
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFilePath));
            var calendarId = this.AllCalendarsList()[0].Id.Replace("#", "%23").Replace("@", "%40");

            var keyPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "apikey.json");
            var apiKey = JObject.Parse(System.IO.File.ReadAllText(keyPath))["key"].ToString();

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            eventDto.Start.dateTime = DateTime.Parse(eventDto.Start.dateTime).ToString("yyyy-MM-dd'T'HH:mm:ss");
            eventDto.End.dateTime = DateTime.Parse(eventDto.End.dateTime).ToString("yyyy-MM-dd'T'HH:mm:ss");

            var model = JsonConvert.SerializeObject(eventDto, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            request.AddQueryParameter("key", apiKey);
            request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application.json");
            request.AddParameter("application/json", model, ParameterType.RequestBody);

            restClient.BaseUrl = new System.Uri("https://www.googleapis.com/calendar/v3/calendars/" + calendarId + "/events");

            var response = restClient.Post(request);

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            return false;


        }


        private bool overlap(DateTime startA, DateTime endA, DateTime startB, DateTime endB)
        {
            //(StartA <= EndB) and (EndA >= StartB)
            return ((startA <= endB) && (endA >= startB));
        }
    }

}
