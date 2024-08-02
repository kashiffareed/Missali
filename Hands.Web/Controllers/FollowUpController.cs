using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Service.SessionFollowup;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class FollowUpController : ControllerBase
    {
        private readonly ISessionfollowupService _sessionfollowupService;

        public FollowUpController()
        {
            _sessionfollowupService = new  SessionfollowupService();
        }
        // GET: FollowUp
        public ActionResult Index()
        {
            var schemeList = _sessionfollowupService.GetAllFollowups(HandSession.Current.ProjectId).OrderByDescending(x => x.session_followup_id);
            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }
        public ActionResult View(int sessionId)
        {

            var model = new Hands.ViewModels.Models.sessioncall.Sessioncall();
            var sessionFollowups = _sessionfollowupService.GetAllFollowups(HandSession.Current.ProjectId).FirstOrDefault(x=>x.session_followup_id == sessionId);
            return View(sessionFollowups);

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
                    IGrid<Hands.Data.HandsDB.GetfollowupByNamesReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.GetfollowupByNamesReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.GetfollowupByNamesReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.GetfollowupByNamesReturnModel> grid = new Grid<Hands.Data.HandsDB.GetfollowupByNamesReturnModel>(_sessionfollowupService.GetAllFollowups(HandSession.Current.ProjectId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.created_at).Titled("Session Date & Time");
            grid.Columns.Add(model => model.lhvname).Titled("Lhv Name");
            grid.Columns.Add(model => model.marviname).Titled("Marvi Name");
            grid.Columns.Add(model => model.mwraname).Titled("Mwra Name");
            grid.Columns.Add(model => model.history_irregular).Titled("History of Irregular Menstrual Cycle");
            grid.Columns.Add(model => model.lmp_date).Titled("LMP(Date)");
            grid.Columns.Add(model => model.bp).Titled("BP");
            grid.Columns.Add(model => model.weight).Titled("Weight");
            grid.Columns.Add(model => model.jaundice).Titled("Jaundice");
            grid.Columns.Add(model => model.polar_anemia).Titled("Pallor/Anemia");
            grid.Columns.Add(model => model.foul).Titled("Foul Smelling Vaginal Discharge");
            grid.Columns.Add(model => model.pain_lower).Titled("Pain Lower Abdomen");
            grid.Columns.Add(model => model.complication).Titled("Any Complication");
            grid.Columns.Add(model => model.danger).Titled("Any Danger Sign(Mention)");
            grid.Columns.Add(model => model.advise).Titled("Advice(Mention)");

            

            grid.Pager = new GridPager<Hands.Data.HandsDB.GetfollowupByNamesReturnModel>(grid);
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