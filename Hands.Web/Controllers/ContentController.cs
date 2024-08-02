using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Categories;
using Hands.Service.Regions;
using Hands.ViewModels.Models;
using Hands.ViewModels.Models.catogaries;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using System.IO;
using Excel;
using System.Data.SqlClient;
using Hands.Common.Common;

namespace Hands.Web.Controllers
{
    public class ContentController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
      
        public ContentController()
        {
            _categoriesService = new CategoriesService();
            
        }

        public ActionResult Category()
        {
            // GET: Noor
            var schemeList = _categoriesService.GetAllActive().Where(x=>x.ProjectId == HandSession.Current.ProjectId).OrderByDescending(x=>x.CategoryId);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Category", schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {

            var model = new categories();
         
            return View(model);

        }
        [HttpPost]
        public ActionResult Create(categories model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Category ModelToSAve = new Data.HandsDB.Category();
                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.CategoryName = model.CategoryName;
                ModelToSAve.CategoryNameUrdu = model.CategoryNameUrdu;
                ModelToSAve.CategoryNameSindhi = model.CategoryNameSindhi;
                ModelToSAve.CreatedAt = DateTime.Now;
                _categoriesService.Insert(ModelToSAve);
                _categoriesService.SaveChanges();
            }
            return RedirectToAction("Category");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.Category model = _categoriesService.GetById(id);
            var Viewmodel = new ViewModels.Models.catogaries.categories();
            Viewmodel.CategoryId = model.CategoryId;
            Viewmodel.CategoryName = model.CategoryName;
            Viewmodel.CategoryNameUrdu = model.CategoryNameUrdu;
            Viewmodel.CategoryNameSindhi = model.CategoryNameSindhi;
            Viewmodel.CreatedAt = DateTime.Now;
       

            return PartialView("Edit", Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.catogaries.categories model)
        {
            if (ModelState.IsValid)
            {
                Category existingModel = _categoriesService.GetById(model.CategoryId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.CategoryId = model.CategoryId;
                    existingModel.CategoryName = model.CategoryName;
                    existingModel.CategoryNameUrdu = model.CategoryNameUrdu;
                    existingModel.CategoryNameSindhi = model.CategoryNameSindhi;
                    existingModel.CreatedAt = model.CreatedAt;
                    _categoriesService.Update(existingModel);
                    _categoriesService.SaveChanges();
                }
            }
            return RedirectToAction("Category");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                var existingModel = _categoriesService.GetById(id);
                existingModel.IsActive = false;
                _categoriesService.Update(existingModel);
                _categoriesService.SaveChanges();
            }


            return RedirectToAction("Category");

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
                    IGrid<Hands.Data.HandsDB.Category> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.Category> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.Category> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.Category> grid = new Grid<Hands.Data.HandsDB.Category>(_categoriesService.GetAllActive().OrderByDescending(x => x.CategoryId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.CategoryName).Titled("Category Name (English)");
            grid.Columns.Add(model => model.CategoryNameUrdu).Titled("Category Name (Urdu)");
            grid.Columns.Add(model => model.CategoryNameSindhi).Titled("Category Name(Sindhi)");
            grid.Columns.Add(model => model.CreatedAt).Titled("CreatedAt");

            grid.Pager = new GridPager<Hands.Data.HandsDB.Category>(grid);
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
                        bulkInsert.DestinationTableName = "categories";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("category_id", "category_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("category_name", "category_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("category_name_sindhi", "category_name_sindhi"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("category_name_urdu", "category_name_urdu"));
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