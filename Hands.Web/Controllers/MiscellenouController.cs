using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Massage;
using Hands.Service.NoorChecklist;
using Hands.Service.Product;
using Miscellenou = Hands.ViewModels.Models.Miscellenou.Miscellenou;
using System.IO;
using OfficeOpenXml;
using NonFactors.Mvc.Grid;
using System.Data.SqlClient;
using System.EnterpriseServices;
using Excel;
using Hands.Common.Common;
using Hands.Service.AppUser;
using Hands.Service.Miscellenou;
using Hands.Service.Noor;
using Hands.Service.Products;
using Hands.Service.Regions;
using Hands.Service.SessionInventory;

namespace Hands.Web.Controllers
{
    public class MiscellenouController : Controller
    {
        private IMiscellenouService _miscellenouService;
        private readonly IProductsService _productService;
        private readonly IAppUserService _appUserService;
        private readonly ISessionInventoryService _inventoryService;
        private readonly IProductService _stock;
        private readonly IRegionService _regionService;
        private readonly IStockService _stockService;
        public MiscellenouController()
        {
            _miscellenouService = new MiscellenouService();
            _productService = new ProductsService();
            _appUserService = new AppUserService();
            _inventoryService = new SessionInventoryService();
            _stock = new ProductService();
            _regionService = new RegionService();
            _stockService=new StockServiceService();
        }

        // GET: Miscellenou
        public ActionResult Miscellenou()
        {
            var schemeList = _miscellenouService.GetActiveData().OrderByDescending(x => x.Id);
            if (Request.IsAjaxRequest())
            {
                return Json(schemeList, JsonRequestBehavior.AllowGet);
            }

            return Json(schemeList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var schemeList = _miscellenouService.GetAllInventoryReturn(HandSession.Current.ProjectId).OrderByDescending(x=>x.Id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new Miscellenou();
            model.RegionList = _regionService.GetRegionList();
            model.ProductList = _productService.GetAllActive().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            model.ProjectId = HandSession.Current.ProjectId;
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(ViewModels.Models.Miscellenou.Miscellenou model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.InventoryReturn modelToSAve = new Data.HandsDB.InventoryReturn();
                Data.HandsDB.SessionInventory sessionInventory = new Data.HandsDB.SessionInventory();
                Data.HandsDB.Stock stock = new Data.HandsDB.Stock();
                modelToSAve.ProjectId = HandSession.Current.ProjectId;
                modelToSAve.ProductId = model.ProductId;
                modelToSAve.RegionId = model.RegionId;
                modelToSAve.Quantity = model.Quantity;
                modelToSAve.Description = model.Description;
                modelToSAve.UserId = model.UserId;
                modelToSAve.UserType = model.UserType;
                modelToSAve.ReturnType = model.ReturnType;
                modelToSAve.CreatedAt = DateTime.Now;
                modelToSAve.IsActive = true;
                _miscellenouService.Insert(modelToSAve);
                _miscellenouService.SaveChanges();

                ////insert inventory for substract inventory
                sessionInventory.ProjectId = HandSession.Current.ProjectId;
                sessionInventory.RegionId = model.RegionId;
                sessionInventory.SessionId = null;
                sessionInventory.ProductId = model.ProductId;
                sessionInventory.Quantity = model.Quantity;
                sessionInventory.UserId = model.UserId;
                sessionInventory.ReturnId = modelToSAve.Id;
                sessionInventory.MwraId = null;
                sessionInventory.IsActive = true;
                sessionInventory.CreatedDate = DateTime.Now;
                _inventoryService.Insert(sessionInventory);
                _inventoryService.SaveChanges();
                if (model.ReturnType == Common.Common.CommonConstant.ReturnTypeEnum.Returntohands.ToInt())
                {
                    stock.ProjectId = HandSession.Current.ProjectId;
                    stock.RegionId = model.RegionId;
                    stock.ProductId = model.ProductId;
                    stock.Quantity = model.Quantity;
                    stock.CreatedAt = DateTime.Now;
                    stock.IsActive = true;
                    stock.ReturnId = modelToSAve.Id;
                    stock.Notes = "this inventory is added from return mangment";
                    _stock.Insert(stock);
                    _stock.SaveChanges();
                }
            } 
            return RedirectToAction("Index");

        }
        public JsonResult GetUsersByUserType(string usertype, int userId,int RegionId)
        {
            //todo fix logical issues

            List<SelectListItem> User = new List<SelectListItem>();
            var users = _miscellenouService.GetUsersByUserType(usertype, HandSession.Current.ProjectId).Where(x=>x.region_id==RegionId).ToList();

            User = new SelectList(users, "user_id", "full_name", userId).ToList();
            return Json(new SelectList(User, "Value", "Text", userId));
        }

        public JsonResult GetProductByUserId(int UserId, int ProductId,int? RegionId=null)
        {
            List<SelectListItem> Product = new List<SelectListItem>();
            Product = new SelectList(_miscellenouService.GetProductByUserId(UserId, RegionId, HandSession.Current.ProjectId), "product_id", "product_name", ProductId).ToList();
            return Json(new SelectList(Product, "Value", "Text", ProductId));
        }

        public JsonResult GetProductQuantity(int UserId, int ProductId, int? RegionId=null)
        {
            //todo fix logical issues
            var ProductClmisReturnList = _miscellenouService.GetQuantityByUserIdAndProductId(UserId, RegionId, HandSession.Current.ProjectId)
                .Where(x => x.user_id == UserId && x.product_id == ProductId);
            return Json(ProductClmisReturnList);
        }

        public ActionResult Edit(int id)
        {
            Data.HandsDB.InventoryReturn model = _miscellenouService.GetById(id);
            

            var Viewmodel = new Miscellenou();
            Viewmodel.IsActive = true;
            Viewmodel.RegionList = _regionService.GetRegionList();
            Viewmodel.ProductList = _productService.GetAllActive().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            Viewmodel.ProjectId = HandSession.Current.ProjectId;
            Viewmodel.Quantity = model.Quantity.ToInt();
            Viewmodel.RegionId = model.RegionId.ToInt();
            Viewmodel.Description = model.Description;
            Viewmodel.UserType = model.UserType;
            Viewmodel.ReturnType = model.ReturnType;
            Viewmodel.UserId = model.UserId;
            Viewmodel.ReturnId = model.Id;
            Viewmodel.ProductId = model.ProductId.ToInt();
            Viewmodel.CreatedAt = DateTime.Now;
            return PartialView("Edit", Viewmodel);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Miscellenou.Miscellenou model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.InventoryReturn existingModel = _miscellenouService.GetById(model.Id);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.ProductId = model.ProductId;
                    existingModel.Quantity = model.Quantity;
                    existingModel.Description = model.Description;
                    existingModel.UserId = model.UserId;
                    existingModel.UserType = model.UserType;
                    existingModel.ReturnType = model.ReturnType;
                    existingModel.RegionId = model.RegionId;
                    existingModel.CreatedAt = DateTime.Now;
                    _miscellenouService.Update(existingModel);
                    _miscellenouService.SaveChanges();

                    Data.HandsDB.SessionInventory sessionInventory = _inventoryService.GetByReturnId(model.ReturnId);

                    sessionInventory.Quantity = model.Quantity;
                    _inventoryService.Update(sessionInventory);
                    _inventoryService.SaveChanges();

                    Data.HandsDB.Stock stockDb = _stockService.GetByReturnId(model.ReturnId);

                    stockDb.Quantity = model.Quantity;
                    _stockService.Update(stockDb);
                    _stockService.SaveChanges();




                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.InventoryReturn existingModel = _miscellenouService.GetById(id);
                existingModel.IsActive = false;
                _miscellenouService.Update(existingModel);
                _miscellenouService.SaveChanges();
            }

            return RedirectToAction("Index");

        }
    }
}