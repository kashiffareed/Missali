using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EnvDTE;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AspnetUsers;
using Hands.Service.AssignMenuToProject;
using Hands.Service.Menu;
using Hands.Service.MenuAssessLevel;
using Hands.Service.MultiProjectAppDetails;
using Hands.Service.Project;
using Hands.Service.RoleMenuAccess;
using Hands.Service.RoleMenuAccessLevelRights;
using Hands.ViewModels.Models.AssignMenuToProject;
using Hands.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Hands.Service.AppMenu;
using Hands.Service.AssignMenuToApp;
using Hands.Service.User;

namespace Hands.Web.Controllers
{
    public class AssignMenuToProjectController : ControllerBase
    {
        // GET: AssignMenuToProject
        private readonly IMenuService _menuService;
        private readonly IAppMenuService _appMenuService;
        private readonly IAssignMenuToProjectService _projectMenuService;
        private readonly IAssignMenuToAppService _assignMenuToAppService;
        private readonly IProjectService _projectService;
        private readonly IMultiProjectAppDetailsService _multiProjectAppDetailsService;
        private ProjectController _projectController;
        private RoleController _roleController;
        private UserController _userController;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly AccountController _accountController;
        private readonly IAspnetUserService _aspnetUserService;
        private readonly IRoleMenuAccessService _roleMenuAccessService;
        private readonly IRoleMenuAccessLevelRightService _accessLevelRightService;
        private readonly IUserService _userService;

        public AssignMenuToProjectController()
        {
            _menuService = new MenuService();
            _appMenuService = new AppMenuService();
            _projectMenuService = new AssignMenuToProjectService();
            _assignMenuToAppService = new AssignMenuToAppService();
            _projectService = new ProjectService();
            _projectController = new ProjectController();
            _roleController = new RoleController();
            _userController = new UserController();
            _accountController = new AccountController(_userManager, _signInManager);
            _aspnetUserService = new AspnetUserService();
            _roleMenuAccessService = new RoleMenuAccessService();
            _accessLevelRightService = new RoleMenuAccessLevelRightService();
            _multiProjectAppDetailsService = new MultiProjectAppDetailsService();
            _userService = new UserService();
        }

        public ActionResult Index()
        {
            var schemeList = _projectMenuService.GetProjectDetails();

            if (Request.IsAjaxRequest())
            {
                return PartialView("ProjectDetails", schemeList);
            }

            return View("ProjectDetails", schemeList);
        }

        public JsonResult assignProjectName(string projectname)
        {
            bool doesExistAlready = _projectService.GetAllActive().Any(o => o.Name == projectname);

            return Json(doesExistAlready);
        }

        public JsonResult assignEmail(string email)
        {
            bool doesExistAlready = _aspnetUserService.GetAllActive().Any(o => o.UserName == email);

            return Json(doesExistAlready);
        }

        public ActionResult Create()
        {
            var model = new AssignMenuToProjectViewModel();
            model.RoleName = "Admin";
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(string data1, string projectName, string roleName, string Email, string Password)
        {
            var projectModel = new ViewModels.Models.Project.Projects();
            projectModel.Name = projectName;
            int NewprojectId = _projectController.CreateProject(projectModel);

            var RoleModel = new ViewModels.Models.Role();
            RoleModel.RoleName = roleName;
            RoleModel.ProjectId = NewprojectId;
            int NewRoleId = _roleController.MultiprojectCreate(RoleModel);

            var UserCreateionmodel = new ViewModels.Models.user();
            UserCreateionmodel.UserName = Email;
            UserCreateionmodel.FullName = Email;
            UserCreateionmodel.Email = Email;
            UserCreateionmodel.PlainPassword = Password;
            UserCreateionmodel.Pwd = Password;
            UserCreateionmodel.ProjectId = NewprojectId;
            UserCreateionmodel.RoleId = NewRoleId;
            UserCreateionmodel.Designation = roleName;
            var userId = _userController.CreateForMultiProject(UserCreateionmodel);

            CreateForMultiProject(Email, Password, NewprojectId);

            JavaScriptSerializer js = new JavaScriptSerializer();
            var selectedMenus = js.Deserialize<IList<dynamic>>(data1);

            List<Hands.ViewModels.Models.RoleMenuAccess> rolemenuAcces =
                new List<Hands.ViewModels.Models.RoleMenuAccess>();
            List<Hands.Data.HandsDB.AssignMenuToProject> Access =
                _projectMenuService.GetAllMenuByProjectId(NewprojectId).ToList();
            if (Access.Count > 0)
            {
                foreach (var Remove in Access)
                {
                    _projectMenuService.Remove(Remove);
                }
            }

            var parentIds = (from item in selectedMenus.ToList()
                             select item["original"]["parent"].ToString()).Distinct().ToList();
            var menuIds = (from item in selectedMenus.ToList()
                           select item["original"]["id"].ToString()).ToList();
            List<object> AllIds = parentIds.Union(menuIds).ToList();
            AllIds[0] = 0;
            var intList = AllIds.Select(Convert.ToInt32).ToList();


            var menus = _menuService.GetAll();
            var selectedMenusfromList = menus.Where(x => intList.Contains(x.Id)).ToList();

            foreach (var values in selectedMenusfromList)
            {
                var model = new Hands.Data.HandsDB.AssignMenuToProject();
                model.ProjectId = NewprojectId;
                model.MenuId = values.Id;
                model.Tittle = values.Tittle;
                model.ParentId = values.ParentId;
                model.Controller = values.Controller;
                model.Action = values.Action;
                model.IsActive = true;
                _projectMenuService.Insert(model);
            }
            _projectMenuService.SaveChanges();
            AssignMenuToSuperAdmin(data1, NewRoleId, NewprojectId);
            return RedirectToAction("Index");
        }


        public ActionResult EditAppMenu(int projectId)
        {

            Data.HandsDB.User model = _userService.GetUserById(projectId);
            var UserCreateionmodel = new ViewModels.Models.user();
            UserCreateionmodel.UserId = model.UserId;
            UserCreateionmodel.UserName = model.Email;
            UserCreateionmodel.FullName = model.Email;
            UserCreateionmodel.Email = model.Email;
            UserCreateionmodel.PlainPassword = model.PlainPassword;
            UserCreateionmodel.Pwd = model.PlainPassword;
            UserCreateionmodel.ProjectName = _projectService.GetAll().Where(x => x.Id == projectId).Select(x => x.Name).FirstOrDefault();
            UserCreateionmodel.ProjectId = model.ProjectId;
            UserCreateionmodel.RoleName = "Admin";
            //UserCreateionmodel.Designation = roleName;
            return View(UserCreateionmodel);
        }

        [HttpPost]
        public ActionResult EditAppMenu( string data1, int projectId, string projectName, string roleName, string Email, string Password,int UserId)
        {
             var projectModel = new ViewModels.Models.Project.Projects();
            projectModel.Name = projectName;
            projectModel.Id = projectId;
            int NewprojectId = _projectController.UpdateProject(projectModel);

            var RoleModel = new ViewModels.Models.Role();
            RoleModel.RoleName = roleName;
            RoleModel.ProjectId = NewprojectId;
            int NewRoleId = _roleController.MultiprojectEdit(RoleModel);

            var UserCreateionmodel = new ViewModels.Models.user();
            UserCreateionmodel.UserId = UserId;
            UserCreateionmodel.UserName = Email;
            UserCreateionmodel.FullName = Email;
            UserCreateionmodel.Email = Email;
            UserCreateionmodel.PlainPassword = Password;
            UserCreateionmodel.Pwd = Password;
            UserCreateionmodel.ProjectId = NewprojectId;
            UserCreateionmodel.RoleId = NewRoleId;
            UserCreateionmodel.Designation = roleName;
            var userId = _userController.EditForMultiProject(UserCreateionmodel);

            //CreateForMultiProject(Email, Password, NewprojectId);

            JavaScriptSerializer js = new JavaScriptSerializer();
            var selectedMenus = js.Deserialize<IList<dynamic>>(data1);
            List<Hands.ViewModels.Models.RoleMenuAccess> rolemenuAcces =
                new List<Hands.ViewModels.Models.RoleMenuAccess>();

            List<Hands.Data.HandsDB.AssignMenuToProject> Access =
                _projectMenuService.GetAllMenuByProjectId(NewprojectId).ToList();
            if (Access.Count > 0)
            {
                foreach (var Remove in Access)
                {
                    _projectMenuService.Remove(Remove);
                }
            }

            var parentIds = (from item in selectedMenus.ToList()
                             select item["original"]["parent"].ToString()).Distinct().ToList();
            var menuIds = (from item in selectedMenus.ToList()
                           select item["original"]["id"].ToString()).ToList();
            List<object> AllIds = parentIds.Union(menuIds).ToList();
            AllIds[0] = 0;
            var intList = AllIds.Select(Convert.ToInt32).ToList();


            var menus = _menuService.GetAll();
            var selectedMenusfromList = menus.Where(x => intList.Contains(x.Id)).ToList();

            foreach (var values in selectedMenusfromList)
            {
                var model = new Hands.Data.HandsDB.AssignMenuToProject();
                model.ProjectId = NewprojectId;
                model.MenuId = values.Id;
                model.Tittle = values.Tittle;
                model.ParentId = values.ParentId;
                model.Controller = values.Controller;
                model.Action = values.Action;
                model.IsActive = true;
                _projectMenuService.Update(model);
            }
            _projectMenuService.SaveChanges();
            AssignMenuToSuperAdmin(data1, NewRoleId, NewprojectId);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult DeleteUser(int userId)
        {
            if (userId != null)
            {
                Data.HandsDB.User existingModel = _userService.GetUserByUserId(userId);
                existingModel.IsActive = false;
                _userService.Update(existingModel);
                _userService.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public JsonResult GetSelectdMenuForEdit(int ProjectId)
        {
           

            List<Hands.Data.HandsDB.AssignMenuToProject> Access =
                _projectMenuService.GetAllMenuByProjectId(ProjectId).ToList();

            return Json(Access);
        }




        #region MenusRender

        /// <summary>
        /// this method is for render the menus list
        /// </summary>
        /// <returns></returns>
        public string MenuCategoryHierarchy()
        {
            var menus = _menuService.GetAll();
            var list = (
                from c in menus
                select new AssignMenuToProjectViewModel()
                {
                    id = c.Id.ToString(),
                    parent = c.ParentId == (int?)0 ? "#" : c.ParentId.ToString(),
                    text = c.Tittle
                }).ToList();
            return new JavaScriptSerializer().Serialize(list);
        }

        #endregion

        #region Applicaiton user


        public void CreateForMultiProject(string email, string password, int projectId)
        {
            var userrr = new ApplicationUser { UserName = email, Email = email };
            var result = UserManager.Create(userrr, password);
            var aspnetuser = _aspnetUserService.GetByEmailAspnetUser(email);
            aspnetuser.ProjectId = projectId;
            _aspnetUserService.Update(aspnetuser);
            _aspnetUserService.SaveChanges();
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        #endregion

        #region assignmenutouperAdmin

        public void AssignMenuToSuperAdmin(string data1, int RoleId, int ProjectId)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var selectedMenus = js.Deserialize<IList<dynamic>>(data1);
            List<Hands.ViewModels.Models.RoleMenuAccess> rolemenuAcces =
                new List<Hands.ViewModels.Models.RoleMenuAccess>();
            List<Hands.Data.HandsDB.RoleMenuAccess> Access =
                _roleMenuAccessService.getAllByRoleId(RoleId, ProjectId).ToList();
            var toSaveRightsModel = new List<RoleMenuAccessLevelRight>();
            if (Access.Count > 0)
            {
                foreach (var Remove in Access)
                {
                    _roleMenuAccessService.Remove(Remove);
                }
            }

            var parentIds = (from item in selectedMenus.ToList()
                             select item["original"]["parent"].ToString()).Distinct().ToList();
            var menuIds = (from item in selectedMenus.ToList()
                           select item["original"]["id"].ToString()).ToList();
            var EnumRigts = Enum.GetValues(typeof(CommonConstant.RightLevelEnum)).Cast<CommonConstant.RightLevelEnum>()
                .Select(v => v.ToString()).ToList();

            var modelToSaveAccess = new List<Hands.Data.HandsDB.RoleMenuAccess>();
            foreach (var menus in parentIds.Union(menuIds))
            {
                if (menus != "#")
                {
                    var model = new Hands.Data.HandsDB.RoleMenuAccess();
                    model.RoleId = RoleId;
                    model.ProjectId = ProjectId;
                    int Ids = Convert.ToInt32(menus);
                    model.MenuId = Ids;
                    model.IsActive = true;
                    model.AccessLevelId = 2;
                    modelToSaveAccess.Add(model);
                    foreach (var rightTosuperUser in EnumRigts)
                    {
                        var modelRights = new RoleMenuAccessLevelRight();
                        modelRights.ProjectId = ProjectId;
                        modelRights.RoleId = RoleId;
                        modelRights.MenuId = Convert.ToInt32(menus);
                        modelRights.AccessLevelId = GetSetRightValuebyName(rightTosuperUser);
                        modelRights.CreatedAt = DateTime.UtcNow;
                        modelRights.IsActive = true;
                        toSaveRightsModel.Add(modelRights);
                    }
                }
            }
            _roleMenuAccessService.InsertRange(modelToSaveAccess.OrderBy(x => x.MenuId).ToList());
            _roleMenuAccessService.SaveChanges();
            _accessLevelRightService.InsertRange(toSaveRightsModel);
            _accessLevelRightService.SaveChanges();
        }

        #endregion

        public ActionResult ProjectMenus(int projectId)
        {
            var schemeList = _projectMenuService.GetAllMenuByProjectId(projectId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("ProjectMenus", schemeList);
            }

            return View(schemeList);
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

        public ActionResult CreateAppMenu(int projectId)
        {
            var model = new ViewModels.Models.AssignMenuToApp.AssignMenuToAppViewModel();
            model.ProjectId = projectId;
            model.ProjectName = _projectService.GetAll().Where(x=>x.Id == projectId).Select(x=>x.Name).FirstOrDefault();
            return View(model);
        }

        public JsonResult Upload(string theIds, int projectId, string PrimaryColor, string SecoundaryColor)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var selectedMenus = js.Deserialize<IList<dynamic>>(theIds);

            List<Hands.Data.HandsDB.MultiProjectAppDetail> Access1 = _multiProjectAppDetailsService.GetAllMenuDetailByProjectId(projectId).ToList();
            if (Access1.Count > 0)
            {
                foreach (var Remove in Access1)
                {
                    _multiProjectAppDetailsService.Remove(Remove);
                }
            }
            List<Hands.Data.HandsDB.AssignMenuToApp> Access = _assignMenuToAppService.GetAllMenuByProjectId(projectId).ToList();
            if (Access.Count > 0)
            {
                foreach (var Remove in Access)
                {
                    _assignMenuToAppService.Remove(Remove);
                }
            }

            var parentIds = (from item in selectedMenus.ToList()
                             select item["original"]["parent"].ToString()).Distinct().ToList();
            var menuIds = (from item in selectedMenus.ToList()
                           select item["original"]["id"].ToString()).ToList();
            List<object> AllIds = parentIds.Union(menuIds).ToList();
            AllIds[0] = 0;
            var intList = AllIds.Select(Convert.ToInt32).ToList();


            var appmenus = _appMenuService.GetAll();
            var selectedMenusfromList = appmenus.Where(x => intList.Contains(x.Id)).ToList();

            foreach (var values in selectedMenusfromList)
            {
                var model = new Hands.Data.HandsDB.AssignMenuToApp();
                model.ProjectId = projectId;
                model.RoleName = "LHV";
                model.MenuId = values.Id;
                model.Title = values.Title;
                model.ParentId = values.ParentId;
                model.ClickEvent = "Open_" + values.Title;
                model.NavImg = values.NavImg;
                model.IsActive = true;
                _assignMenuToAppService.Insert(model);
            }
            _assignMenuToAppService.SaveChanges();

            MultiProjectAppDetail ModelToSAve = new MultiProjectAppDetail();

            var httpPostedFile = Request.Files[0];
            if (httpPostedFile.FileName != "")
            {
                var uploadFilesDir = System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/ApplicationImage");
                if (!Directory.Exists(uploadFilesDir))
                {
                    Directory.CreateDirectory(uploadFilesDir);
                }
                var fileSavePath = Path.Combine(uploadFilesDir, httpPostedFile.FileName);
                if (System.IO.File.Exists(fileSavePath))
                {
                    System.IO.File.Delete(fileSavePath);
                }
                httpPostedFile.SaveAs(fileSavePath);

                ModelToSAve.ProjectImagePath = $"{ConfigurationSettings.AppSettings["VirtualPath"]}/Content/Images/ApplicationImage/" + httpPostedFile.FileName;
                if (Request.Files.Count > 0)
                {
                    var httpPostedFileImage = Request.Files[0];
                    if (httpPostedFileImage.FileName != string.Empty)
                    {
                        if (httpPostedFileImage != null)
                        {
                            //var fileSavePathImage = Path.Combine(uploadFilesDir, httpPostedFileImage.FileName.Replace(" ", "_"));

                            ModelToSAve.ProjectImagePath = $"{ConfigurationSettings.AppSettings["VirtualPath"]}/Content/Images/ApplicationImage/" + httpPostedFile.FileName;
                        }
                    }
                }
            }
            ModelToSAve.ProjectId = projectId;
            ModelToSAve.PrimaryColor = PrimaryColor;
            ModelToSAve.SecoundaryColor = SecoundaryColor;
            ModelToSAve.HeadingColor = PrimaryColor;
            ModelToSAve.SubHeadingColor = PrimaryColor;
            ModelToSAve.IsActive = true;
            _multiProjectAppDetailsService.Insert(ModelToSAve);
            _multiProjectAppDetailsService.SaveChanges();

            return Json(null);
        }


        //[HttpPost]
        ////public ActionResult CreateAppMenu(string data1, int projectId, string PrimaryColor, string SecoundaryColor, string HeadingColor, string SubHeadingColor)
        //public ActionResult CreateAppMenu(string data1, int projectId, string PrimaryColor, string SecoundaryColor)
        //{

        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    var selectedMenus = js.Deserialize<IList<dynamic>>(data1);

        //    List<Hands.Data.HandsDB.MultiProjectAppDetail> Access1 = _multiProjectAppDetailsService.GetAllMenuDetailByProjectId(projectId).ToList();
        //    if (Access1.Count > 0)
        //    {
        //        foreach (var Remove in Access1)
        //        {
        //            _multiProjectAppDetailsService.Remove(Remove);
        //        }
        //    }
        //    List<Hands.Data.HandsDB.AssignMenuToApp> Access =  _assignMenuToAppService.GetAllMenuByProjectId(projectId).ToList();
        //    if (Access.Count > 0)
        //    {
        //        foreach (var Remove in Access)
        //        {
        //            _assignMenuToAppService.Remove(Remove);
        //        }
        //    }

        //    var parentIds = (from item in selectedMenus.ToList()
        //                     select item["original"]["parent"].ToString()).Distinct().ToList();
        //    var menuIds = (from item in selectedMenus.ToList()
        //                   select item["original"]["id"].ToString()).ToList();
        //    List<object> AllIds = parentIds.Union(menuIds).ToList();
        //    AllIds[0] = 0;
        //    var intList = AllIds.Select(Convert.ToInt32).ToList();


        //    var appmenus = _appMenuService.GetAll();
        //    var selectedMenusfromList = appmenus.Where(x => intList.Contains(x.Id)).ToList();

        //    foreach (var values in selectedMenusfromList)
        //    {
        //        var model = new Hands.Data.HandsDB.AssignMenuToApp();
        //        model.ProjectId = projectId;
        //        model.RoleName = "LHV";
        //        model.MenuId = values.Id;
        //        model.Title = values.Title;
        //        model.ParentId = values.ParentId;
        //        model.ClickEvent = "Open_" + values.Title;
        //        model.NavImg = values.NavImg;
        //        model.IsActive = true;
        //        _assignMenuToAppService.Insert(model);
        //    }

        //    {
        //        MultiProjectAppDetail ModelToSAve = new MultiProjectAppDetail();

        //        ModelToSAve.ProjectId = projectId;
        //        ModelToSAve.PrimaryColor = PrimaryColor;
        //        ModelToSAve.SecoundaryColor = SecoundaryColor;
        //        ModelToSAve.HeadingColor = PrimaryColor;
        //        ModelToSAve.SubHeadingColor = PrimaryColor;
        //        ModelToSAve.ProjectImagePath = "sdad";
        //        ModelToSAve.IsActive = true;
        //        _multiProjectAppDetailsService.Insert(ModelToSAve);
        //        _multiProjectAppDetailsService.SaveChanges();
        //    }

        //    _assignMenuToAppService.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        #region AppMenusRender

        /// <summary>
        /// this method is for render the menus list
        /// </summary>
        /// <returns></returns>
        public string AppMenuCategoryHierarchy()
        {
            var appMenus = _appMenuService.GetAll();
            var list = (
                from c in appMenus
                select new ViewModels.Models.AssignMenuToApp.AssignMenuToAppViewModel()
                {
                    id = c.Id.ToString(),
                    parent = c.ParentId == (int?)0 ? "#" : c.ParentId.ToString(),
                    text = c.Title
                }).ToList();
            return new JavaScriptSerializer().Serialize(list);
        }

        #endregion




    }
}