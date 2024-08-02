using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Service.Regions;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class DistrictController : ApiController
    {
        // GET: District

        private readonly IRegionService _regionService;
        private ApiResponse respoonse;

        public DistrictController()
        {
            _regionService = new RegionService();
            respoonse = new ApiResponse();
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetAllDistricts(int? projectId = null)
        {
            respoonse = new ApiResponse();
            var districts = _regionService.GetAllActive().Where(x=>x.ProjectId==projectId).OrderByDescending(x => x.RegionsId);
            if (districts.Any())
            {
                respoonse.Success = true;
                respoonse.Result = districts;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { }; 
            return Json(respoonse);

        }
    }
}