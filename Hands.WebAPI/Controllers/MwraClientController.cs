using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.Mwra;
using Hands.Service.MwraClientNew;
using Hands.Service.Noor;
using Hands.Service.Products;
using Hands.Service.SessionFollowup;
using Hands.Service.SessionInventory;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;


namespace Hands.WebAPI.Controllers
{
    public class MwraClientController : ApiController
    {
        // GET: MwraClient

        private readonly IMwraClientNewService _clientNewService;
        private IMwraService _mwraService;
        private readonly ISessionInventoryService _inventoryService;
        private readonly ISessionfollowupService _sessionfollowupService;
        private readonly IProductsService _productsService; //Ovais changes

        private ApiResponse response;
        public MwraClientController()
        {
            _clientNewService = new MwraClientService();
            _mwraService = new MwraService();
            _inventoryService = new SessionInventoryService();
            _sessionfollowupService = new SessionfollowupService();
            _productsService = new ProductsService(); //Ovais changes
        }
        /// <summary>
        /// this api is for create the mwraclient and update the mwra as client by mwraid
        /// </summary> 
        /// <param name="model"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateMwraClient(List<Hands.WebAPI.Models.MwraClient> model)
        {
            if (model != null && model.Count != 0)
            {
                int skippedCount = 0;
                int insertedCount = 0;
                foreach (var mwraClient in model)
                {
                    var checkRecordoFMwra= _clientNewService.GetClientByMwraId(mwraClient.MwraId);
                    if (checkRecordoFMwra == null)
                    {
                        var mwraclient = mwraClient.MwraClientMapping();
                        mwraclient.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                        _clientNewService.Insert(mwraclient);
                        _clientNewService.SaveChanges();
                        updateMwraasClient(mwraClient.MwraId, (int) mwraClient.ProductId);
                        var sessionInventory = new SessionInventory();
                        sessionInventory.MwraClientId = mwraclient.MwraClientId; //Ovais ADDed Line//
                        sessionInventory.MwraId = mwraClient.MwraId;
                        sessionInventory.ProductId = mwraClient.ProductId;
                        sessionInventory.Quantity = mwraClient.Quantity;
                        sessionInventory.IsActive = true;
                        sessionInventory.UserId = _sessionfollowupService.GetlhvIdByMwraId(mwraClient.MwraId);
                        sessionInventory.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                        _inventoryService.Insert(sessionInventory);
                        _inventoryService.SaveChanges();
                        _inventoryService.Update(sessionInventory); //Ovais Added Line//
                        insertedCount++;
                    }
                    else
                    {
                        skippedCount++;
                    }
                }
                response = new ApiResponse();
                response.Success = true;
                response.Result = "record inserted"+" "+ insertedCount + " " +"record skipped"+" " +skippedCount;
                response.Result = new {insertedCount,  skippedCount};
                return Json(response);
            }
            response = new ApiResponse();
            response.Success = false;
            response.Result = null;
            response.Message = "Invalid Request";
            return Json(response);

        }

        /// <summary>
        /// update mawra as client by mwraId 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void updateMwraasClient(int id,int productId)
        {
            Data.HandsDB.Mwra existingModel = _mwraService.GetById(id);
            existingModel.IsClient = 1;
        
            //existingModel.IsUserFp = "yes";
            //existingModel.NameOfFp = _productsService.GetById(productId).ProductName;
           
            _mwraService.Update(existingModel);
            _mwraService.SaveChanges();
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetMwraClientFormbyId(int id)
        {
            response = new ApiResponse();
            var clientByMwraId = _clientNewService.GetClientByMwraId(id);
            if (clientByMwraId != null)
            {
                response.Success = true;
                response.Result = clientByMwraId;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);


        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetMwraClientFormbyMwraId(int id)
        {
            response = new ApiResponse();
            var clientByMwraId = _clientNewService.GetMwraClientByMwraId(id);

            if (clientByMwraId != null)
            {

                response.Success = true;
                response.Result = clientByMwraId;
                return Json(response);

            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);

        }


    }
}