using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Massage;
using Hands.Service.NoorChecklist;
using Hands.Service.Product;
using Massage = Hands.ViewModels.Models.Massage.PushMassage;
using System.IO;
using OfficeOpenXml;
using NonFactors.Mvc.Grid;
using System.Data.SqlClient;
using Excel;
using Hands.Common.Common;
using Hands.Service.Message_Post_FCM;


namespace Hands.Web.Controllers
{
    public class MassageController : ControllerBase
    {
        private readonly IMassageService _massageService;
        private readonly IFCMService _fcmService ;

        public MassageController()
        {
            _massageService = new MassageService();
            _fcmService = new FCMService();
        }

        // GET: RealTimeCheckList
        public ActionResult PushMessage()
        {
            var schemeList = _massageService.GetAllActive().OrderByDescending(x => x.MessageId);
            if (Request.IsAjaxRequest())
            {
                return Json(schemeList, JsonRequestBehavior.AllowGet);
            }

            return Json(schemeList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            var schemeList = _massageService.GetAllActive().Where(x=>x.ProjectId == HandSession.Current.ProjectId).OrderByDescending(x=>x.MessageId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new PushMessage();
         
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ViewModels.Models.Massage.PushMassage model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.PushMessage ModelToSAve = new Data.HandsDB.PushMessage();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
               ModelToSAve.MessageId = model.MessageId;
                ModelToSAve.Message = model.Message;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _massageService.Insert(ModelToSAve);
                _massageService.SaveChanges();

                
                _fcmService.SendPushToUser(model.Message, "", "Messages", "Messages",
                    "hands://Message");
            }

            return RedirectToAction("Index", "Massage");

        }

        public ActionResult Edit(int id)
        {
            Data.HandsDB.PushMessage model = _massageService.GetById(id);
            var Viewmodel = new ViewModels.Models.Massage.PushMassage();
            Viewmodel.MessageId = model.MessageId;
            Viewmodel.IsActive = true;
            Viewmodel.Message = model.Message;
            Viewmodel.CreatedAt = model.CreatedAt;
         
            return PartialView("Edit", Viewmodel);
        }
        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Massage.PushMassage model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.PushMessage existingModel = _massageService.GetById(model.MessageId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.MessageId = model.MessageId;
                    existingModel.Message = model.Message;
                    existingModel.IsActive = true;
                    existingModel.CreatedAt = DateTime.Now;

                    _massageService.Update(existingModel);
                    _massageService.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                Data.HandsDB.PushMessage existingModel = _massageService.GetById(id);
                existingModel.IsActive = false;
                _massageService.Update(existingModel);
                _massageService.SaveChanges();
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
                    IGrid<Hands.Data.HandsDB.PushMessage> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.PushMessage> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.PushMessage> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.PushMessage> grid = new Grid<Hands.Data.HandsDB.PushMessage>(_massageService.GetAllActive());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.Message).Titled("Message");
            grid.Columns.Add(model => model.CreatedAt).Titled("Created Date");


            grid.Pager = new GridPager<Hands.Data.HandsDB.PushMessage>(grid);
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
                        bulkInsert.DestinationTableName = "push_messages";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("message_id", "message_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("message", "message"));
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