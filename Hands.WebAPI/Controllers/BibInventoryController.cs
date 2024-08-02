using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.BLMIS;
using Hands.Service.Products;
using Hands.WebAPI.Models;
using Hands.WebAPI.Properties;
using Newtonsoft.Json;

namespace Hands.WebAPI.Controllers
{
    public class BibInventoryController : ApiController
    {
        // GET: BIBProcess
        private readonly IBlmisService _blmisService;
        private readonly IProductsService _productService;
        private readonly IProductsService _productsServiceBlmis;

        public BibInventoryController()
        {
            _blmisService = new BlmisService();
            _productService = new ProductsService();
            _productsServiceBlmis = new ProductsService();
        }

        //Create to CreateBibInventory
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateBibInventory(List<ViewModels.Models.BlmisUserStock.BlmisUserStock> model)
        {
            var respoonse = new ApiResponse();
            if (model != null && model.Count != 0)
            {
                foreach (var blmis in model)
                {
                    if (ModelState.IsValid)
                    {
                        Data.HandsDB.BlmisUserstock ModelToSAve = new Data.HandsDB.BlmisUserstock();
                        ModelToSAve.ProductId = blmis.ProductId;
                        if (blmis.ProductId == 2147483647)
                        {
                            Data.HandsDB.Product productModel = new Data.HandsDB.Product();
                            productModel.ProductName = blmis.ProductName;
                            productModel.ProductCategory = "Other";
                            productModel.BrandName = "";
                            productModel.MeasurementUnit = "";
                            productModel.Path = "new";
                            productModel.Producttype = "blmis";
                            productModel.RP = 0;
                            productModel.Generic = blmis.ProductName;
                            productModel.TP = 0;
                            productModel.PackSize = "";
                            productModel.RegNo = "";
                            productModel.ClientId = 0;
                            productModel.CreatedAt = DateTime.Now;
                            productModel.IsActive = true;
                            productModel.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;


                            _productService.Insert(productModel);
                            _productService.SaveChanges();

                            ModelToSAve.ProductId = productModel.ProductId;
                        }
                        ModelToSAve.UserId = blmis.UserId;
                        ModelToSAve.LhvId = blmis.LhvId;
                        ModelToSAve.CategoryId = blmis.CategoryId;
                        ModelToSAve.Quantity = blmis.Quantity;
                        ModelToSAve.Price = blmis.Price;
                        ModelToSAve.CreatedAt = DateTime.Now;
                        ModelToSAve.IsActive = true;
                        ModelToSAve.ProjectId = Request.Headers.Contains("projectId") ? Request.Headers.GetValues("projectId").First().ToInt() : Settings.Default.ProjectId;

                        _blmisService.Insert(ModelToSAve);
                        _blmisService.SaveChanges();
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
        public IHttpActionResult GetAllBibData(int? projectId = null, int? marviId = null)
        {
            var respoonse = new ApiResponse();
            var bib = _blmisService.GetBlmisUserStock(projectId,marviId).OrderByDescending(x => x.Id);
            if (bib != null)
            {
                respoonse.Success = true;
                respoonse.Result = bib;
                return Json(respoonse);
            }
            respoonse.Success = true;
            respoonse.Result = new string[] { };
            return Json(respoonse);
        }

        [ProjectIdAttributeController]
        public IHttpActionResult GetBlmisProducts(int? projectId = null)
        {
            var respoonse = new ApiResponse();
            var products = _productsServiceBlmis.GetAllBlimisProductsWithcategory(projectId);
            if (products != null)
            {
                Hands.Data.HandsDB.SpGetAppBibProductsWithCategoriesReturnModel productss = new SpGetAppBibProductsWithCategoriesReturnModel();
                productss.product_id = 2147483647;
                productss.product_name = "Other";
                products.Add(productss);
                respoonse.Success = true;
                respoonse.Result = products;
                return Json(respoonse, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }
            respoonse.Success = true;
            respoonse.Result = null;
            return Json(respoonse);
        }
    }
}