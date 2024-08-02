using System.Linq;
using System.Web.Http;
using Hands.Service.Pms;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class PmsApiController : ApiController
    {
        // GET: PmsApi

        private IPmsService _pmsService;
        private ApiResponse respoonse;

        public PmsApiController()
        {
            _pmsService = new PmsService();
            respoonse = new ApiResponse();
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetAllPms(int? projectId = null)
        {
            respoonse = new ApiResponse();
            var pmsData = _pmsService.GetAllActive().Where(x=>x.ProjectId== projectId).OrderByDescending(x=>x.ContentId);

            if (pmsData.Any())
            {
               
                respoonse.Success = true;
                respoonse.Result = pmsData;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetAllContentPms(int? projectId = null)
        {

            var pmsData = _pmsService.GetAllContentPms(projectId);

            if (pmsData != null)
            {
                respoonse = new ApiResponse();
                respoonse.Success = true;
                respoonse.Result = pmsData;
                return Json(respoonse);
            }

            return NotFound();
        }
    }
}