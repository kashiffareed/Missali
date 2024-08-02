using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.LhvChecklist;
using Hands.Service.Mwra;
using Hands.Service.Noor;
using Hands.Service.NoorChecklist;
using Hands.Service.Real;
using Hands.ViewModels.Models.RealTimeCheckList;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using Newtonsoft.Json;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;

namespace Hands.WebAPI.Controllers
{
    public class LhvChecklistController : ApiController
    {
        private UnitOfWork _uow;
        private LhvChecklistService _lhvChecklistService;

        public LhvChecklistController()
        {

            _uow = new UnitOfWork(new HandsDBContext());

            _lhvChecklistService = new LhvChecklistService();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateLHVChecklist(List<Hands.WebAPI.Models.LhvChecklist> model)
        {
            if (model != null && model.Count != 0)
            {


                foreach (var lhvChecklist in model)
                {
                    if (ModelState.IsValid)
                    {
                        var lhbChecklist = lhvChecklist.GetMapLhvChecklist();
                        lhbChecklist.CreatedAt = DateTime.Now;
                        lhbChecklist.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                        _lhvChecklistService.Insert(lhbChecklist);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }

                _lhvChecklistService.SaveChanges();
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

        public IHttpActionResult GetLhvCheckList()
        {
            var response = new ApiResponse();
            if (ModelState.IsValid)
            {
                var entity = _lhvChecklistService.GetById(1);
              
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