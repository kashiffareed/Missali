using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.ClientChecklist;
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
    public class ClientChecklistController : ApiController
    {
        private IClientChecklistService _clientChecklistService;
        public ClientChecklistController()
        {
            _clientChecklistService = new ClientChecklistService();
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateClientChecklist(List<Hands.WebAPI.Models.ClientChecklist> model)
        {
            var response = new ApiResponse();
            if (model != null && model.Count != 0)
            {
                foreach (var clientChecklist in model)
                {
                    if (ModelState.IsValid)
                    {
                        var entity = clientChecklist.GetMapclientChecklist();
                        entity.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                        entity.CreatedAt = DateTime.Now;
                        _clientChecklistService.Insert(entity);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                _clientChecklistService.SaveChanges();
                response.Success = true;
                response.Result = null;
                return Json(response);
            }
            response.Success = false;
            response.Result = null;
            response.Message = "Invalid Request";
            return Json(response);
        }

        //[ProjectIdAttributeController]
        public IHttpActionResult GetClientCheckList()
        {
            var response = new ApiResponse();
            if (ModelState.IsValid)
            {
                var entity = _clientChecklistService.GetById(1);
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