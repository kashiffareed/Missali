using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using Hands.Common.Common;
using Hands.Service.Regions;
using Hands.Service.Taluqa;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class TaluqaController : ControllerBase
    {
        // GET: Taluqa
        private ITaluqaService _taluqaService;
        private readonly IRegionService _regionService;

        public TaluqaController()
        {
            _taluqaService = new TaluqaService();
            _regionService = new RegionService();
        }

        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _taluqaService.GetTaluqaListing().OrderByDescending(x=>x.taluqa_id);

            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new ViewModels.Models.Taluqa.Taluqas();
            model.Regions = _regionService.GetAllActive().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            return View(model);

        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.Taluqa.Taluqas model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Taluqa ModelToSAve = new Data.HandsDB.Taluqa();
                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.RegionId = model.RegionId;
                ModelToSAve.TaluqaName = model.TaluqaName;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _taluqaService.Insert(ModelToSAve);
                _taluqaService.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.Taluqa model = _taluqaService.GetById(id);
            var Viewmodel = new ViewModels.Models.Taluqa.Taluqas();
            Viewmodel.TaluqaId = model.TaluqaId;
            Viewmodel.RegionId = model.RegionId;
            Viewmodel.TaluqaName = model.TaluqaName;
            Viewmodel.CreatedAt = DateTime.Now;
            Viewmodel.IsActive = true;
            Viewmodel.Regions = _regionService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            return PartialView("Edit", Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Taluqa.Taluqas model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Taluqa existingModel = _taluqaService.GetById(model.TaluqaId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.RegionId = model.RegionId;
                    existingModel.TaluqaName = model.TaluqaName;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.IsActive = true;
                    _taluqaService.Update(existingModel);
                    _taluqaService.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.Taluqa existingModel = _taluqaService.GetById(id);
                existingModel.IsActive = false;
                _taluqaService.Update(existingModel);
                _taluqaService.SaveChanges();
            }

            return RedirectToAction("Index");

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
                    IGrid<Hands.Data.HandsDB.TaluqaListingDataReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.TaluqaListingDataReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.TaluqaListingDataReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.TaluqaListingDataReturnModel> grid = new Grid<Hands.Data.HandsDB.TaluqaListingDataReturnModel>(_taluqaService.GetTaluqaListing().OrderByDescending(x=>x.taluqa_id));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.region_name).Titled("District Name");
            grid.Columns.Add(model => model.taluqa_name).Titled("Taluqa Name");
           

            grid.Pager = new GridPager<Hands.Data.HandsDB.TaluqaListingDataReturnModel>(grid);
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
                        bulkInsert.DestinationTableName = "taluqa";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("taluqa_id", "taluqa_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("taluqa_name", "taluqa_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("region_id", "region_id"));
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