using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Noor;
using Hands.Service.Real;
using Hands.Service.Regions;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;
using Hands.ViewModels.Models;
using appUser = Hands.ViewModels.Models.real;
using System.IO;
using OfficeOpenXml;
using NonFactors.Mvc.Grid;
using Excel;
using System.Data.SqlClient;
using System.Windows.Media.Media3D;
using Hands.Common.Common;

namespace Hands.Web.Controllers
{
    public class RealController : ControllerBase
    {
        private readonly IRealService _realService;
        private readonly IRegionService _regionService;
        private readonly ITaluqaService _taluqaService;
        private readonly IUnionCouncilService _unionCouncilService;
        private readonly INoorService _noorService;

        public RealController()
        {
            _realService = new RealService();
            _regionService = new RegionService();
            _taluqaService = new TaluqaService();
            _unionCouncilService = new UnionCouncilService();
            _noorService = new NoorService();




        }

        public ActionResult Index(int? RegionId, int? TaluqaId, int? UnionCouncilId, int export = 0)
        {
            var model = new appUser();
            model.RealList = _realService.SearchLhv(RegionId, TaluqaId, UnionCouncilId).OrderByDescending(l => l.AppUserId);
            model.Search.Regions = _regionService.GetAll();
            model.RegionId = RegionId;
            model.TaluqaId = TaluqaId;
            model.Search.ControllerName = "Real";
            model.UnionCouncilId = UnionCouncilId;
            if (export == 1)
            {


                ExportController.ExportExcel(
                    ExportController.RenderViewToString(
                        ControllerContext,
                        "~/Views/Real/_RealListPartial.cshtml",
                        model),
                    "",
                    ""

                );
                return null;
            }

            return Request.IsAjaxRequest() ? (ActionResult)PartialView("Index", model) : View("Index", model);
            // GET: Noor
            //var schemeList = _realService.GetAllActive("realtime").OrderByDescending(x=>x.AppUserId);

            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("Index", schemeList);
            //}

            //return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new Realcreate();
            model.ProjectId = HandSession.Current.ProjectId;
            model.Regions = _regionService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            model.Marvis = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);

            return View(model);

        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.Realcreate model, HttpPostedFileBase file)
        {
           


            if (ModelState.IsValid)
            {
                Data.HandsDB.AppUser ModelToSAve = new Data.HandsDB.AppUser();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.AppUserId = model.AppUserId;
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
                ModelToSAve.UserType = "realtime";
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
                ModelToSAve.Organization = model.Organization;
                ModelToSAve.Designation = model.Designation;
                ModelToSAve.Email = model.Email;
                var httpPostedFile = Request.Files["file"];
                if (httpPostedFile.FileName != "")
                {
                    var uploadFilesDir =
                        System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/English");
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
                    ModelToSAve.PicturePath = $"{ConfigurationSettings.AppSettings["VirtualPath"]}/Content/Images/English/" + httpPostedFile.FileName;
                    if (Request.Files.Count > 0)
                    {
                        var httpPostedFileImage = Request.Files["file"];
                        if (httpPostedFileImage.FileName != string.Empty)
                        {
                            if (httpPostedFileImage != null)
                            {
                                var fileSavePathImage = Path.Combine(uploadFilesDir, httpPostedFileImage.FileName.Replace(" ", "_"));

                                model.PicturePath = fileSavePath;
                            }
                        }
                    }
                }
                _realService.Insert(ModelToSAve);
                _realService.SaveChanges();
                TempData["status"] = 1;
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.AppUser model = _realService.GetById(id);
            var Viewmodel = new ViewModels.Models.Realcreate();
            Viewmodel.ProjectId = HandSession.Current.ProjectId;
            Viewmodel.AppUserId = model.AppUserId;
            Viewmodel.FullName = model.FullName;
            Viewmodel.Pwd = model.Pwd;
            Viewmodel.Dob = model.Dob;
            Viewmodel.Username = model.Username;
            Viewmodel.Address = model.Address;
            Viewmodel.ContactNumber = model.ContactNumber;
            Viewmodel.MaritalStatus = model.MaritalStatus;
            Viewmodel.FatherHusbandName = model.FatherHusbandName;
            Viewmodel.AgePerCnic = model.AgePerCnic;
            Viewmodel.Cnic = model.Cnic;
            Viewmodel.CnicValidtyEnd = model.CnicValidtyEnd;
            Viewmodel.Qualification = model.Qualification;
            Viewmodel.Regions = _regionService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            Viewmodel.Marvis = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            Viewmodel.RegionId = model.RegionId;
            Viewmodel.TaluqaId = model.TaluqaId;
            Viewmodel.UnionCouncilId = model.UnionCouncilId;
            Viewmodel.NoOfHouseHolds = model.NoOfHouseHolds;
            Viewmodel.TargetMwras = model.TargetMwras;
            Viewmodel.PopulcationCovered = model.PopulcationCovered;
            Viewmodel.NearbyPublicFaculty = model.NearbyPublicFaculty;
            Viewmodel.NearbyPrivatefaculty = model.NearbyPrivateFaculty;
            Viewmodel.DateOfJoin = model.DateOfJoin;
            Viewmodel.dateOfTrain = model.DateOfTrain;
            Viewmodel.Organization = model.Organization;
            Viewmodel.Designation = model.Designation;
            Viewmodel.Email = model.Email;
            Viewmodel.PicturePath = model.PicturePath;
            return View(Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Realcreate model , HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                AppUser existingModel = _realService.GetById(model.AppUserId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.FullName = model.FullName;
                    existingModel.Pwd = model.Pwd;
                    existingModel.PlainPassword = model.Pwd;
                    existingModel.Dob = model.Dob;
                    existingModel.Username = model.Username;
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
                    existingModel.UserType = "realtime";
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
                    existingModel.Organization = model.Organization;
                    existingModel.Designation = model.Designation;
                    existingModel.Email = model.Email;
                    var httpPostedFile = Request.Files["file"];
                    if (httpPostedFile.FileName != "")
                    {
                        var uploadFilesDir =
                            System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/English");
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
                        existingModel.PicturePath = $"{ConfigurationSettings.AppSettings["VirtualPath"]}/Content/Images/English/" + httpPostedFile.FileName;
                        if (Request.Files.Count > 0)
                        {
                            var httpPostedFileImage = Request.Files["file"];
                            if (httpPostedFileImage.FileName != string.Empty)
                            {
                                if (httpPostedFileImage != null)
                                {
                                    var fileSavePathImage = Path.Combine(uploadFilesDir, httpPostedFileImage.FileName.Replace(" ", "_"));

                                    model.PicturePath = fileSavePath;
                                }
                            }
                        }
                    }
                    _realService.Update(existingModel);
                    _realService.SaveChanges();
                    TempData["status"] = 2;

                }


            }

            else
            {
                return Edit(model.AppUserId);
            }
            return RedirectToAction("Index");
        }

        public ActionResult View(int id)
        {

            Data.HandsDB.AppUser user = _realService.GetById(id);
            var model = new ViewModels.Models.real();

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


        #region Methods Cascade Dropdowns
        //public JsonResult GetStates(int regionId)
        //{
        //    //todo fix logical issues

        //    List<SelectListItem> Taluqa = new List<SelectListItem>();
        //    var taluqas = _taluqaService.GetAll(regionId);

        //    Taluqa = new SelectList(taluqas, "TaluqaId", "TaluqaName", regionId).ToList();
        //    return Json(new SelectList(Taluqa, "Value", "Text", regionId));
        //}
        public JsonResult GetStates(int regionId, int TaluqaId)
        {
            //todo fix logical issues

            List<SelectListItem> Taluqa = new List<SelectListItem>();
            var taluqas = _taluqaService.GetAll(regionId);

            Taluqa = new SelectList(taluqas, "TaluqaId", "TaluqaName", TaluqaId).ToList();
            return Json(new SelectList(Taluqa, "Value", "Text", TaluqaId));
        }
        public JsonResult GetUnions(int taluqaId, int unioncouncilId)
        {
            List<SelectListItem> UnionCounil = new List<SelectListItem>();


            UnionCounil = new SelectList(_unionCouncilService.GetAll(taluqaId), "UnionCouncilId", "UnionCouncilName", unioncouncilId).ToList();


            return Json(new SelectList(UnionCounil, "Value", "Text", unioncouncilId));
        }
        #endregion
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.AppUser existingModel = _realService.GetById(id);
                existingModel.IsActive = false;
                _realService.Update(existingModel);
                _realService.SaveChanges();
            }

            return RedirectToAction("Index");

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
            IGrid<Hands.Data.HandsDB.AppUser> grid = new Grid<Hands.Data.HandsDB.AppUser>(_realService.GetAllActive("realtime", HandSession.Current.ProjectId).OrderByDescending(x => x.AppUserId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.FullName).Titled("Full Name");
            grid.Columns.Add(model => model.Dob).Titled("D.O.B");
            grid.Columns.Add(model => model.Username).Titled("User Name");
            grid.Columns.Add(model => model.Pwd).Titled("Password");
            grid.Columns.Add(model => model.ContactNumber).Titled("Contact No");
            grid.Columns.Add(model => model.Address).Titled("Address");
            grid.Columns.Add(model => model.FatherHusbandName).Titled("Husband Name");
            grid.Columns.Add(model => model.AgePerCnic).Titled("Age as Per CNIC");
            grid.Columns.Add(model => model.Cnic).Titled("CNIC No");
            grid.Columns.Add(model => model.CnicValidtyEnd).Titled("CNIC Validity End");
            grid.Columns.Add(model => model.Organization).Titled("Organization");
            grid.Columns.Add(model => model.Designation).Titled("Designation");
            grid.Columns.Add(model => model.Email).Titled("Email");
            grid.Columns.Add(model => model.Qualification).Titled("Qualification");
            grid.Columns.Add(model => model.PopulcationCovered).Titled("Population Covered");
            grid.Columns.Add(model => model.NoOfHouseHolds).Titled("No Of House Holds");
            grid.Columns.Add(model => model.NearbyPublicFaculty).Titled("Near By Public Faculty");
            grid.Columns.Add(model => model.NearbyPrivateFaculty).Titled("Near By Private Faculty");
            grid.Columns.Add(model => model.DateOfJoin).Titled("Date Of Joining");
            grid.Columns.Add(model => model.DateOfTrain).Titled("Date Of Training");
             
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