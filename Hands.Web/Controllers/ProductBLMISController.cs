using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using Hands.Common.Common;
using Hands.Service.Products;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class ProductBLMISController : Controller
    {
        // GET: ProductBLMIS
        private IProductsService _productsService;
        public ProductBLMISController()
        {
            _productsService = new ProductsService();

        }

        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _productsService.GetAllProductByType("blmis").OrderByDescending(x=>x.ProductId);

            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.Products.ProductsModel model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Product ModelToSAve = new Data.HandsDB.Product();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.ProductName = model.ProductName;
                ModelToSAve.ProductCategory = model.ProductCategory;
                ModelToSAve.BrandName = model.BrandName;
                ModelToSAve.MeasurementUnit = model.MeasurementUnit;
                ModelToSAve.Path = "new";
                ModelToSAve.Producttype = "blmis";
                ModelToSAve.RP = model.RP;
                ModelToSAve.Generic = model.ProductName;
                ModelToSAve.TP = model.RP;
                ModelToSAve.PackSize = model.PackSize;
                ModelToSAve.RegNo = model.RegNo;
                ModelToSAve.ClientId = 0;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _productsService.Insert(ModelToSAve);
                _productsService.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Edit(int id)
        {
            Data.HandsDB.Product model = _productsService.GetById(id);
            var Viewmodel = new ViewModels.Models.Products.ProductsModel();

            Viewmodel.ProductId = model.ProductId;
            Viewmodel.ProductName = model.ProductName;
            Viewmodel.ProductCategory = model.ProductCategory;
            Viewmodel.BrandName = model.BrandName;
            Viewmodel.Generic = model.ProductName;
            Viewmodel.TP = model.RP;
            Viewmodel.MeasurementUnit = model.MeasurementUnit;
            Viewmodel.Path = "new";
            Viewmodel.Producttype = "blmis";
            Viewmodel.RP = model.RP;
            Viewmodel.PackSize = model.PackSize;
            Viewmodel.RegNo = model.RegNo;
            Viewmodel.ClientId = 0;
            Viewmodel.CreatedAt = DateTime.Now;
            Viewmodel.IsActive = true;
            return PartialView("Edit", Viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Products.ProductsModel model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Product existingModel = _productsService.GetById(model.ProductId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.ProductId = model.ProductId;
                    existingModel.ProductName = model.ProductName;
                    existingModel.ProductCategory = model.ProductCategory;
                    existingModel.BrandName = model.BrandName;
                    existingModel.MeasurementUnit = model.MeasurementUnit;
                    existingModel.Path = "new";
                    existingModel.Producttype = "blmis";
                    existingModel.RP = model.RP;
                    existingModel.PackSize = model.PackSize;
                    existingModel.Generic = model.ProductName;
                    existingModel.TP = model.RP;
                    existingModel.RegNo = model.RegNo;
                    existingModel.ClientId = 0;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.IsActive = true;
                    _productsService.Update(existingModel);
                    _productsService.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.Product existingModel = _productsService.GetById(id);
                existingModel.IsActive = false;
                _productsService.Update(existingModel);
                _productsService.SaveChanges();
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
                    IGrid<Hands.Data.HandsDB.Product> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.Product> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.Product> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.Product> grid = new Grid<Hands.Data.HandsDB.Product>(_productsService.GetAllProductByType("blmis").OrderByDescending(x => x.ProductId));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.ProductCategory).Titled("Product Category");
            grid.Columns.Add(model => model.ProductName).Titled("Product Name");
            grid.Columns.Add(model => model.BrandName).Titled("Brand Name");
            grid.Columns.Add(model => model.MeasurementUnit).Titled("Measurement Unit");
            grid.Columns.Add(model => model.RP).Titled("RP");
            grid.Columns.Add(model => model.RegNo).Titled("Reg No");
            grid.Columns.Add(model => model.PackSize).Titled("Pack Size");


            grid.Pager = new GridPager<Hands.Data.HandsDB.Product>(grid);
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
                        bulkInsert.DestinationTableName = "products";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("product_id", "product_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("product_name", "product_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("generic", "generic"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("reg_no", "reg_no"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("pack_size", "pack_size"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("path", "path"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("t_p", "t_p"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("r_p", "r_p"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("client_id", "client_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Producttype", "Producttype"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ProductCategory", "ProductCategory"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("BrandName", "BrandName"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("MeasurementUnit", "MeasurementUnit"));
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