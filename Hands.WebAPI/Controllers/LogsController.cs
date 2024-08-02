using System.Web.Http;
using Hands.Service.Logs;
using Hands.WebAPI.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Hands.WebAPI.Controllers
{
    public class LogsController : ApiController
    {
        // GET: Logs
        private readonly ILogsService _logsService;

        public LogsController()
        {
            _logsService = new LogsService();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult PostPmsLogs(List<ViewModels.Models.Log.Log> logObject)
        {
            if (logObject != null && logObject.Count != 0)
            {


                foreach (var log in logObject)
                {
                    if (ModelState.IsValid)
                    {
                        _logsService.Insert(log.GetLogEntity());
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }

                _logsService.SaveChanges();
                var response = new ApiResponse();
                response.Success = true;
                response.Result = null;
                return Json(response);
            }
            var responses = new ApiResponse();
            responses.Success = false;
            responses.Result = null;
            responses.Message = "Invalid Requset";
            return Json(responses);

        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetPmsAnalytics(int? projectId = null)
        {
            var response = new ApiResponse();
            var pmsAnalytics = _logsService.GetAllAnalytics(projectId);

            if (pmsAnalytics != null)
            {

                response.Success = true;
                response.Result = pmsAnalytics;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);

        }

        public IHttpActionResult GetByIdfortest(int Id)
        {
            var response = new ApiResponse();
            var pmsAnalytics = _logsService.GetById(Id);
            response.Success = true;
            response.Result = pmsAnalytics;
            return Json(response);
        }
    }
}