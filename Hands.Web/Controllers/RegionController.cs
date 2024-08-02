using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Regions;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using System.IO;
using Excel;
using System.Data.SqlClient;
using Hands.Common.Common;

namespace Hands.Web.Controllers
{
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;



        public RegionController()
        {
            _regionService = new RegionService();
        }

        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _regionService.GetAllActive().Where(x=>x.ProjectId == HandSession.Current.ProjectId).OrderByDescending(x=>x.RegionsId);

            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new ViewModels.Models.region();

            return View(model);

        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.region model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Region ModelToSAve = new Data.HandsDB.Region();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.RegionsId = model.RegionsId;
                ModelToSAve.RegionName = model.RegionName;
                ModelToSAve.ClientId = 0;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _regionService.Insert(ModelToSAve);
                _regionService.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.Region model = _regionService.GetById(id);
            var Viewmodel = new ViewModels.Models.region();
            Viewmodel.RegionsId = model.RegionsId;
            Viewmodel.RegionName = model.RegionName;
            Viewmodel.ClientId = 0;
            Viewmodel.CreatedAt = DateTime.Now;
            return View(Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.region model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Region existingModel = _regionService.GetById(model.RegionsId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.RegionsId = model.RegionsId;
                    existingModel.RegionName = model.RegionName;
                    existingModel.ClientId = 0;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.IsActive = true;
                    _regionService.Update(existingModel);
                    _regionService.SaveChanges();
                }
            }

            else
            {
                return Edit(model.RegionsId);
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.Region existingModel = _regionService.GetById(id);
                existingModel.IsActive = false;
                _regionService.Update(existingModel);
                _regionService.SaveChanges();
            }

            return RedirectToAction("Index");

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
                    IGrid<Hands.Data.HandsDB.Region> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.Region> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.Region> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.Region> grid = new Grid<Hands.Data.HandsDB.Region>(_regionService.GetAllActive().OrderByDescending(x => x.RegionsId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.RegionName).Titled("District Name");
            grid.Columns.Add(model => model.CreatedAt).Titled("Registered Date");


            grid.Pager = new GridPager<Hands.Data.HandsDB.Region>(grid);
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
                        bulkInsert.DestinationTableName = "regions";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("regions_id", "regions_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("region_name", "region_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("client_id", "client_id"));
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