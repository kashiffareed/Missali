using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.SessionFollowup;
using Hands.Service.SessionInventory;

using Hands.Service.ApiDuplicateRecordLogs;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class SessionFollowupController : ApiController
    {
        // GET: SessionFollowup

        private ISessionfollowupService _sessionfollowupService;

        private ApiResponse response;
        private readonly ISessionInventoryService _inventoryService;
        private readonly IApiDuplicateRecordLogsService _apiDuplicateRecordLogsService;

        public SessionFollowupController()
        {
            _sessionfollowupService = new SessionfollowupService();
            _inventoryService = new SessionInventoryService();
            _apiDuplicateRecordLogsService = new ApiDuplicateRecordLogsService();



        }
        /// <summary>
        /// this api is for create/ post session followup
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateSessionFollowup(List<Hands.ViewModels.Models.SessioFolowup.SessionFollowup> model)
        {
            if (model != null && model.Count != 0)
            {
                foreach (var followup in model)
                {
                    var sessionFollowup = followup.SessionFollowupMapping();
                    sessionFollowup.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;


                    var isDuplicated = _sessionfollowupService.isSessionFollowupDuplicated(sessionFollowup);

                    if (isDuplicated)
                    {
                        var apiDuplicateRecordLog = new ApiDuplicateRecordLog();

                        apiDuplicateRecordLog.RequestUrl = Request.RequestUri.AbsolutePath;
                        apiDuplicateRecordLog.JsonString = JsonConvert.SerializeObject(sessionFollowup);
                        apiDuplicateRecordLog.CreatedDate = DateTime.Now;
                        apiDuplicateRecordLog.IsActive = true;
                        _apiDuplicateRecordLogsService.Insert(apiDuplicateRecordLog);
                        _apiDuplicateRecordLogsService.SaveChanges();

                    }
                    else
                    {
                        _sessionfollowupService.Insert(sessionFollowup);

                        var sessionInventory = new SessionInventory();
                        sessionInventory.MwraId = followup.MwraId;
                        sessionInventory.ProductId = followup.ProductId;
                        sessionInventory.Quantity = followup.Quantity;
                        sessionInventory.IsActive = true;
                        sessionInventory.UserId = followup.LhvId;
                        _inventoryService.Insert(sessionInventory);

                    }

                }

                _sessionfollowupService.SaveChanges();
                _inventoryService.SaveChanges();
                response = new ApiResponse();
                response.Success = true;
                response.Result = model;
                return Json(response);
            }
            response = new ApiResponse();
            response.Success = false;
            response.Result = model;
            response.Message = "Invalid Request";
            return Json(response);

        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetFollowupdata(int? LhvId = null, int? projectId = null)
        {
            response = new ApiResponse();
            var followupsData = _sessionfollowupService.GetAllFollowupsByLhv(LhvId,projectId);
            if (followupsData != null)
            {
              
                response.Success = true;
                response.Result = followupsData;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetFollowUpbyMwraId(int id)
        {
            response = new ApiResponse();
            var follupByMwraIdData = _sessionfollowupService.GetBymwraId(id);
            if (follupByMwraIdData != null)
            {
                response.Success = true;
                response.Result = follupByMwraIdData;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);

        }
    }
}