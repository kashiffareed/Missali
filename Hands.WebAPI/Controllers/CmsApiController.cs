using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Service.Cms;
using Hands.WebAPI.Models;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class CmsApiController : ApiController
    {
        // GET: CmsApi
        private ICmsService _cmsService;
        private ApiResponse respoonse;

        public CmsApiController()
        {
            _cmsService = new CmsService();
            respoonse = new ApiResponse();
        }


        /// <summary>
        /// this api is for get active all cms data 
        /// </summary>
        /// <returns></returns>
        [ProjectIdAttributeController]
        public IHttpActionResult GetAllCms(int? projectId = null)
        {
            respoonse = new ApiResponse();
            var cmsData = _cmsService.GetcontentByType(projectId);

            if (cmsData != null)
            {

                respoonse.Success = true;
                respoonse.Result = cmsData;
                return Json(respoonse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { }; 
            return Json(respoonse);
        }

        /// <summary>
        ///this api is for get content data by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [ProjectIdAttributeController]
        public IHttpActionResult GetcontentByType(string type, int? projectId = null)
        {
            var response = new ApiResponse();
            var contentData = _cmsService.GetcontentByType(projectId).Where(x => x.content_type == type).ToList();
            if (contentData.Any())
            {
                response.Success = true;
                response.Result = contentData;
                return Json(response, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { }; 
            return Json(respoonse);
        }


    }
}