using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.Session;
using Hands.Service.Sessioncontent;
using Hands.Service.SessionFollowup;
using Hands.Service.SessionInventory;
using Hands.Service.ApiDuplicateRecordLogs;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class SessionController : ApiController
    {

        //private readonly ISessionService _sessionService;
        private ApiResponse response;
        private readonly ISessionContentService _sessionContentService;
        private readonly ISessionMwraService _sessionMwraService;
        private readonly ISessionInventoryService _inventoryService;
        private readonly ISessionfollowupService _sessionfollowupService;
        private readonly IApiDuplicateRecordLogsService _apiDuplicateRecordLogsService;

        private SessionVM _sessionVM;
        private AppUserService _appService;

        //NewOvais
        private ISessionService _sessionService;
        //NewOvais
        public SessionController()
        {
            _sessionService = new SessionService();
            _sessionContentService = new SessionContentService();
            _sessionMwraService = new SessionMwraService();
            _inventoryService = new SessionInventoryService();
            _sessionfollowupService = new SessionfollowupService();
            _sessionVM = new SessionVM();
            _appService = new AppUserService();
            _apiDuplicateRecordLogsService = new ApiDuplicateRecordLogsService();
        }

        // GET: Session


        /// <summary>
        /// this api is for get the session data by  lhvid , startdate , enddate or next session date
        /// </summary>
        /// <param name="appuserId"></param>
        /// <param name="startDateTime"></param>
        /// <param name="enDateTime"></param>
        /// <param name="nextsessionDateTime"></param>
        /// <returns></returns>
        [ProjectIdAttributeController]
        public IHttpActionResult GetSession(int? appuserId, int? projectId = null, DateTime? startDateTime = null, DateTime? enDateTime = null, DateTime? nextsessionDateTime = null, int pageNo = 1, int pageSize = int.MaxValue)
        {
            response = new ApiResponse();
            var sessionData = _sessionService.GetSessionsData(appuserId, projectId, startDateTime, enDateTime, nextsessionDateTime, out var totalRecords, pageNo, pageSize);
            if (sessionData != null)
            {

                response.Success = true;
                response.TotalRecords = totalRecords;
                response.Result = _sessionVM.PrepareViewList(sessionData, _sessionService, _appService).OrderByDescending(x => x.SessionId);
                return Json(response, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }
        /// <summary>
        /// this api is for create/post the session 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHttpActionResult CreateSession(List<Hands.ViewModels.Models.SessionMwraWithContent.SessionMwraWithContent> model)
        {
            if (model != null && model.Count != 0)
            {
                foreach (var sessionMwraWithContent in model)
                {

                    var session = sessionMwraWithContent.SessionMapping();
                    session.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                    if (sessionMwraWithContent.SessionMwra.Count > 1)
                    {
                        session.IsGroup = 1;
                    }

                    var isDuplicated = _sessionService.isSessionDuplicated(session);

                    if (isDuplicated)
                    {
                        var apiDuplicateRecordLog = new ApiDuplicateRecordLog();

                        apiDuplicateRecordLog.RequestUrl = Request.RequestUri.AbsolutePath;
                        apiDuplicateRecordLog.JsonString = JsonConvert.SerializeObject(sessionMwraWithContent);
                        apiDuplicateRecordLog.CreatedDate = DateTime.Now;
                        apiDuplicateRecordLog.IsActive = true;
                        _apiDuplicateRecordLogsService.Insert(apiDuplicateRecordLog);
                        _apiDuplicateRecordLogsService.SaveChanges();

                    }
                    else
                    {
                        _sessionService.Insert(session);
                        _sessionService.SaveChanges();
                        foreach (var sessionMwra in sessionMwraWithContent.SessionMwra)
                        {
                            sessionMwra.SessionId = session.SessionId;
                            sessionMwra.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                            sessionMwra.MwraCount = sessionMwra.MwraCount;
                            _sessionMwraService.Insert(sessionMwra);
                        }

                        foreach (var sessionContent in sessionMwraWithContent.SessionContent)
                        {
                            sessionContent.SessionId = session.SessionId;
                            sessionContent.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                            _sessionContentService.Insert(sessionContent);
                        }

                        foreach (var sessionInventory in sessionMwraWithContent.SessionInventory)
                        {
                            var sessionInventoryDb = new SessionInventory();
                            sessionInventoryDb.SessionId = session.SessionId;
                            sessionInventoryDb.UserId = sessionInventory.UserId;
                            sessionInventoryDb.MwraId = sessionInventory.MwraId;
                            sessionInventoryDb.Quantity = sessionInventory.Quantity;
                            sessionInventoryDb.ProductId = sessionInventory.ProductId;
                            sessionInventoryDb.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                            //sessionInventoryDb.ProjectId = sessionInventory.ProjectId;
                            sessionInventoryDb.IsActive = true;
                            _inventoryService.Insert(sessionInventoryDb);
                            _inventoryService.SaveChanges();
                        }

                        if (sessionMwraWithContent.SessionFollowup != null)
                        {
                            var sessionFollowup = sessionMwraWithContent.SessionFollowup.SessionFollowupMapping();
                            sessionFollowup.SessionId = session.SessionId;
                            sessionFollowup.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                            _sessionfollowupService.Insert(sessionFollowup);
                            _sessionfollowupService.SaveChanges();
                            var sessionInventory = new SessionInventory();
                            sessionInventory.MwraId = sessionMwraWithContent.SessionFollowup.MwraId;
                            sessionInventory.ProductId = sessionMwraWithContent.SessionFollowup.ProductId;
                            sessionInventory.Quantity = sessionMwraWithContent.SessionFollowup.Quantity;
                            sessionInventory.IsActive = true;
                            sessionInventory.UserId = sessionMwraWithContent.SessionFollowup.LhvId;
                            sessionInventory.SessionId = session.SessionId;
                            sessionInventory.FollowupId = sessionFollowup.SessionFollowupId;
                            sessionInventory.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                            //sessionInventory.ProjectId = sessionMwraWithContent.ProjectId;
                            _inventoryService.Insert(sessionInventory);
                        }

                        _sessionMwraService.SaveChanges();
                        _sessionContentService.SaveChanges();
                        _inventoryService.SaveChanges();
                    }
                   
                }

                response = new ApiResponse();
                response.Success = true;
                response.Result = null;
                return Json(response);
            }
            response = new ApiResponse();
            response.Success = false;
            response.Result = null;
            response.Message = "Invalid Requset";
            return Json(response);
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetSessionData(int? projectId = null)
        {
            response = new ApiResponse();
            var sessionData = _sessionService.GetMwraSessions(projectId);
            if (sessionData != null)
            {

                response.Success = true;
                response.Result = sessionData;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetSessionDatawithOthers(int? projectId = null)
        {
            response = new ApiResponse();
            var sessionData = _sessionService.GetMwraSessions(projectId);
            if (sessionData != null)
            {
                response.Success = true;
                response.Result = sessionData;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetFutureSessionData(int UserId, DateTime nextsessionDateTime, int? projectId = null)
        {
            response = new ApiResponse();
            var sessionData = _sessionService.GetSessionsFutureData(UserId, nextsessionDateTime, projectId);
            if (sessionData != null)
            {
                response.Success = true;
                response.Result = sessionData;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }
    }
}