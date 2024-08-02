using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Events;
using Hands.ViewModels.Models.Events;
using System.IO;
using OfficeOpenXml;
using NonFactors.Mvc.Grid;
using System.Data.SqlClient;
using Excel;

using Hands.Common.Common;
using Hands.Service.Message_Post_FCM;

namespace Hands.Web.Controllers
{
    public class PushEventController : ControllerBase
    {
        private readonly EventService _eventService;
        private readonly IFCMService _fcmService;

        public PushEventController()
        {
            _eventService = new EventService();
            _fcmService = new FCMService();

        }

        public ActionResult Event()
        {
            // GET: Events
            var schemeList = _eventService.GetAllActive().Where(x=>x.ProjectId == HandSession.Current.ProjectId).OrderByDescending(x=>x.EventId);
            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }

        public ActionResult PushEvenNotification()
        {
            // GET: Events
            var schemeList = _eventService.GetAllActive().OrderByDescending(x => x.EventId);
            if (Request.IsAjaxRequest())
            {
                return Json(schemeList, JsonRequestBehavior.AllowGet);
            }

            return Json(schemeList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {

            var model = new Event();

            return View(model);

        }

        [HttpPost]
        public ActionResult Create(ViewModels.Models.Events.Event model)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.PushEvent ModelToSAve = new Data.HandsDB.PushEvent();

                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.Title = model.Title;
                ModelToSAve.Description = model.Description;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _eventService.Insert(ModelToSAve);

                _eventService.SaveChanges();



                _fcmService.SendPushToUser(model.Title, "", "PushEvent", "PushEvent",
                    "hands://PushEvent");
            }

            return RedirectToAction("Event");

        }

        public ActionResult Edit(int id)
        {
            Data.HandsDB.PushEvent model = _eventService.GetById(id);
            var viewmodel = new ViewModels.Models.Events.Event();
            viewmodel.EventId = model.EventId;
            ;
            viewmodel.Title = model.Title;
            viewmodel.Description = model.Description;
            viewmodel.CreatedAt = model.CreatedAt;
                      return PartialView("Edit", viewmodel);
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Events.Event model)
        {
            if (ModelState.IsValid)
            {
                PushEvent existingModel = _eventService.GetById(model.EventId);
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.EventId = model.EventId;
                    existingModel.Title = model.Title;
                    existingModel.Description = model.Description;
                    existingModel.CreatedAt = DateTime.Now;
                    existingModel.IsActive = true;
                    _eventService.Update(existingModel);
                    _eventService.SaveChanges();
                }
            }

            return RedirectToAction("Event");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                PushEvent existingModel = _eventService.GetById(id);
                existingModel.IsActive = false;
                _eventService.Update(existingModel);
                _eventService.SaveChanges();
            }

            return RedirectToAction("Event");


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
                    IGrid<Hands.Data.HandsDB.PushEvent> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.PushEvent> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.PushEvent> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.PushEvent> grid = new Grid<Hands.Data.HandsDB.PushEvent>(_eventService.GetAllActive());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.Title).Titled("Title");
            grid.Columns.Add(model => model.Description).Titled("Description");
            grid.Columns.Add(model => model.CreatedAt).Titled("Created Date");


            grid.Pager = new GridPager<Hands.Data.HandsDB.PushEvent>(grid);
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
                        bulkInsert.DestinationTableName = "push_events";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("event_id", "event_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("title", "title"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("description", "description"));
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