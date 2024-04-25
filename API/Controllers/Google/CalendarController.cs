using Infrastructure.Data.GoogleService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Google
{
    public class CalendarController : BaseApiController
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly IGoogleService _googleService;
        public CalendarController(ILogger<CalendarController> logger, IGoogleService googleService)
        {
            _googleService = googleService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateEvent()
        {
            var res = _googleService.CreateEvent();
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult> GetEvents()
        {
            var events = _googleService.GetEvent();
            return Ok(events.Items);
        }
    }
}