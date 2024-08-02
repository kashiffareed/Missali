using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Service.UnionCouncil;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class UnionCouncilApiController : ApiController
    {
        // GET: UnionCouncilApi

        private IUnionCouncilService _councilService;
        private ApiResponse respoonse;

        public UnionCouncilApiController()
        {
            _councilService = new UnionCouncilService();
            respoonse = new ApiResponse();
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetAllunioncouncils(int? projectId = null)
        {
            respoonse = new ApiResponse();
            var unioncouncilData = _councilService.GetAllActive().Where(x=>x.ProjectId==projectId).OrderByDescending(x=>x.UnionCouncilId);

            if (unioncouncilData.Any())
            {
              
                respoonse.Success = true;
                respoonse.Result = unioncouncilData;
                return Json(respoonse);
            
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);

        }
    }
}