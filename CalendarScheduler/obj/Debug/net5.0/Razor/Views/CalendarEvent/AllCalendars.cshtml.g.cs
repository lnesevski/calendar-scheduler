#pragma checksum "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllCalendars.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59cda622dea2a1a49e69b28f3035b58c60223e60"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_CalendarEvent_AllCalendars), @"mvc.1.0.view", @"/Views/CalendarEvent/AllCalendars.cshtml")]
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
#line 1 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllCalendars.cshtml"
using CalendarScheduler.Domain.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59cda622dea2a1a49e69b28f3035b58c60223e60", @"/Views/CalendarEvent/AllCalendars.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f5506c28c9f8ae8aba6ba714a7fb5f85651b7f0", @"/Views/_ViewImports.cshtml")]
    public class Views_CalendarEvent_AllCalendars : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Calendar>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllCalendars.cshtml"
   
    var uri = "https://calendar.google.com/calendar/u/0/embed?height=600&wkst=1&bgcolor=%23ffffff";
    foreach(Calendar calendar in Model)
    {
        var formattedId = calendar.Id.Replace("#", "%23").Replace("@","%40");
        uri = uri + "&src=" + formattedId;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container\">\r\n    <iframe");
            BeginWriteAttribute("src", " src=\"", 386, "\"", 396, 1);
#nullable restore
#line 12 "D:\Codes\Integrated Systems\IS-Project\CalendarScheduler 2\CalendarScheduler\CalendarScheduler\Views\CalendarEvent\AllCalendars.cshtml"
WriteAttributeValue("", 392, uri, 392, 4, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"border:solid 1px #777; width:60vw; height: 70vh; margin:auto;\" frameborder=\"0\" scrolling=\"yes\"></iframe>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Calendar>> Html { get; private set; }
    }
}
#pragma warning restore 1591
