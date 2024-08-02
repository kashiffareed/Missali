using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.CLMIS;
using Hands.Service.Dashboard;
using Hands.Service.NoorChecklist;
using Hands.Service.Product;
using Hands.Service.Products;
using Hands.ViewModels.Models;
using Hands.ViewModels.Models.UserStock;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using System.IO;
using Excel;
using System.Data.SqlClient;
using System.Windows.Forms;
using Hands.Common.Common;
using Hands.Service.Regions;

namespace Hands.Web.Controllers
{
    public class ClmisController : ControllerBase
    {
        private readonly IClmisService _clmisService;
        private readonly IProductsService _productService;
        private readonly IAppUserService _appUserService;
        private IProductService _productclmisService;
        private IRegionService _regionService;
     

        public ClmisController()
        {
            _clmisService = new ClmisService();
            _productService = new ProductsService();
            _appUserService = new AppUserService();
            _productclmisService = new ProductService();
            _regionService = new RegionService();

        }

        // GET: RealTimeCheckList
        public ActionResult Index()
        {

            //ViewBag.productList= _productService.GetAllActive();
            var schemeList = _clmisService.GetClmisInventory().OrderByDescending(x=>x.users_stock_id);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }

            return View(schemeList);



        }
        public ActionResult Create()
        {
            var model = new userStock();
            model.ProductClmisList = _productclmisService.GetTotalClmisHandStock().Where(x=>x.QuantityRemaining > 0);
            //model.ProductsList = _productService.GetAllProductByType("clmis");
            model.RegionList = _regionService.GetRegionList();
            model.ProjectId = HandSession.Current.ProjectId;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.UserStock.userStock model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.UsersStock ModelToSAve = new Data.HandsDB.UsersStock();

                //var stockQuantity = _clmisService.GetStockQuantity(model.ProductId);

                var ProductClmisList = _productclmisService.ClmisHandStock(model.RegionId).Where(x => x.product_id == model.ProductId).FirstOrDefault();

                if (model.Quantity > 0 && model.Quantity <= ProductClmisList.QuantityRemaining)
                {
                    // Data.HandsDB.UsersStock ModelToSAve = new Data.HandsDB.UsersStock();

                    ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                    ModelToSAve.RegionId = model.RegionId;
                    ModelToSAve.Quantity = model.Quantity;
                    ModelToSAve.CreatedAt = DateTime.Now;
                    ModelToSAve.IsActive = true;
                    ModelToSAve.ProductId = model.ProductId;
                    ModelToSAve.UserId = model.UserId;
                    ModelToSAve.Price = model.Price;
                    ModelToSAve.UserType = model.userType;
                    _clmisService.Insert(ModelToSAve);
                    _clmisService.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Create", "Clmis");
                }
            }

            return RedirectToAction("Index", "Clmis");

        }

        public ActionResult Edit(int id)
        {
            Data.HandsDB.UsersStock model = _clmisService.GetById(id);
            var Viewmodel = new ViewModels.Models.UserStock.userStock();
            Viewmodel.RegionList = _regionService.GetRegionList();
            Viewmodel.ProjectId = HandSession.Current.ProjectId;
            Viewmodel.HlvList = _appUserService.GetAllActive();
            Viewmodel.RegionId = model.RegionId.Value;
            Viewmodel.Quantity = model.Quantity;
            Viewmodel.CreatedAt = DateTime.Now;
            Viewmodel.IsActive = true;
            Viewmodel.ProductId = model.ProductId;
            Viewmodel.UserId = model.UserId;
            Viewmodel.Price = model.Price;
            Viewmodel.userType = model.UserType;
            Viewmodel.UsersStockId = model.UsersStockId;
            Viewmodel.ProductClmisList = _productclmisService.GetTotalClmisHandStock().Where(x => x.QuantityRemaining > 0);
            return PartialView("Edit", Viewmodel);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.Models.UserStock.userStock model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.UsersStock existingModel = _clmisService.GetById(model.UsersStockId);
          
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.ProductId = model.ProductId;
                    existingModel.Quantity = model.Quantity;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.IsActive = true;
                    existingModel.ProductId = model.ProductId;
                    existingModel.Price = model.Price;
                    existingModel.UserId = model.UserId;
                    existingModel.UserType = model.userType;



                    _clmisService.Update(existingModel);
                    _clmisService.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.UsersStock existingModel = _clmisService.GetById(id);
                existingModel.IsActive = false;
                _clmisService.Update(existingModel);
                _clmisService.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        public JsonResult GetUserByType(string usertype , int userId, int RegionId)
        {
            //todo fix logical issues

            List<SelectListItem> Users = new List<SelectListItem>();
            var user = _appUserService.GetAll(usertype,HandSession.Current.ProjectId).Where(x=>x.RegionId == RegionId).ToList();

            Users = new SelectList(user, "AppUserId", "FullName").ToList();
            ViewBag.UsersBytype = Users;
            return Json(new SelectList(Users, "Value", "Text", userId));
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
                    IGrid<Hands.Data.HandsDB.SpClmisInventoryReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpClmisInventoryReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.SpClmisInventoryReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpClmisInventoryReturnModel> grid = new Grid<Hands.Data.HandsDB.SpClmisInventoryReturnModel>(_clmisService.GetClmisInventory());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.region_name).Titled("Region Name");
            grid.Columns.Add(model => model.product_name).Titled("Product Name");
            grid.Columns.Add(model => model.user_type).Titled("User Type");
            grid.Columns.Add(model => model.full_name).Titled("User Name");
            grid.Columns.Add(model => model.QuantityAssigned).Titled("Quantity");
            grid.Columns.Add(model => model.created_at).Titled("Created At");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpClmisInventoryReturnModel>(grid);
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
                        bulkInsert.DestinationTableName = "users_stock";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("users_stock_id", "users_stock_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("product_id", "product_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_type", "user_type"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_id", "user_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("quantity", "quantity"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Price", "Price"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));
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

        public ActionResult InventoryStatus()
        {

            //ViewBag.productList= _productService.GetAllActive();
            var clmisInventoryStatus = _clmisService.ClmisInventoryStatus().OrderBy(x=>x.product_name);

            if (Request.IsAjaxRequest())
            {
                return PartialView("InventoryStatus", clmisInventoryStatus);
            }

            return View(clmisInventoryStatus);



        }
        public ActionResult ClmisInventoryLog()
        {

            //ViewBag.productList= _productService.GetAllActive();
            var clmisInventoryLog = _clmisService.ClmisInventoryLog().OrderBy(x=>x.ProductName);

            if (Request.IsAjaxRequest())
            {
                return PartialView("ClmisInventoryLog", clmisInventoryLog);
            }

            return View(clmisInventoryLog);



        }
        public JsonResult GetProductQuantity(int ProductId,int RegionId)
        {
            //todo fix logical issues
            var ProductClmisList = _productclmisService.ClmisHandStock(RegionId).Where(x => x.product_id == ProductId);
            //var ProductClmisList = _productclmisService.GetTotalClmisHandStock().Where(x => x.product_id == ProductId);
            //List<SelectListItem> Users = new List<SelectListItem>();
            //var user = _appUserService.GetAll(usertype);

            //Users = new SelectList(user, "AppUserId", "FullName").ToList();
            return Json(ProductClmisList);
        }


    }


}