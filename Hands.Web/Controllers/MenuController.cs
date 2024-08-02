using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms.VisualStyles;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Massage;
using Hands.Service.Menu;
using Hands.ViewModels.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Menu = Hands.ViewModels.Models.Model.Menu;

namespace Hands.Web.Controllers
{
    public class MenuController : Controller
    {
        public IMenuService _menu;
        private IMassageService _massageService;

        public MenuController()
        {
            _menu = new MenuService();
            _massageService = new MassageService();
        }

        public ActionResult LeftMenu()
        {
            if (Session["RoleId"] == null && Session["ProjectId"] == null)
            {
                Session.Clear();
                Session.Abandon();
                Session.Contents.RemoveAll();
                Session["RoleId"] = null;
                Session["UserId"] = null;
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                TempData["status"] = 1;
              return Login("login");
            }
            else
            {
                //int RoleId = Session[$"RoleId"].ToInt();
                //int projectId = Session["ProjectId"].ToInt();
                var data = _menu.GetMenuByRoleId(HandSession.Current.RoleId, HandSession.Current.ProjectId);
                return View(data);
            }
            
           
        }

        public ActionResult Login(string returnUrl)
        {
            return View("~/Views/Account/Login.cshtml");
        }
        public ActionResult GetAllmenu()
        {
            var nodes = _menu.GetAll();

            return Json(nodes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Header()
        {
            Hands.ViewModels.Models.Model.Menu menu = new Menu();
            menu.Messages = _massageService.GetAllActive().Take(3);
            return View("_Header" , menu);
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        
        public ActionResult Index()
        {
            var schemeList = _menu.GetAllActive();
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }
            return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new ViewModels.Models.SideMenu();
            return View(model);

        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.SideMenu model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Menu ModelToSAve = new Data.HandsDB.Menu(); ;

                ModelToSAve.Id = model.Id;
                ModelToSAve.Tittle = model.Tittle;
                ModelToSAve.ParentId = model.ParentId;
                ModelToSAve.Controller = model.Controller;
                ModelToSAve.Action = model.Action;
                ModelToSAve.IsActive = true;
                _menu.Insert(ModelToSAve);
                _menu.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.Menu model = _menu.GetById(id);

            var viewmodel = new ViewModels.Models.SideMenu();
            viewmodel.Id = model.Id;
            viewmodel.Tittle = model.Tittle;
            viewmodel.ParentId = model.ParentId;
            viewmodel.Controller = model.Controller;
            viewmodel.Action = model.Action;

            return PartialView("Edit", viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.SideMenu model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Menu existingModel = _menu.GetById(model.Id);

                existingModel.Tittle = model.Tittle;
                existingModel.ParentId = model.ParentId;
                existingModel.Controller = model.Controller;
                existingModel.Action = model.Action;
                existingModel.IsActive = true;
                _menu.Update(existingModel);
                _menu.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.Menu existingModel = _menu.GetById(id);
                existingModel.IsActive = false;
                _menu.Update(existingModel);
                _menu.SaveChanges();
            }

            return RedirectToAction("Index");

        }

    }


}



