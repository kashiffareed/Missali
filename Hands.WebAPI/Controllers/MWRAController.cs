using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.ApiErrorLog;
using Hands.Service.AppUser;
using Hands.Service.Mwra;
using Hands.Service.MwraClientNew;
using Hands.Service.Noor;
using Hands.Service.Products;
using Hands.Service.Regions;
using Hands.Service.SessionFollowup;
using Hands.Service.SessionInventory;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;
using Hands.Service.ApiDuplicateRecordLogs;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using AppUser = Hands.WebAPI.Models.AppUser;
using Mwra = Hands.WebAPI.mwras.Mwra;

namespace Hands.WebAPI.Controllers
{
    public class MWRAController : ApiController
    {
        // GET: Noor
        private readonly IAppUserService _userService;
        private readonly IMwraService _mwraService;
        private readonly IRegionService _regionService;
        private readonly ITaluqaService _taluqaService;
        private readonly IUnionCouncilService _unionCouncilService;
        private readonly IMwraClientNewService _clientNewService;
        private readonly ISessionfollowupService _sessionfollowupService;
        private readonly ISessionInventoryService _inventoryService;
        private readonly IProductsService _productsService;
        private readonly IApiErrorLogsServices _apiErrorLogsServices;
        private readonly IApiDuplicateRecordLogsService _apiDuplicateRecordLogsService;
        private ApiResponse respoonse;
        private Mwra _mwra;

        public MWRAController()
        {
            _mwraService = new MwraService();
            respoonse = new ApiResponse();
            _mwra = new Mwra();
            _userService = new AppUserService();
            _regionService = new RegionService();
            _taluqaService = new TaluqaService();
            _unionCouncilService = new UnionCouncilService();
            _clientNewService = new MwraClientService();
            _sessionfollowupService = new SessionfollowupService();
            _inventoryService = new SessionInventoryService();
            _productsService = new ProductsService();
            _apiErrorLogsServices = new ApiErrorLogsServices();
            _apiDuplicateRecordLogsService = new ApiDuplicateRecordLogsService();
        }

        /// <summary>
        /// get mwraby mwraId
        /// </summary>
        /// <param name="MWRAId"></param>
        /// <returns></returns>
        [ProjectIdAttributeController]
        public IHttpActionResult GetMWRAById(Int32 MWRAId)
        {
            respoonse = new ApiResponse();
            var mwraData = _mwraService.GetMwraById(MWRAId); //_mwraService.GetMwraByIdWithNames(MWRAId);

            if (mwraData != null && mwraData.IsActive)
            {
                respoonse = new ApiResponse();
                _mwra = _mwra.GetMapMwra(mwraData, _userService, _regionService, _taluqaService, _unionCouncilService, _clientNewService);

                respoonse.Success = true;
                respoonse.Result = _mwra;
                return Json(respoonse);
            }

            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }

        /// <summary>
        /// this api is for get the mwra by marviId is or lhvId
        /// </summary>
        /// <param name="marviId"></param>
        /// <param name="lhvId"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [ProjectIdAttributeController]
        public IHttpActionResult GetMwra(int? marviId = null, int? lhvId = null, int? projectId =null, int pageNo = 1,
            int pageSize = int.MaxValue)
        {
            respoonse = new ApiResponse();
            var mwrasData = _mwraService.GetMwras(marviId, lhvId, projectId, out var totalRecords, pageNo, pageSize);
            if (mwrasData != null)
            {

                respoonse.Success = true;
                respoonse.Result = _mwra.ListMwrasWithClientform(mwrasData.OrderByDescending(x => x.mwra_id),_clientNewService);
                respoonse.TotalRecords = totalRecords;
                return Json(respoonse);
            }

            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);

        }

        /// <summary>
        /// get All mawra by marvi id
        /// </summary>
        /// <param name="marviId"></param>
        /// <returns></returns>
        [ProjectIdAttributeController]
        public IHttpActionResult GetMwraByMarviId(int marviId)
        {
            respoonse = new ApiResponse();
            var mwrasData = _mwraService.GetMwrasBymarviId(marviId);
            if (mwrasData != null)
            {

                respoonse.Success = true;
                respoonse.Result = _mwra.PrepareViewList(mwrasData.OrderByDescending(x => x.MwraId), _userService,
                    _regionService, _taluqaService, _unionCouncilService, _clientNewService);
                return Json(respoonse);
            }

            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetMwraclientByLhvId(int? lhvId, int? projectId = null)
        {
            respoonse = new ApiResponse();
            if (lhvId.HasValue)
            {
                var mwrasData = _mwraService.GetMwrasByLhvID(lhvId, projectId).OrderByDescending(x => x.mwra_id);
                respoonse.Success = true;
                respoonse.Result = mwrasData;
                return Json(respoonse);
            }

            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetMwraclientByAppUserId(int? appUserId, int? projectId = null)
        {
            respoonse = new ApiResponse();
            if (appUserId.HasValue)
            {
                var mwrasData1 = _mwraService.GetMwrasByLhvID(appUserId, projectId).OrderByDescending(x => x.mwra_id);
                respoonse.Success = true;
                respoonse.Result = mwrasData1;
                return Json(respoonse);
            }

            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }


        [System.Web.Http.HttpPost]
        public IHttpActionResult Create(List<Hands.ViewModels.Models.Mwra> model)
        {
            if (model != null && model.Count != 0)
            {


                foreach (var Mwra in model)
                {

                    var mwra = Mwra.GetMwraEntity();
                    mwra.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                    _mwraService.Insert(mwra);

                }

                _mwraService.SaveChanges();
                respoonse = new ApiResponse();
                respoonse.Success = true;
                respoonse.Result = null;
                return Json(respoonse);
            }

            respoonse = new ApiResponse();
            respoonse.Success = false;
            respoonse.Result = null;
            respoonse.Message = "Invalid Requset";
            return Json(respoonse);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetmwraClients(int appUserId) /// need to add appUserId
        {
            respoonse = new ApiResponse();
            var mwras = _mwraService.GetAllMwraClientWithRelationNames();
            //var mwrwaDAta = _mwra.PrepareViewList(mwras, _clientNewService).OrderByDescending(mwra => mwra.MwraId);

            if (mwras.Any())
            {
                respoonse.Success = true;
                respoonse.Result = mwras;
                return Json(respoonse);
            }

            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetMwraClient(int? marviId = null, int? lhvId = null,int? projectId=null ,int pageNo = 1,
            int pageSize = int.MaxValue)
        {
            respoonse = new ApiResponse();
            var mwrasData = _mwraService.GetMwrasClient(marviId, lhvId, projectId, out var totalRecords, pageNo, pageSize);
            if (mwrasData != null)
            {
                respoonse.Success = true;
                respoonse.Result = mwrasData;
                respoonse.TotalRecords = totalRecords;
                return Json(respoonse);
            }

            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);

        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult DropMwraClient(List<Hands.ViewModels.Models.DropOutClient> model)
        {
            if (model != null && model.Count != 0)
            {
                foreach (var mwra in model)
                {
                    try
                    {
                        Data.HandsDB.Mwra existingModel = _mwraService.GetById(mwra.mwraId);
                        existingModel.IsClient = 0;
                        existingModel.IsActive = false;
                        existingModel.DropoutDate = mwra.dropOutDate;
                        existingModel.DropoutReason = mwra.dropOutReason;

                        _mwraService.Update(existingModel);
                        _mwraService.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        string output = JsonConvert.SerializeObject(mwra);

                        ApiErrorLog apiErrorLog = new ApiErrorLog();

                        apiErrorLog.ObjectContent = output;
                        apiErrorLog.ObjectName = "Drop Out Mwra";
                        apiErrorLog.CreatedDate = DateTime.Now;
                        apiErrorLog.IsActive = true;
                        _apiErrorLogsServices.Insert(apiErrorLog);
                        _apiErrorLogsServices.SaveChanges();
                    }
                }
                

                respoonse = new ApiResponse();
                respoonse.Success = true;
                respoonse.Message = "Mwra Drop Successfully";
                respoonse.Result = null;
                return Json(respoonse);
            }

            respoonse = new ApiResponse();
            respoonse.Success = false;
            respoonse.Result = null;
            respoonse.Message = "Invalid Requset";
            return Json(respoonse);
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateMwraWithMwraClient(List<Hands.ViewModels.Models.NewMwra> model)
        {
            if (model != null && model.Count != 0)
            {
                foreach (var Mwra in model)
                {
                    try
                    {
                        var mwraId = Mwra.MwraId;

                        if (Mwra.MwraId <= 0)
                        {
                            var mwra = Mwra.GetMwraEntity();
                            mwra.ProjectId = Request.Headers.Contains("projectId")
                                ? Request.Headers.GetValues("projectId").First().ToInt()
                                : Settings.Default.ProjectId;

                            var isDuplicated = _mwraService.isMwraDuplicated(mwra);
                            
                            if(isDuplicated)
                            {
                                var apiDuplicateRecordLog = new ApiDuplicateRecordLog();

                                apiDuplicateRecordLog.RequestUrl = Request.RequestUri.AbsolutePath;
                                apiDuplicateRecordLog.JsonString = JsonConvert.SerializeObject(Mwra);
                                apiDuplicateRecordLog.CreatedDate = DateTime.Now;
                                apiDuplicateRecordLog.IsActive = true;
                                _apiDuplicateRecordLogsService.Insert(apiDuplicateRecordLog);
                                _apiDuplicateRecordLogsService.SaveChanges();
                            }
                            else
                            {
                                _mwraService.Insert(mwra);
                                _mwraService.SaveChanges();

                                mwraId = mwra.MwraId;
                            }
                        }

                        var mwraClientNew = Mwra.clientData;

                        if (mwraClientNew != null)
                        {
                            CreateMwraClient(mwraClientNew, mwraId);
                        }
                    }
                    catch (Exception ex)
                    {
                        string output = JsonConvert.SerializeObject(Mwra);

                        ApiErrorLog apiErrorLog = new ApiErrorLog();

                        apiErrorLog.ObjectContent = output;
                        apiErrorLog.ObjectName = "Mwra With MwraClient";
                        apiErrorLog.CreatedDate = DateTime.Now;
                        apiErrorLog.IsActive = true;
                        _apiErrorLogsServices.Insert(apiErrorLog);
                        _apiErrorLogsServices.SaveChanges();
                    }
                   
                }
                respoonse = new ApiResponse();
                respoonse.Success = true;
                respoonse.Result = null;
                return Json(respoonse);
            }

            respoonse = new ApiResponse();
            respoonse.Success = false;
            respoonse.Result = null;
            respoonse.Message = "Invalid Requset";
            return Json(respoonse);
        }

        public void CreateMwraClient(Hands.WebAPI.Models.MwraClientNew mwraClient, int mwraId)
        {
            if (mwraClient != null)
            {
                //int skippedCount = 0;
                //int insertedCount = 0;

                    var checkRecordoFMwra = _clientNewService.GetClientByMwraId(mwraId);
                    if (checkRecordoFMwra == null)
                    {
                        var mwraclient = mwraClient.MwraClientMapping();
                        mwraclient.MwraId = mwraId;
                        mwraclient.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;


                        var isDuplicated = _clientNewService.isMwraClientDuplicated(mwraclient);

                        if(isDuplicated)
                        {
                            var apiDuplicateRecordLog = new ApiDuplicateRecordLog();

                            apiDuplicateRecordLog.RequestUrl = Request.RequestUri.AbsolutePath;
                            apiDuplicateRecordLog.JsonString = JsonConvert.SerializeObject(mwraClient);
                            apiDuplicateRecordLog.CreatedDate = DateTime.Now;
                            apiDuplicateRecordLog.IsActive = true;
                            _apiDuplicateRecordLogsService.Insert(apiDuplicateRecordLog);
                            _apiDuplicateRecordLogsService.SaveChanges();
                        }
                        else
                        {
                            _clientNewService.Insert(mwraclient);
                            _clientNewService.SaveChanges();
                            updateMwraasClient(mwraclient.MwraId.Value, (int)mwraClient.ProductId);
                            var sessionInventory = new SessionInventory();
                            sessionInventory.MwraClientId = mwraclient.MwraClientId; //Ovais ADDed Line//
                            sessionInventory.MwraId = mwraclient.MwraId;
                            sessionInventory.ProductId = mwraclient.ProductId;
                            sessionInventory.Quantity = mwraclient.Quantity;
                            sessionInventory.IsActive = true;
                            sessionInventory.UserId = _sessionfollowupService.GetlhvIdByMwraId(mwraclient.MwraId.Value);
                            sessionInventory.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                            _inventoryService.Insert(sessionInventory);
                            _inventoryService.SaveChanges();
                        }

                   
                    }
                    else
                    {
                        //skippedCount++;
                    }
            } 
        }

        /// <summary>
        /// update mawra as client by mwraId 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void updateMwraasClient(int id, int productId)
        {
            Data.HandsDB.Mwra existingModel = _mwraService.GetById(id);
            existingModel.IsClient = 1;
          
            //existingModel.IsUserFp = "yes";
            //existingModel.NameOfFp = _productsService.GetById(productId).ProductName;
           
            _mwraService.Update(existingModel);
            _mwraService.SaveChanges();
        }
    }
}
 