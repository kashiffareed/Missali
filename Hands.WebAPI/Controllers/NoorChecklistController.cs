using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Mwra;
using Hands.Service.Noor;
using Hands.Service.NoorChecklist;
using Hands.Service.Real;
using Hands.ViewModels.Models.RealTimeCheckList;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class NoorChecklistController : ApiController
    {
        private readonly INoorChecklistService _noorChecklistService;
        public NoorChecklistController()
        {
            _noorChecklistService = new NoorChecklistService();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateNoorChecklist(List<Hands.WebAPI.Models.NoorChecklist> model)
        {
            if (model != null && model.Count != 0)
            {


                foreach (var checklist in model)
                {
                    if (ModelState.IsValid)
                    {
                        var noorChecklist = checklist.GetMapclientNoorChecklist();
                        noorChecklist.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                        _noorChecklistService.Insert(noorChecklist);
                    }
                    else
                    {

                        return BadRequest(ModelState);
                    }

                }

                _noorChecklistService.SaveChanges();
                var response = new ApiResponse();
                response.Success = true;
                response.Result = null;
                return Json(response);
            }
            var responses = new ApiResponse();
            responses.Success = false;
            responses.Result = null;
            responses.Message = "Invalid Request";
            return Json(responses);


        }
        //[ProjectIdAttributeController]
        public IHttpActionResult GetnoorCheckList()
        {
            var response = new ApiResponse();
            if (ModelState.IsValid)
            {
                var entity = _noorChecklistService.GetById(1);
           
                response.Success = true;
                response.Result = entity;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }

    }
}