using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Service.AppUser;
using Hands.Service.Miscellenou;
using Hands.Service.Product;
using Hands.Service.Products;
using Hands.Service.SessionInventory;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class InventoryReturnManagmentController : ApiController
    {
        // GET: InventoryReturnManagment
        private IMiscellenouService _miscellenouService;
        private readonly IProductsService _productService;
        private readonly IAppUserService _appUserService;
        private readonly ISessionInventoryService _inventoryService;
        private readonly IProductService _stock;

        public InventoryReturnManagmentController()
        {
            _miscellenouService = new MiscellenouService();
            _productService = new ProductsService();
            _inventoryService = new SessionInventoryService();
            _stock = new ProductService();

        }

        [System.Web.Http.HttpPost]
        //Create to CreateInventoryReturn //
        public IHttpActionResult CreateInventoryReturn(List<ViewModels.Models.Miscellenou.Miscellenou> model)
        {
            var respoonse = new ApiResponse();
            if (model != null && model.Count != 0)
            {
                foreach (var miscellenou in model)
                {
                    if (ModelState.IsValid)
                    {
                        Data.HandsDB.InventoryReturn modelToSAve = new Data.HandsDB.InventoryReturn();
                        Data.HandsDB.SessionInventory sessionInventory = new Data.HandsDB.SessionInventory();
                        Data.HandsDB.Stock stock = new Data.HandsDB.Stock();
                        modelToSAve.ProductId = miscellenou.ProductId;
                        modelToSAve.Quantity = miscellenou.Quantity;
                        modelToSAve.Description = miscellenou.Description;
                        modelToSAve.UserId = miscellenou.UserId;
                        modelToSAve.UserType = miscellenou.UserType;
                        modelToSAve.ReturnType = miscellenou.ReturnType;
                        modelToSAve.CreatedAt = DateTime.Now;
                        modelToSAve.IsActive = true;
                        modelToSAve.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                        _miscellenouService.Insert(modelToSAve);
                        _miscellenouService.SaveChanges();

                        ////insert inventory for substract inventory
                        sessionInventory.SessionId = null;
                        sessionInventory.ProductId = miscellenou.ProductId;
                        sessionInventory.Quantity = miscellenou.Quantity;
                        sessionInventory.UserId = miscellenou.UserId;
                        sessionInventory.ReturnId = modelToSAve.Id;
                        sessionInventory.MwraId = null;
                        sessionInventory.IsActive = true;
                        sessionInventory.CreatedDate = DateTime.Now;
                        sessionInventory.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                        _inventoryService.Insert(sessionInventory);
                        _inventoryService.SaveChanges();
                        if (miscellenou.ReturnType == Common.Common.CommonConstant.ReturnTypeEnum.Returntohands.ToInt())
                        {
                            stock.ProductId = miscellenou.ProductId.ToInt();
                            stock.Quantity = miscellenou.Quantity;
                            stock.CreatedAt = DateTime.Now;
                            stock.IsActive = true;
                            stock.ReturnId = modelToSAve.Id;
                            stock.Notes = "this inventory is added from return mangment";
                            stock.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                            stock.ProjectId = miscellenou.ProjectId;
                            _stock.Insert(stock);
                            _stock.SaveChanges();
                        }
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
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
        public IHttpActionResult GetbyId(int id)
        {
            var respoonse = new ApiResponse();
            var inventoryReturn = _miscellenouService.GetById(id);
            if (inventoryReturn != null)
            {
                respoonse.Success = true;
                respoonse.Result = inventoryReturn;
                return Json(respoonse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetAllReturnInventory(int? projectId = null)
        {
            var respoonse = new ApiResponse();
            var inventoryReturn = _miscellenouService.GetAllInventoryReturn(projectId).OrderByDescending(x => x.Id);
            if (inventoryReturn != null)
            {
                respoonse.Success = true;
                respoonse.Result = inventoryReturn;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetProductByUserId(int userId,int? regionId=null, int? projectId = null)
        {
            var respoonse = new ApiResponse();
            var products = _miscellenouService.GetProductByUserId(userId, regionId, projectId);
            if (products != null)
            {
                respoonse.Success = true;
                respoonse.Result = products;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);

        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetProductQuantity(int userId, int productId,int? regionId=null, int? projectId = null)
        {
            var respoonse = new ApiResponse();
            var productClmisReturnList = _miscellenouService.GetProductByUserId(userId, regionId, projectId).AsReadOnly().Where(x => x.user_id == userId && x.product_id == productId);
            if (productClmisReturnList != null)
            {
                respoonse.Success = true;
                respoonse.Result = productClmisReturnList;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
            return Json(productClmisReturnList);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetUsersByUserType(string usertype, int? projectId = null)
        {
            var respoonse = new ApiResponse();
            var users = _miscellenouService.GetUsersByUserType(usertype, projectId);

            if (users != null)
            {
                respoonse.Success = true;
                respoonse.Result = users;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);



        }
    }
}