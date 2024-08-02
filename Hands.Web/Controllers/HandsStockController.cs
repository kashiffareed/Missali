using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.NoorChecklist;
using Hands.Service.Product;
using Hands.Service.Products;
using Product = Hands.ViewModels.Models.Product.Product;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using System.IO;
using Excel;
using System.Data.SqlClient;
using Hands.Common.Common;
using Hands.Service.Regions;

namespace Hands.Web.Controllers
{
    public class HandsStockController : ControllerBase
    {
        private readonly IProductService _productService;
        private IProductsService _productsService;
        private IRegionService _regionService;

        public HandsStockController()
        {
            _productService = new ProductService();
            _productsService = new  ProductsService();
            _regionService = new RegionService();

        }

        // GET: RealTimeCheckList
        public ActionResult Index()
        {
            var schemeList = _productService.GetClmisHandStock();

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }

            return View(schemeList);



        }
        public ActionResult Create()
        {
            var model = new Product();
            model.ProductsList = _productsService.GetAllProductByType("clmis");
            model.RegionList = _regionService.GetRegionList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.Product.Product model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Stock ModelToSAve = new Data.HandsDB.Stock();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.ProductId = model.ProductId;
                ModelToSAve.StockId = model.StockId;
                ModelToSAve.Quantity = model.Quantity;
                ModelToSAve.Notes = model.Notes;
                ModelToSAve.Price = model.Price;
                ModelToSAve.RegionId = model.RegionId;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _productService.Insert(ModelToSAve);

                _productService.SaveChanges();
            }

            return RedirectToAction("Index", "HandsStock");

        }

        public ActionResult Edit(int id)
        {
            
            Data.HandsDB.Stock model = _productService.GetById(id);
            var viewmodel = new ViewModels.Models.Product.Product();
            viewmodel.RegionList = _regionService.GetRegionList();
            viewmodel.ProductId = model.ProductId;
            viewmodel.StockId = model.StockId;
            viewmodel.ProductsList = _productsService.GetAllProductByType("clmis");
            viewmodel.RegionId = model.RegionId.ToInt();
            viewmodel.Quantity = model.Quantity;
          //  viewmodel.Price = model.Price;
            
            viewmodel.Notes = model.Notes;
            viewmodel.CreatedAt = DateTime.Now;
            
            return PartialView("Edit", viewmodel);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Product.Product model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Stock existingModel = _productService.GetById(model.StockId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.ProductId = model.ProductId;
                    existingModel.StockId = model.StockId;
                    existingModel.Quantity = model.Quantity;
                    existingModel.Notes = model.Notes;
                    existingModel.RegionId = model.RegionId;
                    existingModel.CreatedAt = DateTime.Now;

                    _productService.Update(existingModel);
                    _productService.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.Stock existingModel = _productService.GetById(id);
                existingModel.IsActive = false;
                _productService.Update(existingModel);
                _productService.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        public ActionResult TotalStocks()
        {
            var schemeList = _productService.GetTotalClmisHandStock().OrderBy(x => x.product_name);

            if (Request.IsAjaxRequest())
            {
                return PartialView("TotalStocks", schemeList);
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
                    IGrid<Hands.Data.HandsDB.SpClmisHandStockReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpClmisHandStockReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.SpClmisHandStockReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpClmisHandStockReturnModel> grid = new Grid<Hands.Data.HandsDB.SpClmisHandStockReturnModel>(_productService.GetClmisHandStock());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;

            grid.Columns.Add(model => model.region_name).Titled("Region Name");
            grid.Columns.Add(model => model.product_name).Titled("Product Name");
            grid.Columns.Add(model => model.quantity).Titled("Quantity");
            grid.Columns.Add(model => model.notes).Titled("Notes");
            grid.Columns.Add(model => model.created_at).Titled("Stock Date");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpClmisHandStockReturnModel>(grid);
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
                        bulkInsert.DestinationTableName = "stocks";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("stock_id", "stock_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("product_id", "product_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("quantity", "quantity"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("notes", "notes"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Price", "Price"));
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