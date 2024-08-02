using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using NonFactors.Mvc.Grid;
using Hands.Service.Shedule;
using Excel;
using System.Data.SqlClient;

namespace Hands.Web.Controllers
{
    public class SheduleController : ControllerBase
    {
        private readonly ISheduleService _sheduleService;
        
        public SheduleController()
        {
            _sheduleService = new SheduleService();
            
        }

        public ActionResult Index()
        {
                
             var model =new Hands.ViewModels.Models.Shedule.Shedule();
            model.SheduleList = _sheduleService.GetSheduleActivityList();
            return View(model.SheduleList);
           
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
                    IGrid<Hands.Data.HandsDB.GetSheduleActivityListReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.GetSheduleActivityListReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.GetSheduleActivityListReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.GetSheduleActivityListReturnModel> grid = new Grid<Hands.Data.HandsDB.GetSheduleActivityListReturnModel>(_sheduleService.GetSheduleActivityList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.created_at).Titled("Session Date");
            grid.Columns.Add(model => model.user_type).Titled("Session Type");
            grid.Columns.Add(model => model.noorname).Titled("Marvi Name");
            grid.Columns.Add(model => model.lhvname).Titled("Lhv Name");
            grid.Columns.Add(model => model.name).Titled("Mwra Name");

            grid.Pager = new GridPager<Hands.Data.HandsDB.GetSheduleActivityListReturnModel>(grid);
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
                        bulkInsert.DestinationTableName = "session";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("session_id", "session_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("next_session_schedule", "next_session_schedule"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_completed", "is_completed"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("longitude", "longitude"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("latitude", "latitude"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("session_start_datetime", "session_start_datetime"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("session_end_datetime", "session_end_datetime"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("is_group", "is_group"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("marvi_id", "marvi_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("mobile_session_id", "mobile_session_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("lhv_id", "lhv_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_type", "user_type"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));
                       
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