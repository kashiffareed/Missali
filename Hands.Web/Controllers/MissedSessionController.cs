using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Service.Session;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class MissedSessionController : ControllerBase
    {
        // GET: MissedSession

        private readonly ISessioncallService _sessioncallService;

        public MissedSessionController()
        {
            _sessioncallService = new SessioncallService();
        }
        public ActionResult Index()
        {
            var schemeList = _sessioncallService.GetAllMissedSessionsWIthMwraNames();
            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
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
                    IGrid<Hands.Data.HandsDB.SpMissedSessionsWithMwraNamesReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpMissedSessionsWithMwraNamesReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.SpMissedSessionsWithMwraNamesReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpMissedSessionsWithMwraNamesReturnModel> grid = new Grid<Hands.Data.HandsDB.SpMissedSessionsWithMwraNamesReturnModel>(_sessioncallService.GetAllMissedSessionsWIthMwraNames());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.created_at).Titled("Missed Session Date");
            grid.Columns.Add(model => model.user_type).Titled("Session Type");
            grid.Columns.Add(model => model.marvi).Titled("Marvi Name");
            grid.Columns.Add(model => model.lhv).Titled("Lhv Name");
            grid.Columns.Add(model => model.mwraNames).Titled("Mwra Name");


        


            grid.Pager = new GridPager<Hands.Data.HandsDB.SpMissedSessionsWithMwraNamesReturnModel>(grid);
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