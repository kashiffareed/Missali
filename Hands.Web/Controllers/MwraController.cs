using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Excel;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.Mwra;
using Hands.Service.MwraClientListing;
using Hands.Service.MwraClientNew;
using Hands.Service.NewMwraClientListing;
using Hands.Service.Noor;
using Hands.Service.Regions;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;


using Mwra = Hands.ViewModels.Models.Mwra;

namespace Hands.Web.Controllers
{
    public class MwraController : ControllerBase
    {
        // GET: Default
        private readonly IMwraClientListingService _mwraClientListingService;
        private readonly IMwraService _mwraService;
        private readonly IRegionService _regionService;
        private readonly ITaluqaService _taluqaService;
        private readonly INoorService _noorService;
        private readonly IUnionCouncilService _unionCouncilService;
        private readonly IMwraClientNewService _mwraClientNewService;
        private readonly IAppUserService _appUserService;

        //static int unioncouncilID;
        public MwraController()
        {
            _mwraService = new MwraService();
            _regionService = new RegionService();
            _taluqaService = new TaluqaService();
            _noorService = new NoorService();
            _unionCouncilService = new UnionCouncilService();
            _mwraClientListingService = new MwraClientListingService();
            _mwraClientNewService = new MwraClientService();
            _appUserService = new AppUserService();


        }
        
        public ActionResult Index(int? RegionId, int? TaluqaId, int? UnionCouncilId, int export = 0)
        {
            var dd = HandSession.Current.AccessList.Any(x =>
                x.ProjectId == HandSession.Current.ProjectId && x.RoleId == HandSession.Current.RoleId && x.MenuId== CommonConstant.MenuList.MWRA.ToInt()&&
                x.AccessLevelId == CommonConstant.RightLevelEnum.create.ToInt());




            var model = new Mwra();
            model.MwraListt = _mwraService.SearchMwra(RegionId, TaluqaId, UnionCouncilId).OrderByDescending(x => x.mwra_id);
            model.Search.ControllerName = "Mwra";
            model.Search.Regions = _regionService.GetAll();
            model.Search.RegionId= RegionId;
            model.Search.TaluqaId = TaluqaId;
            model.Search.UnionCouncilId = UnionCouncilId;
            if (export == 1)
            {
                ExportController.ExportExcel(
                    ExportController.RenderViewToString(
                        ControllerContext,
                        "~/Views/Mwra/_MwraListing.cshtml",
                        model),
                    "",
                    ""

                );
                return null;
            }


            return Request.IsAjaxRequest() ? (ActionResult)PartialView("Index", model) : View("Index", model);

        }
        public ActionResult MwraClientLisingActionResult(int? regionId, int? taluqaId, int? unionCouncilId, int export = 0)
        {
            var model = new Mwra();
            model.MwrasClientListt = _mwraService.SearchMwraClient(0,regionId, taluqaId, unionCouncilId).OrderByDescending(x => x.mwra_client_id);
            model.Search.Regions = _regionService.GetAll();
            model.RegionId = regionId;
            model.TaluqaId = taluqaId;
            model.Search.ControllerName = "Mwra";
            model.Search.ViewName = "MwraClientLisingActionResult";
            model.UnionCouncilId = unionCouncilId;
            if (export == 1)
            {
                ExportController.ExportExcel(
                    ExportController.RenderViewToString(
                        ControllerContext,
                        "~/Views/Mwra/_MwraClientLisingPartial.cshtml",
                        model),
                    "",
                    ""

                );
                return null;
            }


            return Request.IsAjaxRequest() ? (ActionResult)PartialView("MwraClientLisingActionResult", model) : View("MwraClientLisingActionResult", model);


            //var SchemeList = _mwraService.GetAllMwraClientList(0);
            //if (Request.IsAjaxRequest())
            //{
            //    return PartialView("MwraClientLisingActionResult", SchemeList);
            //}
            //return View(SchemeList);

        }

        public ActionResult MwraDropOutClientLisingActionResult(int? regionId, int? taluqaId, int? unionCouncilId, int export = 0)
        {
            var model = new Mwra();
            model.DropOutClientListing = _mwraService.SearchMwraDropOutClient(0, regionId, taluqaId, unionCouncilId).OrderByDescending(x => x.mwra_client_id);
            model.Search.Regions = _regionService.GetAll();
            model.RegionId = regionId;
            model.TaluqaId = taluqaId;
            model.Search.ControllerName = "Mwra";
            model.Search.ViewName = "MwraDropOutClientLisingActionResult";
            model.UnionCouncilId = unionCouncilId;
            if (export == 1)
            {
                ExportController.ExportExcel(
                    ExportController.RenderViewToString(
                        ControllerContext,
                        "~/Views/Mwra/_MwraClientLisingPartial.cshtml",
                        model),
                    "",
                    ""

                );
                return null;
            }


            return Request.IsAjaxRequest() ? (ActionResult)PartialView("MwraDropOutClientLisingActionResult", model) : View("MwraDropOutClientLisingActionResult", model);

        }

        public ActionResult Create()
        {
            var model = new Mwra();
            model.ProjectId = HandSession.Current.ProjectId;
            model.Regions = _regionService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            model.Marvis = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId && x.UserType == "marvi");
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Mwra model)
        {
            if (ModelState.IsValid)
            {
                var mwra = model.GetMwraEntity();
                mwra.ProjectId = HandSession.Current.ProjectId;
                _mwraService.Insert(mwra);
                _mwraService.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Data.HandsDB.Mwra model = _mwraService.GetById(id);
            var Viewmodel = new ViewModels.Models.Mwra();
            Viewmodel.ProjectId = HandSession.Current.ProjectId;
            Viewmodel.MwraId = model.MwraId;
            Viewmodel.AssignedMarviId = model.AssignedMarviId;
            Viewmodel.Name = model.Name;
            Viewmodel.Dob = model.Dob;
            Viewmodel.Address = model.Address;
            Viewmodel.ContactNumber = model.ContactNumber;
            Viewmodel.MaritialStatus = model.MaritialStatus;
            Viewmodel.Cnic = model.Cnic;
            Viewmodel.Dob = model.Dob;
            Viewmodel.HusbandName = model.HusbandName;
            Viewmodel.Age = model.Age;
            Viewmodel.Occupation = model.Occupation;
            Viewmodel.ContactNumber = model.ContactNumber;
            Viewmodel.RegionId = model.RegionId;
            Viewmodel.TaluqaId = model.TaluqaId;
            Viewmodel.UnionCouncilId = model.UnionCouncilId;
            Viewmodel.Latitude = model.Latitude;
            Viewmodel.Longitude = model.Longitude;
            Viewmodel.IsClient = model.IsClient;
            Viewmodel.EducationOfClass = model.EducationOfClass;
            Viewmodel.DurationOfMarriage = model.DurationOfMarriage;
            Viewmodel.CrrentlyPregnant =model.CrrentlyPregnant ;
            Viewmodel.PregnantNoOfMonths = model.PregnantNoOfMonths;
            Viewmodel.NoOfAliveChildren = model.NoOfAliveChildren;
            Viewmodel.NoOfAbortion = model.NoOfAbortion;
            Viewmodel.NoOfChildrenDied = model.NoOfChildrenDied;
            Viewmodel.ReasonOfDeath =model.ReasonOfDeath;
            Viewmodel.AgeOfYoungestChildMonths = model.AgeOfYoungestChildMonths;
            Viewmodel.AgeOfYoungestChildYears = model.AgeOfYoungestChildYears;
            Viewmodel.HaveUsedFpMethod = model.HaveUsedFpMethod;
            Viewmodel.NameOfFp = model.NameOfFp;
            Viewmodel.FpNotUsedInYears = model.FpNotUsedInYears;
            Viewmodel.ReasonOfDiscontinuation = model.ReasonOfDiscontinuation;
            Viewmodel.FpNoReason = model.FpNoReason;
            Viewmodel.WantToUseFp = model.WantToUseFp;
            Viewmodel.FpPurpose = model.FpPurpose;
            Viewmodel.IsUserFp = model.IsUserFp;
            Viewmodel.FpMethodUsed = model.FpMethodUsed;
            Viewmodel.DateOfRegistration = model.DateOfRegistration;
            Viewmodel.Regions = _regionService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            Viewmodel.Marvis = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId && x.UserType == "marvi");
            return PartialView("Edit", Viewmodel);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Mwra model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Mwra existingModel = _mwraService.GetById(model.MwraId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.MwraId = model.MwraId;
                    existingModel.AssignedMarviId = model.AssignedMarviId;
                    existingModel.Name = model.Name;
                    existingModel.Dob = model.Dob;
                    existingModel.ContactNumber = model.ContactNumber;
                    existingModel.MaritialStatus = "";
                    existingModel.Cnic = model.Cnic;
                    existingModel.HusbandName = model.HusbandName;
                    existingModel.Age = model.Age;
                    existingModel.Occupation = model.Occupation;
                    existingModel.RegionId = model.RegionId;
                    existingModel.TaluqaId = model.TaluqaId;
                    existingModel.UnionCouncilId = model.UnionCouncilId;
                    existingModel.Latitude = model.Latitude;
                    existingModel.Longitude = model.Longitude;
                    existingModel.IsClient = model.IsClient;
                    existingModel.EducationOfClass = model.EducationOfClass;
                    existingModel.DurationOfMarriage = model.DurationOfMarriage;
                    existingModel.CrrentlyPregnant = model.CrrentlyPregnant;
                    existingModel.PregnantNoOfMonths = model.PregnantNoOfMonths;
                    existingModel.NoOfAliveChildren = model.NoOfAliveChildren;
                    existingModel.NoOfAbortion = model.NoOfAbortion;
                    existingModel.NoOfChildrenDied = model.NoOfChildrenDied;
                    existingModel.ReasonOfDeath = model.ReasonOfDeath;
                    existingModel.AgeOfYoungestChildMonths = model.AgeOfYoungestChildMonths;
                    existingModel.AgeOfYoungestChildYears = model.AgeOfYoungestChildYears;
                    existingModel.HaveUsedFpMethod = model.HaveUsedFpMethod;
                    existingModel.FpMethodUsed = model.FpMethodUsed != null ? model.FpMethodUsed.Replace("\n", "") : model.FpMethodUsed;
                    existingModel.FpNotUsedInYears = model.FpNotUsedInYears;
                    existingModel.ReasonOfDiscontinuation = model.ReasonOfDiscontinuation;
                    existingModel.FpNoReason = model.FpNoReason;
                    existingModel.WantToUseFp = model.WantToUseFp;
                    existingModel.FpPurpose = model.FpPurpose;
                    existingModel.IsUserFp = model.IsUserFp;
                    existingModel.NameOfFp = model.NameOfFp;
                    existingModel.DateOfRegistration = model.DateOfRegistration;
                    existingModel.IsActive = true;
                    _mwraService.Update(existingModel);
                    _mwraService.SaveChanges();
                }

                return RedirectToAction("Index");
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

        public JsonResult GetMarvis(int unioncouncil,int Marvi, int? projectId = null)
        {
            List<SelectListItem> Marvis = new List<SelectListItem>();


            Marvis = new SelectList(_appUserService.GetAllMwrvi(unioncouncil, projectId), "AppUserId", "FullName", Marvi).ToList();


            return Json(new SelectList(Marvis, "Value", "Text", Marvi));
        }


        #endregion
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.Mwra existingModel = _mwraService.GetById(id);

                if (existingModel.MwraId != null)
                {
                    Data.HandsDB.MwraClient mwraClientModel = _mwraClientNewService.GetClientByMwraId(existingModel.MwraId);

                    if (mwraClientModel != null)
                    {
                        mwraClientModel.IsActive = false;
                        _mwraClientNewService.Update(mwraClientModel);
                        _mwraClientNewService.SaveChanges();
                    }
                    existingModel.IsActive = false;
                    _mwraService.Update(existingModel);
                    _mwraService.SaveChanges();
                }
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult DeleteClient(int id)
        {
            if (id != null)
            {
                Data.HandsDB.MwraClient existingModel = _mwraClientNewService.GetByIdData(id);
                if (existingModel.MwraId != null)
                {
                    existingModel.IsActive = false;

                    Data.HandsDB.Mwra mwraModel = _mwraService.GetMwraById(existingModel.MwraId.Value);

                    if (mwraModel != null)
                    {
                        mwraModel.IsClient = 0;
                        _mwraService.Update(mwraModel);
                        _mwraService.SaveChanges();
                    }

                    _mwraClientNewService.Update(existingModel);
                    _mwraClientNewService.SaveChanges();

                }
            }
            return RedirectToAction("MwraClientLisingActionResult");

        }
        public void ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = _mwraService.GetAllActive();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

        }


        public ActionResult ViewMwraClient(int id)
        {

            var model = _mwraService.GetAllMwraClientListBYid(id);
            SpMwraClientListingNewReturnModel ab = model.First();
            SpMwraClientListingNewReturnModel datamodel = new SpMwraClientListingNewReturnModel();
            return PartialView("MwraClientView", ab);
        }

        public ActionResult ViewMwraDropOutClient(int id)
        {

            var model = _mwraService.GetAllMwraDropOutClientListBYid(id);
            SpMwraDropOutClientListingReturnModel ab = model.First();
            SpMwraDropOutClientListingReturnModel datamodel = new SpMwraDropOutClientListingReturnModel();
            return PartialView("MwraDropOutClientView", ab);
        }
        public ActionResult View(int id)
        {

            Data.HandsDB.Mwra user = _mwraService.GetById(id);
            var model = new ViewModels.Models.Mwra();
            model.ProjectId = HandSession.Current.ProjectId;
            model.MwraId = user.MwraId;
            model.Name = user.Name;
            model.Dob = user.Dob;
            model.Address = user.Address;
            model.ContactNumber = user.ContactNumber;
            model.AssignedMarviId = user.AssignedMarviId;
            model.Longitude = user.Longitude;
            model.Latitude = user.Latitude;
            model.HusbandName = user.HusbandName;
            model.Cnic = user.Cnic;
            model.MaritialStatus = user.MaritialStatus;
            model.Age = user.Age;
            model.Occupation = user.Occupation;
            model.IsClient = user.IsClient;
            model.CreatedAt = user.CreatedAt;
            model.RegionId = user.RegionId;
            model.TaluqaId = user.TaluqaId;
            model.UnionCouncilId = user.UnionCouncilId;
            model.EducationOfClass = user.EducationOfClass;
            model.DurationOfMarriage = user.DurationOfMarriage;
            model.CrrentlyPregnant = user.CrrentlyPregnant;
            model.PregnantNoOfMonths = user.PregnantNoOfMonths;
            model.NoOfAliveChildren = user.NoOfAliveChildren;
            model.NoOfAbortion = user.NoOfAbortion;
            model.NoOfChildrenDied = user.NoOfChildrenDied;
            model.ReasonOfDeath = user.ReasonOfDeath;
            model.AgeOfYoungestChildYears = user.AgeOfYoungestChildYears;
            model.AgeOfYoungestChildMonths = user.AgeOfYoungestChildMonths;
            model.HaveUsedFpMethod = user.HaveUsedFpMethod;
            model.NameOfFp = user.NameOfFp;
            model.FpNotUsedInYears = user.FpNotUsedInYears;
            model.ReasonOfDiscontinuation = user.ReasonOfDiscontinuation;
            model.FpNoReason = user.FpNoReason;
            model.WantToUseFp = user.WantToUseFp;
            model.FpPurpose = user.FpPurpose;
            model.IsUserFp = user.IsUserFp;
            model.FpMethodUsed = user.FpMethodUsed;
            model.DateOfRegistration = user.DateOfRegistration;
            return View(model);
        }



        //public ActionResult Delete(int id)
        //{
        //    if (id != null)
        //    {
        //        Data.HandsDB.Mwra existingModel = _mwraService.GetById(id);
        //        existingModel.IsActive = false;
        //        _mwraService.Update(existingModel);
        //        _mwraService.SaveChanges();
        //    }

        //    return RedirectToAction("NoorCheckList");

        //}

        //public ActionResult NewMwra(int? mwraclientId)
        //{
        //    SpMwraClientListingNewReturnModel model = _newMwraClientService.GetByMwraId(mwraclientId);
        //    var Viewmodel = new ViewModels.Models.UserCurrentMonth.MwraClientListingNew();
        //    // Viewmodel.MwraClientList[0].mwraclientId = model[0].mwraclientId;
        //    Viewmodel.mwraclientId = model.mwraclientId;
        //    Viewmodel.mwra_client_id = model.mwra_client_id;
        //    Viewmodel.CR_Code = model.CR_Code;
        //    Viewmodel.date_of_client_generation = model.date_of_client_generation;
        //    Viewmodel.occasional = model.occasional;
        //    Viewmodel.referral = model.referral;
        //    Viewmodel.date_of_starting = model.date_of_starting;
        //    Viewmodel.history = model.history;
        //    Viewmodel.lmp_date = model.lmp_date;
        //    Viewmodel.bp = model.bp;
        //    Viewmodel.weight= model.weight;
        //    Viewmodel.jaundice = model.jaundice;
        //    Viewmodel.polar_anemia =model.polar_anemia;

        //    Viewmodel.foul = model.foul;
        //    Viewmodel.pain_lower = model.pain_lower;
        //    Viewmodel.contraceptual = model.contraceptual;

        //    return View(Viewmodel);
        //}

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
                    IGrid<Hands.Data.HandsDB.SpMwrasListingReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpMwrasListingReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" , "report.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpMwrasListingReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpMwrasListingReturnModel> grid = new Grid<Hands.Data.HandsDB.SpMwrasListingReturnModel>(_mwraService.GetMwraListing().OrderByDescending(x => x.mwra_id));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.date_of_registration).Titled("Date Of Registration");
            grid.Columns.Add(model => model.CR_Code).Titled("MW Code");
            grid.Columns.Add(model => model.full_name).Titled("Name Of Marvi Worker");
            grid.Columns.Add(model => model.address).Titled("Village");
            grid.Columns.Add(model => model.region_name).Titled("District");
            grid.Columns.Add(model => model.taluqa_name).Titled("Taluqa");
            grid.Columns.Add(model => model.union_council_name).Titled("Union Council");
            grid.Columns.Add(model => model.name).Titled("Name Of MWRA");
            grid.Columns.Add(model => model.husband_name).Titled("Husband Name");
            grid.Columns.Add(model => model.cnic).Titled("CNIC No");
            grid.Columns.Add(model => model.contact_number).Titled("Contact No");
            grid.Columns.Add(model => model.education_of_class).Titled("Education(Of Class Attended = 0 - 16)");
            grid.Columns.Add(model => model.occupation).Titled("Occupation");
            grid.Columns.Add(model => model.dob).Titled("Year Of Birth");
            grid.Columns.Add(model => model.age).Titled("Age(In Year Of MWRAs)");
            grid.Columns.Add(model => model.duration_of_marriage).Titled("Duration Of Marriage");
            grid.Columns.Add(model => model.crrently_pregnant).Titled("Currently Pregnant");
            grid.Columns.Add(model => model.pregnant_no_of_months).Titled("No Of Month");
            grid.Columns.Add(model => model.no_of_alive_children).Titled("No Of Alive Childern");
            grid.Columns.Add(model => model.no_of_abortion).Titled("No Of Abortion");
            grid.Columns.Add(model => model.no_of_children_died).Titled("No Of Childern Died");
            grid.Columns.Add(model => model.reason_of_death).Titled("Reason Of Death");
            grid.Columns.Add(model => model.age_of_youngest_child_years).Titled("Age Of Youngest Child If > 5(Years)");
            grid.Columns.Add(model => model.age_of_youngest_child_months).Titled("Age Of Youngest Child If < 5(Months)");
            grid.Columns.Add(model => model.have_used_fp_method).Titled("Have You Ever Used FP Method");
            grid.Columns.Add(model => model.fp_method_used).Titled("Name Of FP Method");
            grid.Columns.Add(model => model.fp_not_used_in_years).Titled("Since How Long FP Not Used(Years)");
            grid.Columns.Add(model => model.reason_of_discontinuation).Titled("Reason Of Discontinuation");
            grid.Columns.Add(model => model.fp_no_reason).Titled("FP No, Any Reason");
            grid.Columns.Add(model => model.want_to_use_fp).Titled("Do You Want To Use FP Method");
            grid.Columns.Add(model => model.fp_purpose).Titled("FP Purpose");
            grid.Columns.Add(model => model.is_user_fp).Titled("Current User Of FP");
            grid.Columns.Add(model => model.name_of_fp).Titled("Name Of FP");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpMwrasListingReturnModel>(grid);
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
                        bulkInsert.DestinationTableName = "mwra";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("mwra_id", "mwra_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("name", "name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("dob", "dob"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("address", "address"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("contact_number", "contact_number"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("assigned_marvi_id", "assigned_marvi_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("longitude", "longitude"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("latitude", "latitude"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("husband_name", "husband_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("cnic", "cnic"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("maritial_status", "maritial_status"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("age", "age"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("occupation", "occupation"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_client", "is_client"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("region_id", "region_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("taluqa_id", "taluqa_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("union_council_id", "union_council_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("education_of_class", "education_of_class"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("duration_of_marriage", "duration_of_marriage"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("crrently_pregnant", "crrently_pregnant"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("pregnant_no_of_months", "pregnant_no_of_months"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("no_of_alive_children", "no_of_alive_children"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("no_of_abortion", "no_of_abortion"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("no_of_children_died", "no_of_children_died"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("reason_of_death", "reason_of_death"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("age_of_youngest_child_years", "age_of_youngest_child_years"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("age_of_youngest_child_months", "age_of_youngest_child_months"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("have_used_fp_method", "have_used_fp_method"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("name_of_fp", "name_of_fp"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_not_used_in_years", "fp_not_used_in_years"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("reason_of_discontinuation", "reason_of_discontinuation"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_no_reason", "fp_no_reason"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("want_to_use_fp", "want_to_use_fp"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_purpose", "fp_purpose"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_user_fp", "is_user_fp"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_method_used", "fp_method_used"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("date_of_registration", "date_of_registration"));
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