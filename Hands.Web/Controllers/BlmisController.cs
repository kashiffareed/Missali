using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.BLMIS;
using Hands.Service.Dashboard;
using Hands.Service.NoorChecklist;
using Hands.Service.Product;
using Hands.Service.Products;
using Hands.ViewModels.Models;
using Hands.ViewModels.Models.BlmisUserStock;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using System.IO;
using System.Data.SqlClient;
using Excel;
using Hands.Common.Common;
using Hands.Service.Noor;

namespace Hands.Web.Controllers
{
    public class BlmisController : ControllerBase
    {
        private readonly IBlmisService _blmisService;
        private readonly IProductsService _productService;
        private readonly IAppUserService _appUserService;
        private readonly INoorService _noorService;


        public BlmisController()
        {
            _blmisService = new BlmisService();
            _productService = new ProductsService();
            _appUserService = new AppUserService();
            _noorService = new NoorService();

        }

        // GET: RealTimeCheckList
        public ActionResult Index()
        {
            var schemeList = _blmisService.GetBlmisInventory().OrderByDescending(x => x.Id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }

            return View(schemeList);



        }
        public ActionResult Create()
        {
            var model = new BlmisUserStock();
            model.ProductBlmisList = _productService.GetAllActive().Where(x => x.Producttype == "blmis" && x.ProjectId == HandSession.Current.ProjectId).ToList();
            model.UserList = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId && x.UserType == "marvi").ToList();
            model.ProjectId = HandSession.Current.ProjectId;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.BlmisUserStock.BlmisUserStock model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.BlmisUserstock ModelToSAve = new Data.HandsDB.BlmisUserstock();
                ModelToSAve.ProductId = model.ProductId;
                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                if (model.ProductId == 010)
                {
                    Data.HandsDB.Product productModel = new Data.HandsDB.Product();

                    productModel.ProjectId = HandSession.Current.ProjectId;
                    productModel.ProductName = model.ProductName;
                    productModel.ProductCategory = "Other";
                    productModel.BrandName = "";
                    productModel.MeasurementUnit = "";
                    productModel.Path = "new";
                    productModel.Producttype = "blmis";
                    productModel.RP = 0;
                    productModel.Generic = model.ProductName;
                    productModel.TP = 0;
                    productModel.PackSize = "";
                    productModel.RegNo = "";
                    productModel.ClientId = 0;
                    productModel.CreatedAt = DateTime.Now;
                    productModel.IsActive = true;
                    _productService.Insert(productModel);
                    _productService.SaveChanges();
                    ModelToSAve.ProductId = productModel.ProductId;
                }
                ModelToSAve.UserId = model.UserId;
                ModelToSAve.Quantity = model.Quantity;
                ModelToSAve.Price = model.Price;
                ModelToSAve.Unit = model.Unit;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _blmisService.Insert(ModelToSAve);
                _blmisService.SaveChanges();

            }

            return RedirectToAction("Index", "Blmis");
        }

        public ActionResult Edit(int id)
        {
            Data.HandsDB.BlmisUserstock model = _blmisService.GetById(id);
            var Viewmodel = new ViewModels.Models.BlmisUserStock.BlmisUserStock();
            Viewmodel.ProductBlmisList = _productService.GetAllActive().Where(x => x.Producttype == "blmis" && x.ProjectId == HandSession.Current.ProjectId).ToList();
            Viewmodel.UserList = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId && x.UserType == "marvi").ToList();
            Viewmodel.ProjectId = HandSession.Current.ProjectId;
            Viewmodel.Price = model.Price;
            Viewmodel.Quantity = model.Quantity;
            Viewmodel.Unit = model.Unit;
            Viewmodel.CreatedAt = DateTime.Now;
            Viewmodel.IsActive = true;
            Viewmodel.ProductId = model.ProductId;
            Viewmodel.UserId = model.UserId;

            return PartialView("Edit", Viewmodel);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.Models.BlmisUserStock.BlmisUserStock model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.BlmisUserstock existingModel = _blmisService.GetById(model.Id);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.Quantity = model.Quantity;
                    existingModel.Price = model.Price;
                    existingModel.Unit = model.Unit;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.IsActive = true;
                    existingModel.ProductId = model.ProductId;
                    existingModel.UserId = model.UserId;
                    _blmisService.Update(existingModel);
                    _blmisService.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.BlmisUserstock existingModel = _blmisService.GetById(id);
                existingModel.IsActive = false;
                _blmisService.Update(existingModel);
                _blmisService.SaveChanges();
            }

            return RedirectToAction("Index");

        }
        public JsonResult GetVisitCustomer(string term = "")
        {
            var marvis = _appUserService.GetAllActive().Where(x => x.UserType == "marvi").ToList();
            var objCustomerlist = marvis.Where(c => c.FullName.ToUpper().Contains(term.ToUpper())).Select(c => new { Name = c.FullName, ID = c.AppUserId }).Distinct().ToList();
            return Json(objCustomerlist, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ActionResult ExportIndex()
        //{
        //    string ExcelName = "test.xlsx";
        //    string ZipName = "test.zip";
        //    // Using EPPlus from nuget
        //    using (var stream = new MemoryStream())
        //    {
        //        using (ExcelPackage package = new ExcelPackage())
        //        {
        //            Int32 row = 2;
        //            Int32 col = 1;

        //            package.Workbook.Worksheets.Add("Data");
        //            IGrid<Hands.Data.HandsDB.UsersStock> grid = CreateExportableGrid();
        //            ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

        //            foreach (IGridColumn column in grid.Columns)
        //            {
        //                sheet.Cells[1, col].Value = column.Title;
        //                sheet.Column(col++).Width = 18;
        //            }

        //            foreach (IGridRow<Hands.Data.HandsDB.UsersStock> gridRow in grid.Rows)
        //            {
        //                col = 1;
        //                foreach (IGridColumn column in grid.Columns)
        //                    sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

        //                row++;
        //            }

        //            return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        //            //return File(
        //            //    stream.ToArray(),
        //            //    "application/xlsx",
        //            //    ExcelName
        //            //);
        //        }
        //    }
        //}

        //private IGrid<Hands.Data.HandsDB.UsersStock> CreateExportableGrid()
        //{
        //    IGrid<Hands.Data.HandsDB.UsersStock> grid = new Grid<Hands.Data.HandsDB.UsersStock>(_blmisService.GetAllActive());
        //    grid.ViewContext = new ViewContext { HttpContext = HttpContext };
        //    grid.Query = Request.QueryString;
        //    grid.Columns.Add(model => model.Quantity).Titled("Quantity");
        //    grid.Columns.Add(model => model.CreatedAt).Titled("Stock Date");

        //    grid.Pager = new GridPager<Hands.Data.HandsDB.UsersStock>(grid);
        //    grid.Processors.Add(grid.Pager);
        //    grid.Pager.PageSizes = new Dictionary<int, string>()
        //    {
        //        {0 ,"All" }
        //    };
        //    grid.Pager.ShowPageSizes = true;

        //    foreach (IGridColumn column in grid.Columns)
        //    {
        //        column.Filter.IsEnabled = true;
        //        column.Sort.IsEnabled = true;
        //    }

        //    return grid;
        //}

        //public ActionResult Import()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Import(HttpPostedFileBase upload)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        if (upload != null && upload.ContentLength > 0)
        //        {
        //            try
        //            {
        //                var stream = upload.InputStream;
        //                IExcelDataReader reader = null;
        //                if (upload.FileName.EndsWith(".xls"))
        //                {
        //                    reader = ExcelReaderFactory.CreateBinaryReader(upload.InputStream);
        //                }
        //                else if (upload.FileName.EndsWith(".xlsx"))
        //                {
        //                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //                }
        //                reader.IsFirstRowAsColumnNames = true;
        //                var result = reader.AsDataSet();
        //                reader.Close();



        //                string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Hands.Web.Properties.Settings.HandsDBConnection"].ConnectionString;
        //                SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
        //                bulkInsert.DestinationTableName = "users_stock";
        //                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("users_stock_id", "users_stock_id"));
        //                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("product_id", "product_id"));
        //                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_type", "user_type"));
        //                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_id", "user_id"));
        //                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("quantity", "quantity"));
        //                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Price", "Price"));
        //                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));
        //                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("IsActive", "IsActive"));
        //                bulkInsert.WriteToServer(result.Tables["Data"]);

        //            }
        //            catch (Exception e)
        //            {
        //                ModelState.AddModelError("Record", "No Records updated.");
        //                ModelState.AddModelError("Record", e.Message);
        //                //return View(iStatus);
        //            }
        //        }
        //    }
        //    return View();
        //}

    }


}