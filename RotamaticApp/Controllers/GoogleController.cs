using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using RotamaticApp.Models;
using Google.Apis.Calendar.v3.Data;

namespace RotamaticApp.Controllers
{
    public class GoogleController : ApiController
    {
        [HttpGet]
        public List<FullCalendarEvent> GetAll()
        {
            List<FullCalendarEvent> eventsToDisplay = new List<FullCalendarEvent>();

            try
            {
                String serviceAccountEmail = "1032688373753-e500pi2clnfjdg0ih14dm5bkcu5jfjef@developer.gserviceaccount.com";

                var certificate = new X509Certificate2(@"ServiceAccountPrivateKey.p12", "notasecret", X509KeyStorageFlags.Exportable);

                ServiceAccountCredential credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(serviceAccountEmail)
                   {
                       Scopes = new[] { CalendarService.Scope.Calendar, CalendarService.Scope.CalendarReadonly },
                       User = "testrotamatic@gmail.com",
                   }.FromCertificate(certificate));

                HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);

                // Create the service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Calendar API Sample",
                });


                EventsResource.ListRequest listRequest = service.Events.List("testrotamatic@gmail.com");
                listRequest.SingleEvents = true;
                var calEvents = listRequest.Execute();

                foreach (Event e in calEvents.Items)
                {
                    FullCalendarEvent fcEvent = new FullCalendarEvent()
                    {
                        Id = e.Id,
                        Title = e.Summary,
                        Start = e.Start.DateTime,
                        End = e.End.DateTime,
                        AllDay = (e.End.DateTime == null) ? true : false
                    };
                    eventsToDisplay.Add(fcEvent);
                }
            }
            catch (System.Net.Http.HttpRequestException)
            {
                for (int i = 0; i < 7; i++)
                {
                    FullCalendarEvent fcEvent = new FullCalendarEvent()
                    {
                        Id = i.ToString(),
                        Title = "No Internet " + i,
                        Start = DateTime.Now.AddDays(i),
                        End = DateTime.Now.AddDays(i),
                        AllDay = false
                    };
                    eventsToDisplay.Add(fcEvent);
                }
            }

            return eventsToDisplay;
        }
    }
}
