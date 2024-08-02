using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Service.Session;
using Hands.Service.SessionInventory;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;


namespace Hands.WebAPI.Controllers
{
    public class SessionInventoryController : ApiController
    {
        // GET: SessionInventory
        private readonly ISessionInventoryService _inventoryService;
        private readonly ISessioncallService _sessioncallService;
        private ApiResponse response;

        public SessionInventoryController()
        {
            _inventoryService = new SessionInventoryService();
            response = new ApiResponse();
            _sessioncallService = new SessioncallService();

        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Create(List<Hands.ViewModels.Models.SessionInventory.SessionInventory> model)
        {
            if (model != null && model.Count != 0)
            {
                foreach (var sessiopnInventory in model)
                {
                    if (ModelState.IsValid)
                    {
                        Data.HandsDB.SessionInventory ModelToSAve = new Data.HandsDB.SessionInventory();
                        ModelToSAve.Id = sessiopnInventory.Id;
                        ModelToSAve.SessionId = sessiopnInventory.SessionId;
                        ModelToSAve.ProductId = sessiopnInventory.ProductId;
                        ModelToSAve.Quantity = sessiopnInventory.Quantity;
                        ModelToSAve.UserId = sessiopnInventory.UserId;
                        ModelToSAve.MwraId = sessiopnInventory.MwraId;
                        ModelToSAve.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                        //ModelToSAve.ProjectId = sessiopnInventory.ProjectId;
                        ModelToSAve.IsActive = true;
                        _inventoryService.Insert(ModelToSAve);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }


                }

                _inventoryService.SaveChanges();
                response.Success = true;
                response.Result = null;
                return Json(response);
            }
            response.Success = false;
            response.Result = new string[] { };
            response.Message = "Invalid Request";
            return Json(response);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetAllSessionInventory(int userId,int? projectId = null)
        {
            response = new ApiResponse();
            if (userId != null)
            {
                var sessionInventories = _sessioncallService.GetAllInventoryByUserId(projectId, userId).OrderByDescending(x => x.SessionId).ToList();
             
                response.Success = true;
                response.Result = sessionInventories;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }
    }
}