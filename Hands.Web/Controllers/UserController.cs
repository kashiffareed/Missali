using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Asp.netRoles;
using Hands.Service.Noor;
using Hands.Service.Role;
using Hands.Service.RoleMenuAccess;
using Hands.Service.User;
using Hands.ViewModels.Models;
using user = Hands.Data.HandsDB.User;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using System.IO;
using Excel;
using System.Data.SqlClient;
using Hands.Common.Common;
using Hands.Service.AspnetUsers;
using Hands.Service.AssignRoleToProject;
using Hands.Service.Project;
using Hands.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Hands.Web.Controllers
{
    public class UserController : ControllerBase
    {
        // GET: User
        private readonly IUserService _userService;
        private readonly INoorService _noorService;
        //private readonly IAspNetRolesService _aspNetRolesService;
        private readonly IRoleService _roleService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly AccountController _accountController;
        private readonly IAspnetUserService _aspnetUserService;
        private readonly IProjectService _projectService;
        private readonly IAssignRoleToProjectService _assignRoleToProjectService;

        public UserController()
        {
            _userService = new UserService();
            _noorService = new NoorService();
            _roleService = new RoleService();
            _accountController = new AccountController(_userManager, _signInManager);
            _aspnetUserService = new AspnetUserService();
            _projectService = new ProjectService();
            _assignRoleToProjectService = new AssignRoleToProjectService();

        }
        public ActionResult Index()
        {
            //HandsDBContext GVDB = new HandsDBContext();

            var schemeList = _userService.GetUsers().OrderByDescending(x => x.user_id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }
            return View("Index", schemeList);
        }

        public ActionResult Create()
        {
            var model = new ViewModels.Models.user();
            //model.Project = _projectService.GetAllActive().OrderByDescending(x => x.Id);
            model.Role = _roleService.GetRollByProjectId(HandSession.Current.ProjectId.ToInt());
            return View(model);
        }

        public int CreateForMultiProject(ViewModels.Models.user model)
        {
           
           
                AccountController _accountController =  new AccountController(_userManager, _signInManager);
                Data.HandsDB.User ModelToSAve = new Data.HandsDB.User();
                //ModelToSAve.UserId = model.UserId;
                ModelToSAve.UserName = model.UserName;
                ModelToSAve.FullName = model.UserName;
                ModelToSAve.Email = model.Email;
                ModelToSAve.Pwd = model.Pwd;
                ModelToSAve.PlainPassword = model.Pwd;
                ModelToSAve.ProjectId = model.ProjectId;
                ModelToSAve.RoleId = model.RoleId;
                ModelToSAve.Designation = model.Designation;
                ModelToSAve.IsActive = true;
                _userService.Insert(ModelToSAve);
                _userService.SaveChanges();
                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                //var result = UserManager.Create(user, model.Pwd);
                //var aspnetuser = _aspnetUserService.GetByEmailAspnetUser(model.Email);
                //aspnetuser.ProjectId = model.ProjectId;
                //_aspnetUserService.SaveChanges();
                return ModelToSAve.UserId;
           

        }

        public int EditForMultiProject(ViewModels.Models.user model)
        {


            //AccountController _accountController = new AccountController(_userManager, _signInManager);



            Data.HandsDB.User ModelToSAve = _userService.GetById(model.UserId);
            //Data.HandsDB.User ModelToSAve = new Data.HandsDB.User();
            //ModelToSAve.UserId = model.UserId;
            ModelToSAve.UserName = model.UserName;
            ModelToSAve.FullName = model.UserName;
            ModelToSAve.Email = model.Email;
            ModelToSAve.Pwd = model.Pwd;
            ModelToSAve.PlainPassword = model.Pwd;
            ModelToSAve.ProjectId = model.ProjectId;
            ModelToSAve.RoleId = model.RoleId;
            ModelToSAve.Designation = model.Designation;
            ModelToSAve.IsActive = true;
            _userService.Update(ModelToSAve);
            _userService.SaveChanges();
            //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            //var result = UserManager.Create(user, model.Pwd);
            //var aspnetuser = _aspnetUserService.GetByEmailAspnetUser(model.Email);
            //aspnetuser.ProjectId = model.ProjectId;
            //_aspnetUserService.SaveChanges();
            return ModelToSAve.UserId;


        }


        [HttpPost]
        public ActionResult Create(ViewModels.Models.user model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.User ModelToSAve = new Data.HandsDB.User();
                ModelToSAve.UserId = model.UserId;
                ModelToSAve.UserName = model.UserName;
                ModelToSAve.FullName = model.UserName;
                ModelToSAve.Email = model.Email;
                ModelToSAve.Pwd = model.Pwd;
                ModelToSAve.PlainPassword = model.Pwd;
                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                //ModelToSAve.ProjectId = model.ProjectId;
                ModelToSAve.RoleId = model.RoleId;
                ModelToSAve.Designation = model.Designation;
                ModelToSAve.IsActive = true;
                _userService.Insert(ModelToSAve);
                _userService.SaveChanges();
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = UserManager.Create(user, model.Pwd);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.User model = _userService.GetById(id);
            var Viewmodel = new ViewModels.Models.user();
            Viewmodel.UserId = model.UserId;
            Viewmodel.FullName = model.UserName;
            Viewmodel.UserName = model.UserName;
            Viewmodel.Email = model.Email;
            Viewmodel.Pwd = model.Pwd;
            //Viewmodel.ProjectId = model.ProjectId;
            Viewmodel.RoleId = model.RoleId;
            Viewmodel.IsActive = model.IsActive;
            Viewmodel.Project = _projectService.GetAllActive().OrderByDescending(x => x.Id);
            Viewmodel.IsActive = Convert.ToBoolean(model.IsActive);
            Viewmodel.Role = _roleService.GetRollByProjectId(HandSession.Current.ProjectId.ToInt());
            Viewmodel.Designation = model.Designation;
            return View(Viewmodel);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.Models.user model)
        {
            if (ModelState.IsValid)
            {
                user existingModel = _userService.GetById(model.UserId);
                if (existingModel != null)
                {
                    existingModel.FullName = model.UserName;
                    existingModel.UserName = model.UserName;
                    existingModel.Email = model.Email;
                    existingModel.Pwd = model.Pwd;
                    existingModel.PlainPassword = model.Pwd;
                    //existingModel.ProjectId = model.ProjectId;
                    existingModel.RoleId = model.RoleId;
                    existingModel.Designation = model.Designation;
                    existingModel.IsActive = model.IsActive;
                    _userService.Update(existingModel);
                    _userService.SaveChanges();
                    var aspnetuser = _aspnetUserService.GetByEmailAspnetUser(model.Email);
                    if (aspnetuser != null)
                    {
                        _aspnetUserService.Remove(aspnetuser);
                        _aspnetUserService.SaveChanges();
                    }

                    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var result = UserManager.Create(user, model.Pwd);

                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult View(int id)
        {
            Data.HandsDB.User model = _userService.GetById(id);
            var Viewmodel = new ViewModels.Models.user();
            Viewmodel.UserId = model.UserId;
            Viewmodel.FullName = model.FullName;
            Viewmodel.UserName = model.UserName;
            Viewmodel.Email = model.Email;
            Viewmodel.Pwd = model.Pwd;
            Viewmodel.ProjectId = model.ProjectId;
            Viewmodel.RoleId = model.RoleId;
            Viewmodel.IsActive = model.IsActive;
            Viewmodel.IsActive = Convert.ToBoolean(model.IsActive);
            Viewmodel.Role = _roleService.GetRollByProjectId(HandSession.Current.ProjectId.ToInt());
            Viewmodel.Designation = model.Designation;
            return View(Viewmodel);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                user existingModel = _userService.GetById(id);
                var aspnetuser = _aspnetUserService.GetByEmailAspnetUser(existingModel.Email);
                existingModel.IsActive = false;
                _userService.Update(existingModel);
                if (aspnetuser != null)
                {
                    _aspnetUserService.Remove(aspnetuser);
                    _aspnetUserService.SaveChanges();
                }
                _userService.SaveChanges();


            }

            return RedirectToAction("Index");

        }

        public JsonResult GetRoles(int projectId, string RoleId)
        {
            //todo fix logical issues

            List<SelectListItem> Role = new List<SelectListItem>();
            var Roles = _assignRoleToProjectService.GetRoleByProject(projectId);

            Role = new SelectList(Roles, "RoleId", "RoleName").ToList();
            return Json(new SelectList(Role, "Value", "Text", RoleId));
        }

        public JsonResult assignUserName(string userName)
        {
            bool doesExistAlready = _userService.GetAllActive().Any(o => o.UserName == userName);

            return Json(doesExistAlready);
        }
        public JsonResult assignEmail(string email)
        {
            bool doesExistAlready = _userService.GetAllActive().Any(o => o.Email == email);

            return Json(doesExistAlready);
        }

        [HttpGet]
        public ActionResult ExportIndex()
        {
            //string ExcelName = "test.xlsx";
            //string ZipName = "test.zip";
            // Using EPPlus from nuget
            using (var stream = new MemoryStream())
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    Int32 row = 2;
                    Int32 col = 1;

                    package.Workbook.Worksheets.Add("Data");
                    IGrid<Hands.Data.HandsDB.SpUsersReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpUsersReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpUsersReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpUsersReturnModel> grid = new Grid<Hands.Data.HandsDB.SpUsersReturnModel>(_userService.GetUsers().OrderByDescending(x => x.user_id));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.full_name).Titled("Full Name");
            grid.Columns.Add(model => model.user_name).Titled("User Name");
            grid.Columns.Add(model => model.pwd).Titled("Password");
            grid.Columns.Add(model => model.email).Titled("Email");
            grid.Columns.Add(model => model.role_name).Titled("Role");
            grid.Columns.Add(model => model.designation).Titled("Designation");
            grid.Columns.Add(model => model.IsActive).Titled("Active/InActive");
            grid.Columns.Add(model => model.created_at).Titled("Created Date");


            grid.Pager = new GridPager<Hands.Data.HandsDB.SpUsersReturnModel>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Pager.PageSizes = new Dictionary<int, string>()
            {
                {0 ,"All" }
            };
            grid.Pager.ShowPageSizes = true;

            foreach (IGridColumn column in grid.Columns)
            {
                column.Filter.IsEnabled = true;
                column.Sort.IsEnabled = true;
            }
            return grid;
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    try
                    {
                        var stream = upload.InputStream;
                        IExcelDataReader reader = null;
                        if (upload.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(upload.InputStream);
                        }
                        else if (upload.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        reader.IsFirstRowAsColumnNames = true;
                        var result = reader.AsDataSet();
                        reader.Close();


                        string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Hands.Web.Properties.Settings.HandsDBConnection"].ConnectionString;
                        SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
                        bulkInsert.DestinationTableName = "users";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_id", "user_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("full_name", "full_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_name", "user_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("pwd", "pwd"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("plain_password", "plain_password"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("email", "email"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_active", "is_active"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("role_id", "role_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("designation", "designation"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));

                        bulkInsert.WriteToServer(result.Tables["Data"]);

                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("Record", "No Records updated.");
                        ModelState.AddModelError("Record", e.Message);
                        //return View(iStatus);
                    }
                }
            }
            return View();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}