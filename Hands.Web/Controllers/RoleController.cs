using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Asp.netRoles;
using Hands.Service.Role;
using Hands.ViewModels.Models;
using Roles = Hands.ViewModels.Models.Role;


namespace Hands.Web.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly RoleManagmentController _roleManagmentController;
        private readonly IAspNetRolesService _aspNetRolesService;



        public RoleController()
        {
            _roleService = new RoleService();
            _roleManagmentController = new RoleManagmentController();
            _aspNetRolesService = new AspNetRolesService();


        }

        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _roleService.GetAllActive().Where(x=>x.ProjectId == HandSession.Current.ProjectId).OrderByDescending(r=>r.RoleId);

            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new ViewModels.Models.Role();

            return View(model);

        }

        public int MultiprojectCreate(ViewModels.Models.Role model)
        {
            Data.HandsDB.Role ModelToSAve = new Data.HandsDB.Role();
           
            if (ModelState.IsValid)
            {
              

                ModelToSAve.RoleId = model.RoleId;
                ModelToSAve.RoleName = model.RoleName;
                ModelToSAve.IsActive = true;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.ProjectId = model.ProjectId;
                _roleService.Insert(ModelToSAve);
                _roleService.SaveChanges();
                _roleManagmentController.Create(model);
            }

            return ModelToSAve.RoleId;

        }

        public int MultiprojectEdit(ViewModels.Models.Role model)
        {
            Data.HandsDB.Role ModelToSAve = new Data.HandsDB.Role();
            Data.HandsDB.Role source = _roleService.GetRole(model.ProjectId.Value);
            if (ModelState.IsValid)
            {


                ModelToSAve.RoleId = source.RoleId;
                ModelToSAve.RoleName = model.RoleName;
                ModelToSAve.IsActive = true;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.ProjectId = model.ProjectId;
                _roleService.Update(ModelToSAve);
                _roleService.SaveChanges();
                //_roleManagmentController.Create(model);
            }

            return ModelToSAve.RoleId;

        }


        [HttpPost]
        public ActionResult Create(ViewModels.Models.Role model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Role ModelToSAve = new Data.HandsDB.Role();

                ModelToSAve.RoleId = model.RoleId;
                ModelToSAve.RoleName = model.RoleName;
                ModelToSAve.IsActive = true;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.ProjectId = Common.Common.HandSession.Current.ProjectId.ToInt(); 
                _roleService.Insert(ModelToSAve);
                _roleService.SaveChanges();
                _roleManagmentController.Create(model);
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.Role model = _roleService.GetById(id);
            var Viewmodel = new ViewModels.Models.Role();
            Viewmodel.RoleId = model.RoleId;
            Viewmodel.RoleName = model.RoleName;
            return View(Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Role model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Role existingModel = _roleService.GetById(model.RoleId);
                if (existingModel != null)
                {
                    var aspnetrole = _aspNetRolesService.GetRoleByName(existingModel.RoleName);
                    aspnetrole.Name = model.RoleName;
                    existingModel.RoleId = model.RoleId;
                    existingModel.RoleName = model.RoleName;
                    existingModel.IsActive = true;
                    existingModel.CreatedAt = DateTime.Now;
                    _roleService.Update(existingModel);
                    _aspNetRolesService.Update(aspnetrole);
                    _roleService.SaveChanges();
                    _aspNetRolesService.SaveChanges();

                }


            }

            else
            {
                return Edit(model.RoleId);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.Role existingModel = _roleService.GetById(id);
               
                existingModel.IsActive = false;
                _roleService.Update(existingModel);
                _roleService.SaveChanges();
                var aspnetrole = _roleService.GetById(id);
                aspnetrole.IsActive = false;
                _roleService.Update(aspnetrole);
                _roleService.SaveChanges();
            }

            return RedirectToAction("Index");

        }



    }
}