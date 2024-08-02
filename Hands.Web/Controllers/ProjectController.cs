
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Project;

namespace Hands.Web.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        private readonly IProjectService _projectService;

        public ProjectController()
        {
            _projectService = new ProjectService();
        }
        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _projectService.GetAllActive().OrderByDescending(x=>x.Id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index" ,schemeList);
            }

            return View(schemeList);
        }
        public ActionResult Create()
        {
            var model = new ViewModels.Models.Project.Projects();

            return View(model);

        }
        public int CreateProject(ViewModels.Models.Project.Projects model)
        {
            Data.HandsDB.ProjectSolution ModelToSAve = new Data.HandsDB.ProjectSolution();
            if (ModelState.IsValid)
            {


                ModelToSAve.Name = model.Name;
                ModelToSAve.IsActive = true;
                _projectService.Insert(ModelToSAve);
                _projectService.SaveChanges();
            }

            return ModelToSAve.Id;


        }

        public int UpdateProject(ViewModels.Models.Project.Projects model)
        {
            //Data.HandsDB.ProjectSolution ModelToSAve = new Data.HandsDB.ProjectSolution();
            
            Data.HandsDB.ProjectSolution tareget = _projectService.GetById(model.Id);
            if (ModelState.IsValid)
            {


                tareget.Name = model.Name;
                tareget.IsActive = true;
                _projectService.Update(tareget);
                _projectService.SaveChanges();
            }

            return tareget.Id;


        }



        [HttpPost]
        public ActionResult Create(ViewModels.Models.Project.Projects model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.ProjectSolution ModelToSAve = new Data.HandsDB.ProjectSolution();

                ModelToSAve.Name = model.Name;
                ModelToSAve.IsActive = true;
                _projectService.Insert(ModelToSAve);
                _projectService.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.ProjectSolution model = _projectService.GetById(id);
            var Viewmodel = new ViewModels.Models.Project.Projects(); ;
            Viewmodel.Id = model.Id;
            Viewmodel.Name = model.Name;
            Viewmodel.IsActive = true;
            return View(Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Project.Projects model)
        {
            if (ModelState.IsValid)
            {
                ProjectSolution existingModel = _projectService.GetById(model.Id);
                if (existingModel != null)
                {
                    existingModel.Name = model.Name;
                    existingModel.IsActive = true;
                    _projectService.Update(existingModel);
                    _projectService.SaveChanges();

                }

            }

            else
            {
                return Edit(model.Id);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.ProjectSolution existingModel = _projectService.GetById(id);
                existingModel.IsActive = false;
                _projectService.Update(existingModel);
                _projectService.SaveChanges();
            }

            return RedirectToAction("Index");

        }
    }
}