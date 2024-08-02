using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Mwra;
using Hands.Service.Role;
using Hands.Service.Session;
using Hands.ViewModels.Models;
using Hands.ViewModels.Models.Activity;
using System.IO;
using Excel;
using Hands.Common.Common;
using OfficeOpenXml;
using NonFactors.Mvc.Grid;

namespace Hands.Web.Controllers
{
    public class ActivityController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ISessionMwraService _sessionmwraService;
        private readonly IMwraService _mwraService;
        public ActivityController()
        {
            _sessionService = new SessionService();
            
        }

        public ActionResult Index()
        {

             var model =new Hands.ViewModels.Models.Activity.Activity();
            model.MwraSesstionlist = _sessionService.GetMwraSessions(HandSession.Current.ProjectId);
            return View(model.MwraSesstionlist.OrderByDescending(x=>x.session_id));
           
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
                    IGrid<Hands.Data.HandsDB.GetMwraSessionReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.GetMwraSessionReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.GetMwraSessionReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.GetMwraSessionReturnModel> grid = new Grid<Hands.Data.HandsDB.GetMwraSessionReturnModel>(_sessionService.GetMwraSessions(HandSession.Current.ProjectId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.next_session_schedule).Titled("Session Date & Time");
            grid.Columns.Add(model => model.user_type).Titled("Session Type");
            grid.Columns.Add(model => model.lhv).Titled("Lhv Name");
            grid.Columns.Add(model => model.marvi).Titled("Marvi Name");
            grid.Columns.Add(model => model.name).Titled("MWRA Name");
            
            grid.Pager = new GridPager<Hands.Data.HandsDB.GetMwraSessionReturnModel>(grid);
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
                        bulkInsert.DestinationTableName = "session_mwras";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("session_mwra_id", "session_mwra_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("session_id", "session_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("mwra_id", "mwra_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("join_datetime", "join_datetime"));
                        
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