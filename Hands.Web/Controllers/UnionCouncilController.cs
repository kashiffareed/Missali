using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using Hands.Common.Common;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class UnionCouncilController : ControllerBase
    {
        // GET: UnionCouncil

        private IUnionCouncilService _councilService;
        private ITaluqaService _taluqaService;

        public UnionCouncilController()
        {
            _councilService = new UnionCouncilService();
            _taluqaService = new TaluqaService();
          
        }

        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _councilService.GetUnionCouncilListing().OrderByDescending(x=>x.union_council_id);

            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new ViewModels.Models.UnionCouncil.UnionCouncils();
            model.Taluqa = _taluqaService.GetAll().Where(x=>x.IsActive && x.ProjectId == HandSession.Current.ProjectId);
            return View(model);

        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.UnionCouncil.UnionCouncils model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.UnionCouncil ModelToSAve = new Data.HandsDB.UnionCouncil();;

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.TaluqaId = model.TaluqaId;
                ModelToSAve.UnionCouncilName = model.UnionCouncilName;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _councilService.Insert(ModelToSAve);
                _councilService.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.UnionCouncil model = _councilService.GetById(id);
            var Viewmodel = new ViewModels.Models.UnionCouncil.UnionCouncils();
            Viewmodel.UnionCouncilId= model.UnionCouncilId;
            Viewmodel.TaluqaId = model.TaluqaId;
            Viewmodel.UnionCouncilName = model.UnionCouncilName;
            Viewmodel.CreatedAt = DateTime.Now;
            Viewmodel.IsActive = true;
            Viewmodel.Taluqa = _taluqaService.GetAll().Where(x => x.IsActive && x.ProjectId == HandSession.Current.ProjectId);
            return PartialView("Edit", Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.UnionCouncil.UnionCouncils model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.UnionCouncil existingModel = _councilService.GetById(model.UnionCouncilId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.UnionCouncilId = model.UnionCouncilId;
                    existingModel.TaluqaId = model.TaluqaId;
                    existingModel.UnionCouncilName = model.UnionCouncilName;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.IsActive = true;
                    _councilService.Update(existingModel);
                    _councilService.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.UnionCouncil existingModel = _councilService.GetById(id);
                existingModel.IsActive = false;
                _councilService.Update(existingModel);
                _councilService.SaveChanges();
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
                    IGrid<Hands.Data.HandsDB.UnionCouncilListingDataReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.UnionCouncilListingDataReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.UnionCouncilListingDataReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.UnionCouncilListingDataReturnModel> grid = new Grid<Hands.Data.HandsDB.UnionCouncilListingDataReturnModel>(_councilService.GetUnionCouncilListing().OrderByDescending(x=>x.union_council_id));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.taluqa_name).Titled("Taluqa Name");
            grid.Columns.Add(model => model.union_council_name).Titled("union Council");
          
            grid.Pager = new GridPager<Hands.Data.HandsDB.UnionCouncilListingDataReturnModel>(grid);
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
                        bulkInsert.DestinationTableName = "union_council";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("union_council_id", "union_council_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("union_council_name", "union_council_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("taluqa_id", "taluqa_id"));
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