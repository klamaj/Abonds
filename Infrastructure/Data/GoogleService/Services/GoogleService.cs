using Core.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Infrastructure.Data.GoogleService.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.GoogleService.Services
{
    public class GoogleService : IGoogleService
    {
        private readonly ILogger<GoogleService> _logger;
        private readonly GoogleCalendarConfig _googleConfig;
        private readonly IConfiguration _config;
        public GoogleService(ILogger<GoogleService> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _googleConfig = ConfigurationBinder.Get<GoogleCalendarConfig>(config.GetSection("GoogleCalendarConfig"))!;
        }

        public Event CreateEvent()
        {
            Event body = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTimeDateTimeOffset = Convert.ToDateTime("2024-04-22T14:00:00");
            EventDateTime end = new EventDateTime();
            end.DateTimeDateTimeOffset = Convert.ToDateTime("2024-04-22T15:00:00");
            body.Start = start;
            body.End = end;
            body.Location = "Test Location";
            body.Summary = "fjkweioioweioweioweio";
            CalendarService service = GetCalendarService();
            EventsResource.InsertRequest request = new EventsResource.InsertRequest(service, body, _googleConfig.Calendar);
            Event response = request.Execute();
            return response;
        }

        public Events GetEvent()
        {
            CalendarService service = GetCalendarService();
            EventsResource.ListRequest request = service.Events.List(_googleConfig.Calendar);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();

            return events;
        }

        private CalendarService GetCalendarService()
        {
            try
            {
                string[] Scopes =
                {
                    CalendarService.Scope.Calendar,
                    CalendarService.Scope.CalendarEvents,
                    CalendarService.Scope.CalendarEventsReadonly
                };

                GoogleCredential credential;
                using (var stream = new FileStream(_googleConfig.Document!, FileMode.Open, FileAccess.Read))
                {
                    credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes).CreateWithUser(_googleConfig.ServiceAcc);
                }

                // Create calendar API service
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = _googleConfig.ServiceName
                });

                return service;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}