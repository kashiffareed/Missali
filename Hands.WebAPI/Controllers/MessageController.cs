using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Service.Massage;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class MessageController : ApiController
    {
        // GET: Message'
        private readonly IMassageService _massageService;

        public MessageController()
        {
            _massageService = new MassageService();
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetMessages(int? projectId = null)
        {
            var response = new ApiResponse();
            var messagedata = _massageService.GetAllActive().Where(x=>x.ProjectId== projectId).OrderByDescending(x => x.MessageId);

            if (messagedata.Any())
            {

                response.Success = true;
                response.Result = messagedata;
                response.TotalRecords = messagedata.Count();
                return Json(response);
            }
            response.Result= new string[] { };
            response.Success = true;
            return Json(response);
        }
    }
}