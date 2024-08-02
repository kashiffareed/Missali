using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Service.Products;
using Hands.ViewModels.Models;
using Hands.WebAPI.Models;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: Products

        private readonly IProductsService _productsService;
        private ApiResponse respoonse;

        public ProductsController()
        {
            _productsService = new ProductsService();
        }
        [ProjectIdAttributeController]
        public IHttpActionResult GetByUserType(string userType,int? projectId = null, int pageNo = 1, int pageSize = int.MaxValue)
        {
            respoonse = new ApiResponse();
            var products = _productsService.GetAllProductsByType(userType, projectId, out var totalRecords, pageNo, pageSize);
            if (products != null)
            {

                respoonse.Success = true;
                respoonse.Result = products;
                respoonse.TotalRecords = totalRecords;
                return Json(respoonse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            respoonse.Success = true;
            respoonse.Result = null;
            return Json(respoonse);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetAllProducts(int? projectId = null)
        {
            respoonse = new ApiResponse();
            var products = _productsService.GetAllProducts().Where(x => x.Producttype == "clmis" && x.ProjectId== projectId).ToList();
            if (products.Any())
            {
                respoonse.Success = true;
                respoonse.Result = products.OrderByDescending(x => x.product_id);
                return Json(respoonse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }

    }
}
