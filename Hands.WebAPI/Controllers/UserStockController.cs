
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Service.CLMIS;
using Hands.ViewModels.Models;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;

namespace Hands.WebAPI.Controllers
{
    public class UserStockController : ApiController
    {
        // GET: UserStock
        private IClmisService _clmisService;
        private ApiResponse response;


        public UserStockController()
        {
            _clmisService = new ClmisService();
        }

        /// <summary>
        /// this api is for create/post the userStock
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IHttpActionResult CreateUserStock(List<ViewModels.Models.UserStock.userStock> model)
        {
            if (model != null && model.Count != 0)
            {
                foreach (var userStock in model)
                {
                    if (ModelState.IsValid)
                    {
                        Data.HandsDB.UsersStock ModelToSAve = new Data.HandsDB.UsersStock();
                        ModelToSAve.Quantity = userStock.Quantity;
                        ModelToSAve.CreatedAt = DateTime.Now;
                        ModelToSAve.IsActive = true;
                        ModelToSAve.ProductId = userStock.ProductId;
                        ModelToSAve.Price = userStock.Price;
                        ModelToSAve.UserId = userStock.UserId;
                        ModelToSAve.UserType = userStock.userType;
                        ModelToSAve.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;
                        //ModelToSAve.ProjectId = userStock.ProjectId;
                        _clmisService.Insert(ModelToSAve);
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }

                _clmisService.SaveChanges();
                response = new ApiResponse();
                response.Success = true;
                response.Result = null;
                return Json(response);
            }

            response = new ApiResponse();
            response.Success = false;
            response.Result = null;
            response.Message = "Invalid Request";
            return Json(response);

        }
        /// <summary>
        /// this api is for get the stock data by productId or userId
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [ProjectIdAttributeController]
        public IHttpActionResult GetStockById(int? ProductId, int? UserId, int? projectId = null, int pageNo = 1, int pageSize = int.MaxValue)
        {
            response = new ApiResponse();
            var stockData = _clmisService.GetStocks(ProductId, UserId, projectId, out var totalRecords, pageNo, pageSize);
            if (stockData != null)
            {
              
                response.Success = true;
                response.Result = stockData.Where(x=>x.quantity > 0);
                response.TotalRecords = totalRecords;
                return Json(response);
            }
            response.Success = true;
            response.Result = new string[] { };
            return Json(response);
        }

    }
}