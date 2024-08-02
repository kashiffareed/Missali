using Hands.Service.Logs;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using NonFactors.Mvc.Grid;
using Hands.Service.Shedule;
using System.Collections.Generic;
using System.Data;
using Excel;
using System.Data.SqlClient;
using System.Linq;
using Hands.Common.Common;

namespace Hands.Web.Controllers
{
    public class LogController : ControllerBase
    {

        private readonly ILogsService _logService;
        
        public LogController()
        {
            _logService = new LogsService();
          
        }

        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _logService.GetAllActive().Where(x=>x.ProjectId == HandSession.Current.ProjectId);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }

            return View(schemeList);
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
                    IGrid<Hands.Data.HandsDB.Log> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.Log> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.Log> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.Log> grid = new Grid<Hands.Data.HandsDB.Log>(_logService.GetAllActive());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.Description).Titled("ACTIVITY");
            grid.Columns.Add(model => model.UserType).Titled("USER TYPE");
            grid.Columns.Add(model => model.CreatedAt).Titled("DATE TIME");
            

            grid.Pager = new GridPager<Hands.Data.HandsDB.Log>(grid);
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
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (upload.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }

                        reader.IsFirstRowAsColumnNames = true;
                        var result = reader.AsDataSet();
                        reader.Close();



                        string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Hands.Web.Properties.Settings.HandsDBConnection"].ConnectionString;
                        SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
                        if (result.Tables["Sheet1"].Rows.Count > 0)
                        {
                            DataColumn newColumn = new DataColumn("IsCompletee", typeof(System.Boolean));
                            newColumn.DefaultValue = true;
                            result.Tables["Sheet1"].Columns.Add(newColumn);
                        }
                        bulkInsert.DestinationTableName = "tempLogs";
                       // bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("log_id", "log_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("description", "description"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_type", "user_type"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_id", "user_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Duration", "Duration"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("IsCompletee", "IsComplete"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));
                        //bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("IsActive", "IsActive"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ProjectId", "ProjectId"));
                        
                        bulkInsert.WriteToServer(result.Tables["Sheet1"]);


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