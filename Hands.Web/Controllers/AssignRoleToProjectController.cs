
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Asp.netRoles;
using Hands.Service.AssignRoleToProject;
using Hands.Service.Project;

namespace Hands.Web.Controllers
{
    public class AssignRoleToProjectController : Controller
    {
        // GET: Project
        private readonly IAssignRoleToProjectService _assignRoleToProjectService;
        private readonly IProjectService _projectService;
        private readonly IAspNetRolesService _aspNetRolesService;
        public AssignRoleToProjectController()
        {
            _assignRoleToProjectService = new AssignRoleToProjectService();
            _projectService = new ProjectService();
            _aspNetRolesService = new AspNetRolesService();
        }
        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _assignRoleToProjectService.SpAssignRoleToProject().OrderByDescending(x=>x.Id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index" ,schemeList);
            }

            return View(schemeList);
        }
        public ActionResult Create()
        {
            var model = new ViewModels.Models.AssignRoleToProject.AssignRoleToProject();
            model.ProjectList = _projectService.GetAllActive();
            model.RoleList = _aspNetRolesService.GetAllActive();
            return View(model);

        }


        [HttpPost]
        public ActionResult Create(ViewModels.Models.AssignRoleToProject.AssignRoleToProject model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.AssignRoleToProject ModelToSAve = new Data.HandsDB.AssignRoleToProject();
                ModelToSAve.ProjectId = model.ProjectId;
                ModelToSAve.RoleId = model.RoleId;
                ModelToSAve.IsActive = true;
                ModelToSAve.CreatedAt = DateTime.Now;
                _assignRoleToProjectService.Insert(ModelToSAve);
                _assignRoleToProjectService.SaveChanges();
            }
            return RedirectToAction("Index");

        }


        public JsonResult assignRole(int projectId, string roleId)
        {
            bool doesExistAlready = _assignRoleToProjectService.GetAllActiveData().Any(o => o.ProjectId == projectId && o.RoleId == roleId);

            return Json(doesExistAlready);
        }



        //public ActionResult Edit(int id)
        //{
        //    Data.HandsDB.ProjectSolution model = _projectService.GetById(id);
        //    var Viewmodel = new ViewModels.Models.Project.Projects(); ;
        //    Viewmodel.Id = model.Id;
        //    Viewmodel.Name = model.Name;
        //    Viewmodel.IsActive = true;
        //    return View(Viewmodel);
        //}

        //[HttpPost]
        //public ActionResult Edit(ViewModels.Models.Project.Projects model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ProjectSolution existingModel = _projectService.GetById(model.Id);
        //        if (existingModel != null)
        //        {
        //            existingModel.Name = model.Name;
        //            existingModel.IsActive = true;
        //            _projectService.Update(existingModel);
        //            _projectService.SaveChanges();

        //        }

        //    }

        //    else
        //    {
        //        return Edit(model.Id);
        //    }
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    if (id != null)
        //    {
        //        Data.HandsDB.ProjectSolution existingModel = _projectService.GetById(id);
        //        existingModel.IsActive = false;
        //        _projectService.Update(existingModel);
        //        _projectService.SaveChanges();
        //    }

        //    return RedirectToAction("Index");

        //}
    }
}