using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.ClmisDashboard;
using Hands.Service.CLMIS;
using Hands.Service.Dashboard;
using Hands.Service.NoorChecklist;
using Hands.Service.Product;
using Hands.Service.Products;
using Hands.ViewModels.Models;
using Hands.ViewModels.Models.UserStock;

namespace Hands.Web.Controllers
{
    public class ClmisaDashboardController : ControllerBase
    {
        private readonly IClmisDashboar _clmisDashboar;

        public ClmisaDashboardController()
        {
            _clmisDashboar = new ClmisDashboardService();
           
        }

        // GET: RealTimeCheckList
        //public ActionResult Index()
        //{
        //    var model = new Hands.ViewModels.Models.ClmisDashboard.ClmisDashboard();
        //    model.ClmisDashboardlist = _clmisDashboar.GetClmisLhvDetail();
        //    return View(model);
        // }



        //public ActionResult Create()
        //{
        //    var model = new userStock();
        //    model.ProductsList = _productService.GetAllActive();
        //    return View(model);
        //}
        //[HttpPost]
        //public ActionResult Create(ViewModels.Models.UserStock.userStock model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Data.HandsDB.UsersStock ModelToSAve = new Data.HandsDB.UsersStock();

        //        //ModelToSAve.ProductlName = model.ProductlName;
        //        ModelToSAve.Quantity = model.Quantity;
        //        ModelToSAve.CreatedAt = DateTime.Now;
        //        ModelToSAve.IsActive = true;
        //        ModelToSAve.ProductId = model.ProductId;
        //        ModelToSAve.UserId = model.UserId;
        //        ModelToSAve.UserType = model.userType;
        //        _clmisService.Insert(ModelToSAve);
        //        _clmisService.SaveChanges();

        //    }

        //    return RedirectToAction("Index", "Clmis");

        //}

        //public ActionResult Edit(int id)
        //{
        //    Data.HandsDB.UsersStock model = _clmisService.GetById(id);
        //    var Viewmodel = new ViewModels.Models.UserStock.userStock();
        //    Viewmodel.ProductsList = _productService.GetAllActive();
        //    Viewmodel.HlvList = _appUserService.GetAllActive();
        //    //Viewmodel.ProductName = model.ProductName;
        //    Viewmodel.Quantity = model.Quantity;
        //    Viewmodel.CreatedAt = DateTime.Now;
        //    Viewmodel.IsActive = true;
        //    Viewmodel.ProductId = model.ProductId;
        //    Viewmodel.UserId = model.UserId;
        //    Viewmodel.userType = model.UserType;
        //    Viewmodel.UsersStockId = model.UsersStockId;
        //    return PartialView("Edit", Viewmodel);
        //}
        //[HttpPost]
        //public ActionResult Edit(ViewModels.Models.UserStock.userStock model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Data.HandsDB.UsersStock existingModel = _clmisService.GetById(model.UsersStockId);
          
        //        if (existingModel != null)
        //        {
        //            existingModel.ProductId = model.ProductId;
        //            existingModel.Quantity = model.Quantity;
        //            existingModel.CreatedAt = DateTime.Now;
        //            existingModel.IsActive = true;
        //            existingModel.ProductId = model.ProductId;
        //            existingModel.UserId = model.UserId;
        //            existingModel.UserType = model.userType;



        //            _clmisService.Update(existingModel);
        //            _clmisService.SaveChanges();
        //        }

        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Delete(int id)
        //{
        //    if (id != null)
        //    {
        //        Data.HandsDB.UsersStock existingModel = _clmisService.GetById(id);
        //        existingModel.IsActive = false;
        //        _clmisService.Update(existingModel);
        //        _clmisService.SaveChanges();
        //    }

        //    return RedirectToAction("Index");

        //}

        //public JsonResult GetUserByType(string usertype , int userId)
        //{
        //    //todo fix logical issues

        //    List<SelectListItem> Users = new List<SelectListItem>();
        //    var user = _appUserService.GetAll(usertype);

        //    Users = new SelectList(user, "AppUserId", "FullName").ToList();
        //    return Json(new SelectList(Users, "Value", "Text", userId));
        //}


    }


}