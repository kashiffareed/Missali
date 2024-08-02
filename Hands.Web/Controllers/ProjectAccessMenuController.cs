using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Hands.Service.Menu;
using Hands.Service.Project;
using Hands.Service.ProjectMenuAccess;
using Hands.ViewModels.Models.Model;
using Hands.ViewModels.Models.ProjectAcessMenuModel;
using Microsoft.ApplicationInsights.WindowsServer;

namespace Hands.Web.Controllers
{
    public class ProjectAccessMenuController : ControllerBase
    {
        // GET: ProjectAccessMenu
        private readonly IProjectService _projectService;
        private readonly IMenuService _menuService;
        private readonly IProjectMenuAccessService _projectMenuAccessService;

        public ProjectAccessMenuController()
        {
            _projectService = new ProjectService();
            _menuService = new MenuService();
            _projectMenuAccessService = new ProjectMenuAccessService();

        }

        public ActionResult Index()
        {
            var model = new ProjectAcecssMenu();
            model.Projects = _projectService.GetAllActive();

            return View(model);
        }


        [HttpPost]
        public ActionResult Indexx(string data1, int projectId)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            int[] selectedMenus = js.Deserialize<int[]>(data1);
            List<Hands.ViewModels.Models.ProjectAcessMenuModel.ProjectAcecssMenu> projectmenuAcces = new List<Hands.ViewModels.Models.ProjectAcessMenuModel.ProjectAcecssMenu>();

            List<Hands.Data.HandsDB.ProjectMenuAccess> Access = _projectMenuAccessService.getAllByProjectId(projectId).ToList();
            if (Access.Count > 0)
            {
                foreach (var remove in Access)
                {
                    _projectMenuAccessService.Remove(remove);
                }

            }

            foreach (var menus in selectedMenus)
            {
                var model = new Hands.Data.HandsDB.ProjectMenuAccess
                {
                    ProjectId = projectId,
                    MenuId = menus,
                    IsActive = true
                };
                _projectMenuAccessService.Insert(model);
            }
            _projectMenuAccessService.SaveChanges();
            return RedirectToAction("Index");
        }

        public string DocumentCategoryHierarchy()
        {
            var menus = _menuService.GetAll();
            var list = (
                from c in menus
                select new Menu
                {
                    id = c.Id.ToString(),
                    parent = c.ParentId == (int?)0 ? "#" : c.ParentId.ToString(), 
                    text = c.Tittle
                }).ToList();
            return new JavaScriptSerializer().Serialize(list);
        }
    }
}