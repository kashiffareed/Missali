using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.BlmisSells;
using Hands.Service.Noor;
using Hands.ViewModels.Models.BlmisSells;
namespace Hands.Web.Controllers
{
    public class BlmisSellsController : ControllerBase
    {
        private readonly IBlmisSellsService _blmisSellsService;
        private readonly IAppUserService _appUserService;
        private readonly INoorService _noorService;

        public BlmisSellsController()
        {
            _blmisSellsService = new BlmisSellsService();
            _appUserService = new AppUserService();
            _noorService = new NoorService();
        }

        // GET: RealTimeCheckList
        public ActionResult Index()
        {
            var schemeList = _blmisSellsService.GetBlmisSells(HandSession.Current.ProjectId, null).OrderByDescending(x => x.Id);
            
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }

            return View(schemeList);



        }
        public ActionResult Create()
        {
            var model = new BlmisSells();
            model.UserList = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId && x.UserType == "marvi").ToList();
            model.SellDate = DateTime.Today.ToString("MM-dd-yyyy");
            model.YesterdayDate = DateTime.Today.ToString("MM-dd-yyyy");
            model.ProjectId = HandSession.Current.ProjectId;
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.BlmisSells.BlmisSells model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.BlmisSellHistory ModelToSAve = new Data.HandsDB.BlmisSellHistory();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.SellDate = model.SellDate;
                ModelToSAve.Amount = model.Amount;
                ModelToSAve.YesterdaySelldate = model.YesterdayDate.AsDateTime();
                ModelToSAve.DayWiseAmount = model.DayWiseAmount;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.UserId = model.UserId;
                ModelToSAve.IsActive = true;
                _blmisSellsService.Insert(ModelToSAve);
                _blmisSellsService.SaveChanges();
                
            }

            return RedirectToAction("Index", "BlmisSells");
        }

        public ActionResult Edit(int id)
        {
            Data.HandsDB.BlmisSellHistory model = _blmisSellsService.GetById(id);
            var Viewmodel = new BlmisSells();
            Viewmodel.ProjectId = HandSession.Current.ProjectId;
            Viewmodel.UserList = _noorService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId && x.UserType == "marvi").ToList();
            Viewmodel.Amount = model.Amount;
            Viewmodel.SellDate = model.SellDate;
            Viewmodel.DayWiseAmount = model.DayWiseAmount;
            Viewmodel.YesterdayDate = model.YesterdaySelldate.ToString();
            Viewmodel.UserId = model.UserId;
            Viewmodel.CreatedAt = DateTime.Now;
            Viewmodel.IsActive = true;

            return PartialView("Edit", Viewmodel);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.Models.BlmisSells.BlmisSells model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.BlmisSellHistory existingModel = _blmisSellsService.GetById(model.Id);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.Amount = model.Amount;
                    existingModel.SellDate = model.SellDate;
                    existingModel.DayWiseAmount = model.DayWiseAmount;
                    existingModel.YesterdaySelldate = model.YesterdayDate.AsDateTime();
                    existingModel.UserId = model.UserId;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.IsActive = true;

                    _blmisSellsService.Update(existingModel);
                    _blmisSellsService.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.BlmisSellHistory existingModel = _blmisSellsService.GetById(id);
                existingModel.IsActive = false;
                _blmisSellsService.Update(existingModel);
                _blmisSellsService.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        ////[HttpGet]
        ////public ActionResult ExportIndex()
        ////{
        ////    string ExcelName = "test.xlsx";
        ////    string ZipName = "test.zip";
        ////    // Using EPPlus from nuget
        ////    using (var stream = new MemoryStream())
        ////    {
        ////        using (ExcelPackage package = new ExcelPackage())
        ////        {
        ////            Int32 row = 2;
        ////            Int32 col = 1;

        ////            package.Workbook.Worksheets.Add("Data");
        ////            IGrid<Hands.Data.HandsDB.UsersStock> grid = CreateExportableGrid();
        ////            ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

        ////            foreach (IGridColumn column in grid.Columns)
        ////            {
        ////                sheet.Cells[1, col].Value = column.Title;
        ////                sheet.Column(col++).Width = 18;
        ////            }

        ////            foreach (IGridRow<Hands.Data.HandsDB.UsersStock> gridRow in grid.Rows)
        ////            {
        ////                col = 1;
        ////                foreach (IGridColumn column in grid.Columns)
        ////                    sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

        ////                row++;
        ////            }

        ////            return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        ////            //return File(
        ////            //    stream.ToArray(),
        ////            //    "application/xlsx",
        ////            //    ExcelName
        ////            //);
        ////        }
        ////    }
        ////}

        ////private IGrid<Hands.Data.HandsDB.UsersStock> CreateExportableGrid()
        ////{
        ////    IGrid<Hands.Data.HandsDB.UsersStock> grid = new Grid<Hands.Data.HandsDB.UsersStock>(_blmisService.GetAllActive());
        ////    grid.ViewContext = new ViewContext { HttpContext = HttpContext };
        ////    grid.Query = Request.QueryString;
        ////    grid.Columns.Add(model => model.Quantity).Titled("Quantity");
        ////    grid.Columns.Add(model => model.CreatedAt).Titled("Stock Date");

        ////    grid.Pager = new GridPager<Hands.Data.HandsDB.UsersStock>(grid);
        ////    grid.Processors.Add(grid.Pager);
        ////    grid.Pager.PageSizes = new Dictionary<int, string>()
        ////    {
        ////        {0 ,"All" }
        ////    };
        ////    grid.Pager.ShowPageSizes = true;

        ////    foreach (IGridColumn column in grid.Columns)
        ////    {
        ////        column.Filter.IsEnabled = true;
        ////        column.Sort.IsEnabled = true;
        ////    }

        ////    return grid;
        ////}

        ////public ActionResult Import()
        ////{
        ////    return View();
        ////}

        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Import(HttpPostedFileBase upload)
        ////{

        ////    if (ModelState.IsValid)
        ////    {
        ////        if (upload != null && upload.ContentLength > 0)
        ////        {
        ////            try
        ////            {
        ////                var stream = upload.InputStream;
        ////                IExcelDataReader reader = null;
        ////                if (upload.FileName.EndsWith(".xls"))
        ////                {
        ////                    reader = ExcelReaderFactory.CreateBinaryReader(upload.InputStream);
        ////                }
        ////                else if (upload.FileName.EndsWith(".xlsx"))
        ////                {
        ////                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        ////                }
        ////                reader.IsFirstRowAsColumnNames = true;
        ////                var result = reader.AsDataSet();
        ////                reader.Close();



        ////                string sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Hands.Web.Properties.Settings.HandsDBConnection"].ConnectionString;
        ////                SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);
        ////                bulkInsert.DestinationTableName = "users_stock";
        ////                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("users_stock_id", "users_stock_id"));
        ////                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("product_id", "product_id"));
        ////                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_type", "user_type"));
        ////                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("user_id", "user_id"));
        ////                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("quantity", "quantity"));
        ////                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Price", "Price"));
        ////                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));
        ////                bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("IsActive", "IsActive"));
        ////                bulkInsert.WriteToServer(result.Tables["Data"]);

        ////            }
        ////            catch (Exception e)
        ////            {
        ////                ModelState.AddModelError("Record", "No Records updated.");
        ////                ModelState.AddModelError("Record", e.Message);
        ////                //return View(iStatus);
        ////            }
        ////        }
        ////    }
        ////    return View();
        ////}

    }


}