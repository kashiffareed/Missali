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
    public class NewUserClientsController : ControllerBase
    {
        // GET: Default
        private readonly IMwraClientListingService _mwraClientListingService;
        private readonly IMwraService _mwraService;
        private readonly IRegionService _regionService;
        private readonly ITaluqaService _taluqaService;
        private readonly INoorService _noorService;
        private readonly IUnionCouncilService _unionCouncilService;
        private readonly IMwraClientNewService _mwraClientNewService;

        //static int unioncouncilID;
        public NewUserClientsController()
        {
            _mwraService = new MwraService();
            _regionService = new RegionService();
            _taluqaService = new TaluqaService();
            _noorService = new NoorService();
            _unionCouncilService = new UnionCouncilService();
            _mwraClientListingService = new MwraClientListingService();
            _mwraClientNewService = new MwraClientService();


        }
        public ActionResult NewUserClientLisingActionResult(int? regionId, int? taluqaId, int? unionCouncilId, int export = 0)
        {
            var model = new Mwra();
            model.NewUserClientList = _mwraService.SearchNewUserClient(0,regionId, taluqaId, unionCouncilId).OrderByDescending(x => x.mwra_client_id);
            model.Search.Regions = _regionService.GetAll();
            model.RegionId = regionId;
            model.TaluqaId = taluqaId;
            model.Search.ControllerName = "NewUserClients";
            model.Search.ViewName = "NewUserClientLisingActionResult";
            model.UnionCouncilId = unionCouncilId;
            if (export == 1)
            {
                ExportController.ExportExcel(
                    ExportController.RenderViewToString(
                        ControllerContext,
                        "~/Views/NewUserClients/_NewUserClientLisingPartial.cshtml",
                        model),
                    "",
                    ""

                );
                return null;
            }


            return Request.IsAjaxRequest() ? (ActionResult)PartialView("NewUserClientLisingActionResult", model) : View("NewUserClientLisingActionResult", model);
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


        public ActionResult ViewNewUserClient(int id)
        {
            var model = _mwraService.GetAllNewUserClientListBYid(id);
            SpNewUserClientListingReturnModel ab = model.First();
            SpNewUserClientListingReturnModel datamodel = new SpNewUserClientListingReturnModel();
            return PartialView("NewUserClientView", ab);
        }
       

        [HttpGet]
        public ActionResult ExportIndex()
        {
            using (var stream = new MemoryStream())
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    Int32 row = 2;
                    Int32 col = 1;

                    package.Workbook.Worksheets.Add("Data");
                    IGrid<Hands.Data.HandsDB.SpNewUserClientListingCsvReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpNewUserClientListingCsvReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" , "report.xlsx");
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpNewUserClientListingCsvReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpNewUserClientListingCsvReturnModel> grid = new Grid<Hands.Data.HandsDB.SpNewUserClientListingCsvReturnModel>(_mwraService.GetNewUserClientListing().OrderByDescending(x => x.mwraclientId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.CR_Code).Titled("CR Code");
            grid.Columns.Add(model => model.date_of_client_generation).Titled("Date Of Client Generation");
            grid.Columns.Add(model => model.name).Titled("Name Of Client");
            grid.Columns.Add(model => model.full_name).Titled("Name Of Marvi Assigned");
            grid.Columns.Add(model => model.occasional).Titled("Occasional");
            grid.Columns.Add(model => model.referral).Titled("Referral");
            grid.Columns.Add(model => model.date_of_starting).Titled("In case of Current User, Date of Starting");
            grid.Columns.Add(model => model.history).Titled("History of Irregular Menstrual Cycle");
            grid.Columns.Add(model => model.lmp_date).Titled("LMP (Date)");
            grid.Columns.Add(model => model.bp).Titled("BP");
            grid.Columns.Add(model => model.weight).Titled("Weight");
            grid.Columns.Add(model => model.jaundice).Titled("Jaundice");
            grid.Columns.Add(model => model.polar_anemia).Titled("Pallor/Anemia");
            grid.Columns.Add(model => model.foul).Titled("Foul Smelling Vaginal Discharge");
            grid.Columns.Add(model => model.pain_lower).Titled("Pain Lower abdomen");
            grid.Columns.Add(model => model.contraceptual).Titled("Contraceptive");
            grid.Columns.Add(model => model.Quantity).Titled("Quantity");
           
            grid.Pager = new GridPager<Hands.Data.HandsDB.SpNewUserClientListingCsvReturnModel>(grid);
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