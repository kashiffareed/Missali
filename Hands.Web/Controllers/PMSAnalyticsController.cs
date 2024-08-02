using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Service.Logs;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class PMSAnalyticsController : ControllerBase
    {
        // GET: PMSAnalytics
        private readonly ILogsService _logsService;

        public PMSAnalyticsController()
        {
            _logsService = new LogsService();
        }

        public ActionResult Index()
        {

            //ViewBag.productList= _productService.GetAllActive();
            var schemeList = _logsService.GetAllAnalyticsWithCount();

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
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
                    IGrid<Hands.Data.HandsDB.SpPmsAnalticswithCountReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpPmsAnalticswithCountReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.SpPmsAnalticswithCountReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpPmsAnalticswithCountReturnModel> grid = new Grid<Hands.Data.HandsDB.SpPmsAnalticswithCountReturnModel>(_logsService.GetAllAnalyticsWithCount());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.RowNumber).Titled("S.No");
            grid.Columns.Add(model => model.user_type).Titled("Type");
            grid.Columns.Add(model => model.full_name).Titled("User Name");
            grid.Columns.Add(model => model.description).Titled("Activity");
            grid.Columns.Add(model => model.created_at).Titled("Created Date");
            grid.Columns.Add(model => model.count).Titled("Count");
            grid.Columns.Add(model => model.Duration).Titled("Duration");
            grid.Columns.Add(model => model.IsComplete).Titled("Complete/InComplete");
           


                             
            grid.Pager = new GridPager<Hands.Data.HandsDB.SpPmsAnalticswithCountReturnModel>(grid);
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