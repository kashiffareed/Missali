using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Service.BlmisCategory;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class BlmisCategoryController : ApiController
    {
        // GET: BlmisCategory

        private IBlmisCategoryService _blmisCategoryService;
        private ApiResponse _apiResponse;

        public BlmisCategoryController()
        {
            _blmisCategoryService = new BlmisCategoryService();
            _apiResponse = new ApiResponse();
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetAllBlmisCategory()
        {
            _apiResponse = new ApiResponse();
            var blmisCategories = _blmisCategoryService.GetAllActive();
            if (blmisCategories.Any())
            {

                _apiResponse.Success = true;
                _apiResponse.Result = blmisCategories;
                return Json(_apiResponse);
            }
            _apiResponse.Success = true;
            _apiResponse.Result = new string[] { };
            return Json(_apiResponse);
        }

       
    }
}