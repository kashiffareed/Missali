using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Regions;
using Hands.Service.Noor;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;
using Hands.ViewModels.Models;
using appUser = Hands.ViewModels.Models.noor;
using System.IO;
using OfficeOpenXml;
using NonFactors.Mvc.Grid;
using Excel;
using System.Data.SqlClient;
using Hands.Common.Common;

namespace Hands.Web.Controllers
{
    public class MarviController : ControllerBase
    {

        private readonly IRegionService _regionService;
        private readonly INoorService _noorService;
        private readonly ITaluqaService _taluqaService;
        private readonly IUnionCouncilService _unionCouncilService;


        public MarviController()
        {
            _taluqaService = new TaluqaService();
            _unionCouncilService = new UnionCouncilService();
            _regionService = new RegionService();
            _noorService = new NoorService();

        }

        public ActionResult Index(int? RegionId, int? TaluqaId, int? UnionCouncilId, int export = 0)
        {
            var model = new noor();
            model.NoorList = _noorService.SearchLhv(RegionId, TaluqaId, UnionCouncilId).OrderByDescending(l => l.app_user_id);
            model.Search.Regions = _regionService.GetAll();
            model.RegionId = RegionId;
            model.TaluqaId = TaluqaId;
            model.Search.ControllerName = "Marvi";
            model.UnionCouncilId = UnionCouncilId;
            if (export == 1)
            {


                ExportController.ExportExcel(
                    ExportController.RenderViewToString(
                        ControllerContext,
                        "~/Views/Marvi/_NoorListPartial.cshtml",
                        model),
                    "Noor LISTING",
                    "Noor LISTING"

                );
                return null;
            }

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("Index", model) : View("Index", model);
            //{
            //    // GET: Noor
            //    var SchemeList = _noorService.GetAllActive("marvi").OrderByDescending(x=>x.AppUserId);
            //    if (Request.IsAjaxRequest())
            //    {
            //        return PartialView("Index", SchemeList);
            //    }

            //    return View("Index", SchemeList);
        }

        public ActionResult Create()
        {

            var model = new noor();
            model.Regions = _regionService.GetAll().Where(x=>x.ProjectId == HandSession.Current.ProjectId);
            model.Marvis = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            model.Lhvs = _noorService.GetAllActiveLHV();
            model.ProjectId = HandSession.Current.ProjectId;
            return View(model);

        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.noor model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.AppUser ModelToSAve = new Data.HandsDB.AppUser();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.FullName = model.FullName;
                ModelToSAve.Username = model.Username;
                ModelToSAve.Pwd = model.Pwd;
                ModelToSAve.PlainPassword = model.Pwd;
                ModelToSAve.Dob = model.Dob;
                ModelToSAve.Address = model.Address;
                ModelToSAve.ContactNumber = model.ContactNumber;
                ModelToSAve.MaritalStatus = model.MaritalStatus;
                ModelToSAve.FatherHusbandName = model.FatherHusbandName;
                ModelToSAve.AgePerCnic = model.AgePerCnic;
                ModelToSAve.Cnic = model.Cnic;
                ModelToSAve.CnicValidtyEnd = model.CnicValidtyEnd;
                ModelToSAve.Qualification = model.Qualification;
                ModelToSAve.RegionId = model.RegionId ?? 0;
                ModelToSAve.TaluqaId = model.TaluqaId ?? 0;
                ModelToSAve.UnionCouncilId = model.UnionCouncilId ?? 0;
                ModelToSAve.UserType = "marvi";
                ModelToSAve.LhvAssigned = model.LhvId;
                ModelToSAve.FullNameSindhi = model.FullName;
                ModelToSAve.FullNameUrdu = model.FullName;
                ModelToSAve.IsActive = true;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.PopulcationCovered = model.PopulcationCovered;
                ModelToSAve.NoOfHouseHolds = model.NoOfHouseHolds;
                ModelToSAve.TargetMwras = model.TargetMwras;
                ModelToSAve.NearbyPublicFaculty = model.NearbyPublicFaculty;
                ModelToSAve.NearbyPrivateFaculty = model.NearbyPrivatefaculty;
                ModelToSAve.DateOfJoin = model.DateOfJoin;
                ModelToSAve.DateOfTrain = model.dateOfTrain;
                ModelToSAve.Latitude = model.Latitude;
                ModelToSAve.Longitude = model.Longitude;
                ModelToSAve.CurrentStatusOfService = model.CurrentStatusOfService;

                _noorService.Insert(ModelToSAve);
                _noorService.SaveChanges();

            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.AppUser model = _noorService.GetById(id);
            var Viewmodel = new ViewModels.Models.noor();
            Viewmodel.ProjectId = HandSession.Current.ProjectId;
            Viewmodel.AppUserId = model.AppUserId;
            Viewmodel.Username = model.Username;
            Viewmodel.FullName = model.FullName;
            Viewmodel.Pwd = model.Pwd;
            Viewmodel.Dob = model.Dob;
            Viewmodel.Address = model.Address;
            Viewmodel.ContactNumber = model.ContactNumber;
            Viewmodel.MaritalStatus = model.MaritalStatus;
            Viewmodel.FatherHusbandName = model.FatherHusbandName;
            Viewmodel.AgePerCnic = model.AgePerCnic;
            Viewmodel.Cnic = model.Cnic;
            Viewmodel.CnicValidtyEnd = model.CnicValidtyEnd;
            Viewmodel.Qualification = model.Qualification;
            Viewmodel.LhvId = model.LhvAssigned ?? 0;
            Viewmodel.RegionId = model.RegionId;
            Viewmodel.TaluqaId = model.TaluqaId;
            Viewmodel.UnionCouncilId = model.UnionCouncilId;
            Viewmodel.Marvis = _noorService.GetAll().Where(x=>x.ProjectId == HandSession.Current.ProjectId);
            Viewmodel.Regions = _regionService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            Viewmodel.Lhvs = _noorService.GetAllActiveLHV();
            Viewmodel.NoOfHouseHolds = model.NoOfHouseHolds;
            Viewmodel.TargetMwras = model.TargetMwras;
            Viewmodel.NearbyPublicFaculty = model.NearbyPublicFaculty;
            Viewmodel.NearbyPrivatefaculty = model.NearbyPrivateFaculty;
            Viewmodel.DateOfJoin = model.DateOfJoin;
            Viewmodel.dateOfTrain = model.DateOfTrain;
            Viewmodel.Latitude = model.Latitude;
            Viewmodel.Longitude = model.Longitude;
            Viewmodel.CurrentStatusOfService = model.CurrentStatusOfService;
            return PartialView("Edit", Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.noor model)
        {
            if (ModelState.IsValid)
            {
                AppUser existingModel = _noorService.GetById(model.AppUserId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.AppUserId = model.AppUserId;
                    existingModel.FullName = model.FullName;
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
                    existingModel.RegionId = model.RegionId ?? 0;
                    existingModel.TaluqaId = model.TaluqaId ?? 0;
                    existingModel.UnionCouncilId = model.UnionCouncilId ?? 0;
                    existingModel.LhvAssigned = model.LhvId;
                    existingModel.UserType = "marvi";
                    existingModel.LhvAssigned = model.LhvId;
                    existingModel.FullNameSindhi = model.FullName;
                    existingModel.FullNameUrdu = model.FullName;
                    existingModel.IsActive = true;
                    existingModel.PopulcationCovered = model.PopulcationCovered;
                    existingModel.PopulcationCovered = model.PopulcationCovered;
                    existingModel.NoOfHouseHolds = model.NoOfHouseHolds;
                    existingModel.TargetMwras = model.TargetMwras;
                    existingModel.NearbyPublicFaculty = model.NearbyPublicFaculty;
                    existingModel.NearbyPrivateFaculty = model.NearbyPrivatefaculty;
                    existingModel.DateOfJoin = model.DateOfJoin;
                    existingModel.DateOfTrain = model.dateOfTrain;
                    existingModel.CurrentStatusOfService = model.CurrentStatusOfService;
                    existingModel.Latitude = model.Latitude;
                    existingModel.Longitude = model.Longitude;
                    _noorService.Update(existingModel);
                    _noorService.SaveChanges();
                }
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
                Data.HandsDB.AppUser existingModel = _noorService.GetById(id);
                existingModel.IsActive = false;
                _noorService.Update(existingModel);
                _noorService.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        public ActionResult View(int id)
        {

            Data.HandsDB.AppUser user = _noorService.GetById(id);
            var model = new ViewModels.Models.noor();

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
            model.Qualification = user.Qualification;
            model.LhvAssigned = user.LhvAssigned ?? 0;
            model.TotalMarviAssigned = user.TotalMarviAssigned ?? 0;
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
            var projectId = HandSession.Current.ProjectId;
            IGrid <Hands.Data.HandsDB.AppUser > grid = new Grid<Hands.Data.HandsDB.AppUser>(_noorService.GetAll().Where(x=>x.IsActive && x.UserType == "marvi" && x.ProjectId == projectId).OrderByDescending(x => x.AppUserId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.LhvAssigned).Titled("Name Of Supervisor LHV");
            grid.Columns.Add(model => model.FullName).Titled("Name Of Marvi Worker");
            grid.Columns.Add(model => model.MaritalStatus).Titled("Current Martial Status");
            grid.Columns.Add(model => model.FatherHusbandName).Titled("Father/Husband Name");
            grid.Columns.Add(model => model.Dob).Titled("D.O.B");
            grid.Columns.Add(model => model.AgePerCnic).Titled("Age As Per CNIC");
            grid.Columns.Add(model => model.Cnic).Titled("CNIC No");
            grid.Columns.Add(model => model.CnicValidtyEnd).Titled("CNIC Validity End");
            grid.Columns.Add(model => model.ContactNumber).Titled("Contact No");
            grid.Columns.Add(model => model.Qualification).Titled("Qualification");
            grid.Columns.Add(model => model.DateOfJoin).Titled("Date Of Joining");
            grid.Columns.Add(model => model.DateOfTrain).Titled(" Date Of Training");
            grid.Columns.Add(model => model.CurrentStatusOfService).Titled("Current Status Of Service");
            grid.Columns.Add(model => model.Address).Titled("Village Name");
            grid.Columns.Add(model => model.RegionId).Titled("District");
            grid.Columns.Add(model => model.TaluqaId).Titled("Taluqa");
            grid.Columns.Add(model => model.UnionCouncilId).Titled("Union Council");
            grid.Columns.Add(model => model.PopulcationCovered).Titled("Population Covered");
            grid.Columns.Add(model => model.NoOfHouseHolds).Titled("No Of House Hold");
            grid.Columns.Add(model => model.TargetMwras).Titled("Target MWRAs");
            grid.Columns.Add(model => model.Longitude).Titled("Longitude");
            grid.Columns.Add(model => model.Latitude).Titled("Latitude");
            grid.Columns.Add(model => model.NearbyPublicFaculty).Titled("Nearby Public Health Facility");
            grid.Columns.Add(model => model.NearbyPrivateFaculty).Titled("Nearby Private Health Faculty");
                    
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