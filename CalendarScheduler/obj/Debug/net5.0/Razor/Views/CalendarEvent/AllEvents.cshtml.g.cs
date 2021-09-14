#pragma checksum "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5aef4ba7bd5c072d4b2508c9aff83455b9689c0a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CalendarEvent_AllEvents), @"mvc.1.0.view", @"/Views/CalendarEvent/AllEvents.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\_ViewImports.cshtml"
using CalendarScheduler;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
using CalendarScheduler.Domain.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5aef4ba7bd5c072d4b2508c9aff83455b9689c0a", @"/Views/CalendarEvent/AllEvents.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f5506c28c9f8ae8aba6ba714a7fb5f85651b7f0", @"/Views/_ViewImports.cshtml")]
    public class Views_CalendarEvent_AllEvents : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Event>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
 if (Model.Count != 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"container-fluid\">\r\n");
#nullable restore
#line 7 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
         foreach (Event calendarEvent in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card col-md-8\">\r\n\r\n                <div>\r\n                    <div class=\"card-body\">\r\n                        <h5 class=\"card-title\">");
#nullable restore
#line 13 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                          Write(calendarEvent.Summary);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                        <ul class=\"list-group list-group-flush\">\r\n                            <li class=\"list-group-item\">\r\n                                <p>");
#nullable restore
#line 16 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                              Write(calendarEvent.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </li>\r\n\r\n                            <li class=\"list-group-item\">\r\n");
#nullable restore
#line 20 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                 if (calendarEvent.Description != null && (calendarEvent.Description.Contains("holiday") || calendarEvent.Description.Contains("Observance")))
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <h6 class=\"h6\">All-day event</h6>\r\n                                    <p>Date: ");
#nullable restore
#line 23 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                        Write(calendarEvent.Start.date);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 24 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <p>Start: ");
#nullable restore
#line 27 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                         Write(calendarEvent.Start.dateTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 27 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                                                       Write(calendarEvent.Start.TimeZone);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                    <p>End: ");
#nullable restore
#line 28 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                       Write(calendarEvent.End.dateTime);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 28 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                                                   Write(calendarEvent.End.TimeZone);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 29 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </li>\r\n                        </ul>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 35 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n");
#nullable restore
#line 37 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        No events found\r\n    </div>\r\n");
#nullable restore
#line 43 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllEvents.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Event>> Html { get; private set; }
    }
}
#pragma warning restore 1591
