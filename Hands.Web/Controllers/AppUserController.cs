using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.Noor;
using Hands.Service.Regions;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;
using Hands.ViewModels.Models;
using Microsoft.Ajax.Utilities;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using appUser = Hands.ViewModels.Models.appUser;

namespace Hands.Web.Controllers
{

    [Authorize]
    public class AppUserController : ControllerBase
    {


        private readonly IAppUserService _appUserService;
        private readonly IRegionService _regionService;
        private readonly INoorService _noorService;
        private readonly ITaluqaService _taluqaService;
        private readonly IUnionCouncilService _unionCouncilService;

        public AppUserController()
        {
            _appUserService = new AppUserService();
            _regionService = new RegionService();
            _noorService = new NoorService();
            _taluqaService = new TaluqaService();
            _unionCouncilService = new UnionCouncilService();

        }

        public ActionResult Index(int? RegionId, int? TaluqaId, int? unionCouncilId, int export = 0)
        {
            // GET: AppUser
            var model = new appUser();
            model.APpUSerList = _appUserService.SearchLhv(RegionId, TaluqaId, unionCouncilId).OrderByDescending(l => l.AppUserId);
            model.Search.Regions = _regionService.GetAll();
            model.Search.ControllerName = "AppUser";


            if (export == 1)
            {


                ExportController.ExportExcel(
                    ExportController.RenderViewToString(
                        ControllerContext,
                        "~/Views/AppUser/_LhvListPartial.cshtml",
                       model),
                    "",
                    ""

                );
                return null;
            }


            return Request.IsAjaxRequest() ? (ActionResult)PartialView("Index", model) : View("Index", model);




        }



        public ActionResult Create()
        {
            var model = new appUser();
            model.Regions = _regionService.GetAll();
            model.Marvis = _noorService.GetAll();
            model.ProjectId = HandSession.Current.ProjectId;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.appUser model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.AppUser ModelToSAve = new Data.HandsDB.AppUser();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.FullName = model.FullName;
                ModelToSAve.Username = model.Username;
                ModelToSAve.Pwd = model.Pwd;
                ModelToSAve.Dob = model.Dob;
                ModelToSAve.Address = model.Address;
                ModelToSAve.ContactNumber = model.ContactNumber;
                ModelToSAve.MaritalStatus = model.MaritalStatus;
                ModelToSAve.FatherHusbandName = model.FatherHusbandName;
                ModelToSAve.AgePerCnic = model.AgePerCnic;
                ModelToSAve.Cnic = model.Cnic;
                ModelToSAve.CnicValidtyEnd = model.CnicValidtyEnd;
                ModelToSAve.Qualification = model.Qualification;
                //ModelToSAve.RegionId = model.RegionId.Value;
                //ModelToSAve.TaluqaId = model.TaluqaId.Value;
                //ModelToSAve.UnionCouncilId = model.UnionCouncilId.Value;
                ModelToSAve.UserType = "lhv";
                ModelToSAve.FullNameSindhi = model.FullName;
                ModelToSAve.FullNameUrdu = model.FullName;
                ModelToSAve.PlainPassword = model.Pwd;
                ModelToSAve.IsActive = true;
                ModelToSAve.PopulcationCovered = model.PopulcationCovered;
                ModelToSAve.NoOfHouseHolds = model.NoOfHouseHolds;
                ModelToSAve.TargetMwras = model.TargetMwras;
                ModelToSAve.NearbyPublicFaculty = model.NearbyPublicFaculty;
                ModelToSAve.NearbyPrivateFaculty = model.NearbyPrivatefaculty;
                ModelToSAve.DateOfJoin = model.DateOfJoin;
                ModelToSAve.DateOfTrain = model.dateOfTrain;
                ModelToSAve.CreatedAt = DateTime.Now;
                _appUserService.Insert(ModelToSAve);
                _appUserService.SaveChanges();
            }

            return RedirectToAction("Index", "AppUser");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.AppUser user = _appUserService.GetById(id);
            var model = new ViewModels.Models.appUser();
            model.ProjectId = HandSession.Current.ProjectId;
            model.AppUserId = user.AppUserId;
            model.FullName = user.FullName;
            model.Username = user.Username;
            model.Pwd = user.Pwd;
            model.Dob = user.Dob;
            model.Address = user.Address;
            model.ContactNumber = user.ContactNumber;
            model.MaritalStatus = user.MaritalStatus;
            model.FatherHusbandName = user.FatherHusbandName;
            model.AgePerCnic = user.AgePerCnic;
            model.Cnic = user.Cnic;
            model.CnicValidtyEnd = user.CnicValidtyEnd;
            model.Qualification = user.Qualification;
            model.Regions = _regionService.GetAll();
            model.Marvis = _noorService.GetAll();
            model.RegionId = user.RegionId;
            model.TaluqaId = user.TaluqaId;
            model.CreatedAt = DateTime.Now;
            model.PopulcationCovered = user.PopulcationCovered;
            model.UnionCouncilId = user.UnionCouncilId;
            model.NoOfHouseHolds = user.NoOfHouseHolds;
            model.TargetMwras = user.TargetMwras;
            model.NearbyPublicFaculty = user.NearbyPublicFaculty;
            model.NearbyPrivatefaculty = user.NearbyPrivateFaculty;
            model.DateOfJoin = user.DateOfJoin;
            model.dateOfTrain = user.DateOfTrain;
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.appUser model)
        {
            if (ModelState.IsValid)
            {
                AppUser existingModel = _appUserService.GetById(model.AppUserId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.FullName = model.FullName;
                    existingModel.Username = model.Username;
                    existingModel.Pwd = model.Pwd;
                    existingModel.PlainPassword = model.Pwd;
                    existingModel.Dob = model.Dob;
                    existingModel.Address = model.Address;
                    existingModel.ContactNumber = model.ContactNumber;
                    existingModel.MaritalStatus = model.MaritalStatus;
                    existingModel.FatherHusbandName = model.FatherHusbandName;
                    existingModel.AgePerCnic = model.AgePerCnic;
                    existingModel.Cnic = model.Cnic;
                    existingModel.CnicValidtyEnd = model.CnicValidtyEnd;
                    existingModel.Qualification = model.Qualification;
                    //existingModel.RegionId = model.RegionId.Value;
                    //existingModel.TaluqaId = model.TaluqaId.Value;
                    //existingModel.UnionCouncilId = model.UnionCouncilId.Value;
                    existingModel.UserType = "lhv";
                    existingModel.IsActive = true;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.PopulcationCovered = model.PopulcationCovered;
                    existingModel.PopulcationCovered = model.PopulcationCovered;
                    existingModel.NoOfHouseHolds = model.NoOfHouseHolds;
                    existingModel.TargetMwras = model.TargetMwras;
                    existingModel.NearbyPublicFaculty = model.NearbyPublicFaculty;
                    existingModel.NearbyPrivateFaculty = model.NearbyPrivatefaculty;
                    existingModel.DateOfJoin = model.DateOfJoin;
                    existingModel.DateOfTrain = model.dateOfTrain;
                    _appUserService.Update(existingModel);
                    _appUserService.SaveChanges();
                }
            }
            else
            {
                return Edit(model.AppUserId);
            }
            return RedirectToAction("Index");
        }


        #region Methods Cascade Dropdowns
        public JsonResult GetStates(int regionId, int TaluqaId)
        {
            //todo fix logical issues

            List<SelectListItem> Taluqa = new List<SelectListItem>();
            var taluqas = _taluqaService.GetAll(regionId);

            Taluqa = new SelectList(taluqas, "TaluqaId", "TaluqaName", TaluqaId).ToList();
            return Json(new SelectList(Taluqa, "Value", "Text", TaluqaId));
        }
        public JsonResult GetUnions(int taluqaId, int UnionCouncilId)
        {
            List<SelectListItem> UnionCounil = new List<SelectListItem>();


            UnionCounil = new SelectList(_unionCouncilService.GetAll(taluqaId), "UnionCouncilId", "UnionCouncilName", UnionCouncilId).ToList();


            return Json(new SelectList(UnionCounil, "Value", "Text", UnionCouncilId));
        }
        #endregion
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.AppUser existingModel = _appUserService.GetById(id);
                existingModel.IsActive = false;
                _appUserService.Update(existingModel);
                _appUserService.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult View(int id)
        {
            Data.HandsDB.AppUser user = _appUserService.GetById(id);
            var model = new ViewModels.Models.appUser();

            model.ProjectId = HandSession.Current.ProjectId;
            model.AppUserId = user.AppUserId;
            model.FullName = user.FullName;
            model.FullNameUrdu = user.FullNameUrdu;
            model.FullNameSindhi = user.FullNameSindhi;
            model.Dob = user.Dob;
            model.RegionId = user.RegionId;
            model.TaluqaId = user.TaluqaId;
            model.UnionCouncilId = user.UnionCouncilId;
            model.Username = user.Username;
            model.Pwd = user.Pwd;
            model.PlainPassword = user.PlainPassword;
            model.Address = user.Address;
            model.ContactNumber = user.ContactNumber;
            model.MaritalStatus = user.MaritalStatus;
            model.FatherHusbandName = user.FatherHusbandName;
            model.AgePerCnic = user.AgePerCnic;
            model.Cnic = user.Cnic;
            model.CnicValidtyEnd = user.CnicValidtyEnd;
            //model.Qualification = user.Qualification;
            //model.LhvAssigned = user.LhvAssigned ?? 0;
            //model.TotalMarviAssigned = user.TotalMarviAssigned ?? 0;
            model.UserType = user.UserType;
            model.PopulcationCovered = user.PopulcationCovered;
            model.NoOfHouseHolds = user.NoOfHouseHolds;
            model.TargetMwras = user.TargetMwras;
            model.NearbyPublicFaculty = user.NearbyPublicFaculty;
            model.NearbyPrivatefaculty = user.NearbyPrivateFaculty;
            model.DateOfJoin = user.DateOfJoin;
            model.dateOfTrain = user.DateOfTrain;
            model.CreatedAt = user.CreatedAt;

            return View(model);
        }

        [HttpGet]
        public ActionResult ExportIndex()
        {
            // Using EPPlus from nuget
            using (var stream = new MemoryStream())
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    Int32 row = 2;
                    Int32 col = 1;

                    package.Workbook.Worksheets.Add("Data");
                    IGrid<Hands.Data.HandsDB.AppUser> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.AppUser> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.AppUser> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.AppUser> grid = new Grid<Hands.Data.HandsDB.AppUser>(_appUserService.GetAllActive("lhv",HandSession.Current.ProjectId).OrderByDescending(x => x.AppUserId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.FullName).Titled("Name Of LHV");
            grid.Columns.Add(model => model.MaritalStatus).Titled("Current Martial Status");
            grid.Columns.Add(model => model.FatherHusbandName).Titled("Father/Husband Name");
            grid.Columns.Add(model => model.Dob).Titled("D.O.B");
            grid.Columns.Add(model => model.AgePerCnic).Titled("Age Per CNIC");
            grid.Columns.Add(model => model.Cnic).Titled("CNIC No");
            grid.Columns.Add(model => model.CnicValidtyEnd).Titled("CNIC Validity End");
            grid.Columns.Add(model => model.ContactNumber).Titled("Contact Number");
            grid.Columns.Add(model => model.Qualification).Titled("Qualification");
            grid.Columns.Add(model => model.TotalMarviAssigned).Titled("No Of Marvi Worker");
            grid.Columns.Add(model => model.PopulcationCovered).Titled("Population Covered");
              
            grid.Pager = new GridPager<Hands.Data.HandsDB.AppUser>(grid);
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
                        bulkInsert.DestinationTableName = "app_users";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("app_user_id", "app_user_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("full_name", "full_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("full_name_urdu", "full_name_urdu"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("full_name_sindhi", "full_name_sindhi"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("dob", "dob"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("region_id", "region_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("taluqa_id", "taluqa_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("union_council_id", "union_council_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("username", "username"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("pwd", "pwd"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("plain_password", "plain_password"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("address", "address"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("contact_number", "contact_number"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("marital_status", "marital_status"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("father_husband_name", "father_husband_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("age_per_cnic", "age_per_cnic"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("cnic", "cnic"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("cnic_validty_end", "cnic_validty_end"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("qualification", "qualification"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("lhv_assigned", "lhv_assigned"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("total_marvi_assigned", "total_marvi_assigned"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_type", "user_type"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("populcation_covered", "populcation_covered"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("no_of_house_holds", "no_of_house_holds"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("target_mwras", "target_mwras"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("nearby_public_faculty", "nearby_public_faculty"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("nearby_private_faculty", "nearby_private_faculty"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("date_of_join", "date_of_join"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("date_of_train", "date_of_train"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("IsActive", "IsActive"));

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

    }
}