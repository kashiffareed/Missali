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
    public class MwraDropoutClientController : ControllerBase
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
        public MwraDropoutClientController()
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
        
        public ActionResult MwraDropOutClientLisingActionResult(int? regionId, int? taluqaId, int? unionCouncilId, int export = 0)
        {
            var model = new Mwra();
            model.DropOutClientListing = _mwraService.SearchMwraDropOutClient(0, regionId, taluqaId, unionCouncilId).OrderByDescending(x => x.mwra_client_id);
            model.Search.Regions = _regionService.GetAll();
            model.RegionId = regionId;
            model.TaluqaId = taluqaId;
            model.Search.ControllerName = "MwraDropoutClient";
            model.Search.ViewName = "MwraDropOutClientLisingActionResult";
            model.UnionCouncilId = unionCouncilId;
            if (export == 1)
            {
                ExportController.ExportExcel(
                    ExportController.RenderViewToString(
                        ControllerContext,
                        "~/Views/Mwra/_MwraDropOutClientListingPartial.cshtml",
                        model),
                    "",
                    ""

                );
                return null;
            }


            return Request.IsAjaxRequest() ? (ActionResult)PartialView("MwraDropOutClientLisingActionResult", model) : View("MwraDropOutClientLisingActionResult", model);

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
        
 

        
        public ActionResult ViewMwraDropOutClient(int id)
        {

            var model = _mwraService.GetAllMwraDropOutClientListBYid(id);
            SpMwraDropOutClientListingReturnModel ab = model.First();
            SpMwraDropOutClientListingReturnModel datamodel = new SpMwraDropOutClientListingReturnModel();
            return PartialView("MwraDropOutClientView", ab);
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
                    IGrid<Hands.Data.HandsDB.SpMwraDropOutClientListingReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpMwraDropOutClientListingReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" , "MwraDropOutClient.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpMwraDropOutClientListingReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpMwraDropOutClientListingReturnModel> grid = new Grid<Hands.Data.HandsDB.SpMwraDropOutClientListingReturnModel>(_mwraService.SearchMwraDropOutClient(0, null, null, null).OrderByDescending(x => x.mwra_client_id));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.date_of_registration).Titled("Date Of Registration");
            grid.Columns.Add(model => model.CR_Code).Titled("MW Code");
            grid.Columns.Add(model => model.full_name).Titled("Name Of Marvi Worker");
            grid.Columns.Add(model => model.address).Titled("Village");
            grid.Columns.Add(model => model.DropoutDate).Titled("DropoutDate");
            grid.Columns.Add(model => model.DropoutReason).Titled("DropoutReason");
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

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpMwraDropOutClientListingReturnModel>(grid);
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

       


    }
}