using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Service.Taluqa;
using Hands.WebAPI.Models;

namespace Hands.WebAPI.Controllers
{
    public class TaluqaController : ApiController
    {
        // GET: Taluqa
        private readonly ITaluqaService _taluqaService;
        private ApiResponse response;

        public TaluqaController()
        {
            _taluqaService = new TaluqaService();
            response = new ApiResponse();
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetAllTaluqa(int? projectId = null)
        {
            response = new ApiResponse();
            var taluqas = _taluqaService.GetAllActive().Where(x=>x.ProjectId== projectId).OrderByDescending(x=>x.TaluqaId);

            if (taluqas.Any())
            {
              
                response.Success = true;
                response.Result = taluqas;
                return Json(response);

            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);

        }
    }
}