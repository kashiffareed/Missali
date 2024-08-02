using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Service.Events;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class EventsController : ApiController
    {
        // GET: Events
        private readonly IEventService _eventService;
        private ApiResponse _apiResponse;

        public EventsController()
        {
            _eventService = new EventService();
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetAllEvents(int? projectId = null)
        {
            _apiResponse = new ApiResponse();
            var eventsData = _eventService.GetAllActive().Where(x=>x.ProjectId== projectId).OrderByDescending(x => x.EventId);
            if (eventsData.Any())
            {

                _apiResponse.Success = true;
                _apiResponse.Result = eventsData;
                return Json(_apiResponse);
            }
            _apiResponse.Success = true;
            _apiResponse.Result = new string[] { };
            return Json(_apiResponse);
        }
    }
}