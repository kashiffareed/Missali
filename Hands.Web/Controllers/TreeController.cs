using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Asp.netRoles;
using Hands.Service.AssignMenuToProject;
using Hands.Service.AssignRoleToProject;
using Hands.Service.Menu;
using Hands.Service.MenuAssessLevel;
using Hands.Service.Project;
using Hands.Service.Role;
using Hands.Service.RoleMenuAccess;
using Hands.Service.RoleMenuAccessLevelRights;
using Menu = Hands.ViewModels.Models.Model.Menu;

namespace Hands.Web.Controllers
{
    public class TreeController : ControllerBase
    {
        private readonly IAspNetRolesService _aspNetRolesService;

        private readonly IAssignMenuToProjectService _AssignMenuToProjectService;

        private readonly IAssignRoleToProjectService _assignRoleToProjectService;
        private readonly IMenuLevelAccessService _levelAccessService;

        // GET: Tree
        private readonly IMenuService _menuService;
        private readonly IProjectService _projectService;
        private readonly IRoleMenuAccessService _roleMenuAccessService;
        private readonly IRoleService _roleService;
        private readonly IRoleMenuAccessLevelRightService _accessLevelRightService;

        public TreeController()
        {
            _menuService = new MenuService();
            _roleService = new RoleService();
            _roleMenuAccessService = new RoleMenuAccessService();
            _aspNetRolesService = new AspNetRolesService();
            _projectService = new ProjectService();
            _assignRoleToProjectService = new AssignRoleToProjectService();
            _AssignMenuToProjectService = new AssignMenuToProjectService();
            _levelAccessService = new MenuLevelAccessService();
            _accessLevelRightService = new RoleMenuAccessLevelRightService();
        }
        public ActionResult AssignMenuToRoleList()
        {
            var schemeList = _roleService.GetAllActive().Where(x => x.ProjectId == HandSession.Current.ProjectId).OrderByDescending(r => r.RoleId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("AssignMenuToRoleList", schemeList);
            }

            return View("AssignMenuToRoleList", schemeList);
        }

        public ActionResult AssignMenuToRoleDetialList(int roleId)
        {
            var schemeList = _roleMenuAccessService.getAllAssignMenutoRoles().Where(x => x.RoleId == roleId && x.ProjectId == HandSession.Current.ProjectId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("AssignMenuToRoleDetialList", schemeList);
            }

            return View("AssignMenuToRoleDetialList", schemeList);
        }
        public ActionResult Index()
        {
            var menu = new Menu();
            menu.Roles = _roleService.GetRollByProjectId(HandSession.Current.ProjectId.ToInt());
            //menu.ProjectList = _projectService.GetAllActive().OrderByDescending(x => x.Id);
            return View(menu);
        }

        [HttpPost]
        public ActionResult Indexx(string data1, int RoleId)
        {
            var js = new JavaScriptSerializer();
            var selectedMenus = js.Deserialize<IList<dynamic>>(data1);

            List<Hands.ViewModels.Models.RoleMenuAccess> rolemenuAcces = new List<Hands.ViewModels.Models.RoleMenuAccess>();
            List<Hands.Data.HandsDB.RoleMenuAccess> access = _roleMenuAccessService.getAllByRoleId(RoleId, HandSession.Current.ProjectId).ToList();
            List<RoleMenuAccessLevelRight> accessLevel = _accessLevelRightService
                .GetListByProjectAndRoleId(HandSession.Current.ProjectId, RoleId).ToList();
            if (access.Count > 0)
            {
                foreach (var remove in access)
                {
                    _roleMenuAccessService.Remove(remove);
                }
            }
            if (accessLevel.Count > 0)
            {
                foreach (var remove in accessLevel)
                {
                    _accessLevelRightService.Remove(remove);
                }
            }
            _accessLevelRightService.SaveChanges();
            var parentIds = (from item in selectedMenus.ToList()
                             select item["original"]["parent"].ToString()).Distinct().ToList();
            var menuIds = (from item in selectedMenus.ToList()
                           select item["original"]["id"].ToString()).ToList();
            var toSaveModel = new List<RoleMenuAccess>();
            var toSaveRightsModel = new List<RoleMenuAccessLevelRight>();
            foreach (var menuId in parentIds.Union(menuIds))
            {
                if (menuId != "#")
                {
                    int CurrentMenuId;
                    var iParse = int.TryParse(menuId, out CurrentMenuId);
                    if (iParse)
                    {
                        var model = new RoleMenuAccess();
                        model.RoleId = RoleId;
                        model.ProjectId = HandSession.Current.ProjectId;
                        //model.RoleId = 3;
                        //model.ProjectId = 7;
                        int Ids = CurrentMenuId;
                        model.MenuId = Ids;
                        model.IsActive = true;
                        model.AccessLevelId = 2;
                        toSaveModel.Add(model);

                        var right = _levelAccessService.GetListByMenuId(CurrentMenuId);
                        if (right.Count > 0)
                        {
                            var levelRight = new RoleMenuAccessLevelRight();
                            foreach (var levelRights in right)
                            {
                                levelRight.ProjectId = HandSession.Current.ProjectId;
                                levelRight.RoleId = RoleId;
                                levelRight.MenuId = levelRights.MenuId;
                                levelRight.AccessLevelId = GetSetRightValuebyName(levelRights.LevelName.ToLower());
                                levelRight.CreatedAt = DateTime.UtcNow;
                                levelRight.IsActive = true;
                                _accessLevelRightService.Insert(levelRight);
                                _accessLevelRightService.SaveChanges();
                            }
                        }

                    }
                    //else
                    //{

                    //    //var right = _levelAccessService.GetListByMenuIdString(menuId.ToString());
                    //    //foreach (var levelRights in right)
                    //    //{
                    //    //    model.ProjectId = HandSession.Current.ProjectId;
                    //    //    model.RoleId = HandSession.Current.RoleId;
                    //    //    model.MenuId = levelRights.MenuId;
                    //    //    model.AccessLevelId = GetSetRightValuebyName(levelRights.LevelName);
                    //    //    model.CreatedAt = DateTime.UtcNow;
                    //    //    model.IsActive = true;
                    //    //    toSaveRightsModel.Add(model);
                    //    //}
                    //}

                }

                //_roleMenuAccessService.Insert(model);
                //_roleMenuAccessService.SaveChanges();
            }
            _roleMenuAccessService.InsertRange(toSaveModel.OrderBy(x => x.MenuId).ToList());
            _roleMenuAccessService.SaveChanges();
            //_accessLevelRightService.InsertRange(toSaveRightsModel);
            //_accessLevelRightService.SaveChanges();
            return RedirectToAction("Index");
        }

        public string DocumentCategoryHierarchy()
        {
            //var ddd = _AssignMenuToProjectService.GetAllMenuByProjectId(34);
            var dbmenus = _AssignMenuToProjectService.GetAllMenuByProjectId(HandSession.Current.ProjectId);

            var GetAllAccessLevel = _levelAccessService.GetAllActive();
            var menustoRender = (
                from c in dbmenus
                select new Menu
                {
                    id = c.MenuId.ToString(),
                    parent = c.ParentId == (int?)0 ? "#" : c.ParentId.ToString(),
                    text = c.MenuId == 1 ? "All" : c.Tittle
                }).ToList();
            if (menustoRender.All(x => x.id != "1"))
            {
                var list = new Menu();
                list.id = "1";
                list.text = "All";
                list.parent = "#";
                menustoRender.Insert(0, list);
            }

            var listmenus = new List<Menu>();
            foreach (var menu in menustoRender)
            {
                var accessLevel = _levelAccessService.GetListByMenuId(menu.id.ToInt());
                if (accessLevel.Count > 0)
                {
                    foreach (var levelAccess in accessLevel)
                    {
                        var list = new Menu();
                        list.id = levelAccess.Id;
                        list.text = levelAccess.LevelName;
                        list.parent = levelAccess.MenuId.ToString();
                        listmenus.Add(list);
                    }

                }
            }
            menustoRender.AddRange(listmenus);


            return new JavaScriptSerializer().Serialize(menustoRender);
        }

        public JsonResult GetRoles(int projectId, string RoleId)
        {
            //todo fix logical issues

            var Role = new List<SelectListItem>();
            var Roles = _assignRoleToProjectService.GetRoleByProject(projectId);

            Role = new SelectList(Roles, "RoleId", "RoleName").ToList();
            return Json(new SelectList(Role, "Value", "Text", RoleId));
        }

        public int GetSetRightValuebyName(string name)
        {
            switch (name.ToLower())
            {
                case "view":
                    return 1;
                case "create":
                    return 2;
                case "edit":
                    return 3;
                default:
                    return 4;


            }
        }
    }
}