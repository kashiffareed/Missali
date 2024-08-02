using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.Service.Categories;
using Hands.Service.Pms;
using Hands.ViewModels.Models.Pms;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Configuration;
using Excel;
using System.Data.SqlClient;
using System.Linq;
using Hands.Common.Common;

namespace Hands.Web.Controllers
{
    public class PmsController : ControllerBase
    {
        private readonly IPmsService _pmsService;
        private readonly ICategoriesService _categoriesService;
        public PmsController()
        {
            _pmsService = new PmsService();
            _categoriesService = new CategoriesService();
        }

        public ActionResult Index()
        {
            // GET: Noor
            var schemeList = _pmsService.GetAllPmsList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("Index", schemeList);
            }

            return View(schemeList);
        }

        public ActionResult Create()
        {
            var model = new Pms();
            model.CategoryList = _categoriesService.GetAllActive().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            model.ContentTypeList = _pmsService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
            return View(model);

        }

        [HttpPost]
        public ActionResult Create(Pms model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Pm ModelToSAve = new Data.HandsDB.Pm();
                var httpPostedFile = Request.Files["file"];
                if (httpPostedFile.FileName != "")
                {
                    var uploadFilesDir =
                        System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/English");
                    if (!Directory.Exists(uploadFilesDir))
                    {
                        Directory.CreateDirectory(uploadFilesDir);
                    }
                    var fileSavePath = Path.Combine(uploadFilesDir, httpPostedFile.FileName);
                    if (System.IO.File.Exists(fileSavePath))
                    {
                        System.IO.File.Delete(fileSavePath);
                    }
                    httpPostedFile.SaveAs(fileSavePath);
                    ModelToSAve.Path = $"{ConfigurationSettings.AppSettings["VirtualPath"]}/Content/Images/English/" + httpPostedFile.FileName;
                    if (Request.Files.Count > 0)
                    {
                        var httpPostedFileImage = Request.Files["file"];
                        if (httpPostedFileImage.FileName != string.Empty)
                        {
                            if (httpPostedFileImage != null)
                            {
                                var fileSavePathImage = Path.Combine(uploadFilesDir, httpPostedFileImage.FileName.Replace(" ", "_"));

                                model.Path = fileSavePathImage;
                            }
                        }
                    }
                }
                ModelToSAve.ProjectId = HandSession.Current.ProjectId;
                ModelToSAve.CategoryId = model.CategoryId;
                ModelToSAve.ContentId = model.ContentId;
                ModelToSAve.ContentName = model.ContentName;
                ModelToSAve.ContentNameSindhi = model.ContentNameSindhi;
                ModelToSAve.ContentNameUrdu = model.ContentNameUrdu;
                //ModelToSAve.Path = model.Path;
                //ModelToSAve.PathUrdu = model.PathUrdu;
                //ModelToSAve.PathSindhi = model.PathSindhi;
                ModelToSAve.ContentType = model.ContentType;
                ModelToSAve.CreatedAt = DateTime.Now;
                ModelToSAve.IsActive = true;
                _pmsService.Insert(ModelToSAve);
                _pmsService.SaveChanges();
                TempData["status"] = 1;
            }
            return RedirectToAction("Index", "Pms");


        }

        public ActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                Data.HandsDB.Pm model = _pmsService.GetById(id);
                var Viewmodel = new ViewModels.Models.Pms.Pms();
                Viewmodel.CategoryList = _categoriesService.GetAllActive().Where(x => x.ProjectId == HandSession.Current.ProjectId);
                Viewmodel.ContentTypeList = _pmsService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId);
                Viewmodel.CategoryId = model.CategoryId;
                Viewmodel.ContentId = model.ContentId;
                Viewmodel.ContentName = model.ContentName;
                Viewmodel.ContentNameSindhi = model.ContentNameSindhi;
                Viewmodel.ContentNameUrdu = model.ContentNameUrdu;
                Viewmodel.Path = model.Path;
                Viewmodel.PathUrdu = model.PathUrdu;
                Viewmodel.PathSindhi = model.PathSindhi;
                Viewmodel.ContentType = model.ContentType;
                Viewmodel.CreatedAt = DateTime.Now;
                return View(Viewmodel);
            } 
            
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.Models.Pms.Pms model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Pm existingModel = _pmsService.GetById(model.ContentId);
                var httpPostedFile = Request.Files["file"];
                if (httpPostedFile.FileName != "")
                {
                    var uploadFilesDir =
                        System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/English");
                    if (!Directory.Exists(uploadFilesDir))
                    {
                        Directory.CreateDirectory(uploadFilesDir);
                    }
                    var fileSavePath = Path.Combine(uploadFilesDir, httpPostedFile.FileName);
                    if (System.IO.File.Exists(fileSavePath))
                    {
                        System.IO.File.Delete(fileSavePath);
                    }
                    httpPostedFile.SaveAs(fileSavePath);
                    existingModel.Path = $"{ConfigurationSettings.AppSettings["VirtualPath"]}/Content/Images/English/" + httpPostedFile.FileName;
                    if (Request.Files.Count > 0)
                    {
                        var httpPostedFileImage = Request.Files["file"];
                        if (httpPostedFileImage.FileName != string.Empty)
                        {
                            if (httpPostedFileImage != null)
                            {
                                var fileSavePathImage = Path.Combine(uploadFilesDir, httpPostedFileImage.FileName.Replace(" ", "_"));
                                //httpPostedFileImage.SaveAs(fileSavePathImage);
                                model.Path = fileSavePath;
                            }
                        }
                    }
                }
                if (existingModel != null)
                {
                    existingModel.ProjectId = HandSession.Current.ProjectId;
                    existingModel.CategoryId = model.CategoryId;
                    existingModel.ContentId = model.ContentId;
                    existingModel.ContentName = model.ContentName;
                    existingModel.ContentNameSindhi = model.ContentNameSindhi;
                    existingModel.ContentNameUrdu = model.ContentNameUrdu;
                    existingModel.PathUrdu = model.PathUrdu;
                    existingModel.PathSindhi = model.PathSindhi;
                    existingModel.ContentType = model.ContentType;
                    existingModel.CreatedAt = DateTime.Now;


                    _pmsService.Update(existingModel);
                    _pmsService.SaveChanges();
                    TempData["status"] = 2;
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (id != null)
            {
                var existingModel = _pmsService.GetById(id);
                existingModel.IsActive = false;
                _pmsService.Update(existingModel);
                _pmsService.SaveChanges();
                TempData["status"] = 3;
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
                    IGrid<Hands.Data.HandsDB.SpContentpmsReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpContentpmsReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.SpContentpmsReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpContentpmsReturnModel> grid = new Grid<Hands.Data.HandsDB.SpContentpmsReturnModel>(_pmsService.GetAllPmsList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.content_name).Titled("TITLE (ENGLISH)");
            grid.Columns.Add(model => model.content_name_urdu).Titled("TITLE (URDU)");
            grid.Columns.Add(model => model.content_name_sindhi).Titled("TITLE (SINDHI)");
            grid.Columns.Add(model => model.category_name).Titled("CATEGORY");
            grid.Columns.Add(model => model.content_type).Titled("CONTENT TYPE");
            grid.Columns.Add(model => model.created_at).Titled("CREATED DATE");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpContentpmsReturnModel>(grid);
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

        public void DownloadAndTemplate(string pathCode)
        {
            var fileName = Path.GetFileName(pathCode);
            if (fileName != null)
            {
                var imgPath = Server.MapPath(pathCode);
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                Response.WriteFile(imgPath);
            }
            else
            {
                var imgPath = Server.MapPath(@"~/Content/Images/English/ValidateImage.jpg");
                Response.AddHeader("Content-Disposition", "attachment;filename=" + "ValidateImage.jpg");
                Response.WriteFile(imgPath);
            }

            Response.End();
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
                        bulkInsert.DestinationTableName = "pms";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("content_id", "content_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("content_name", "content_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("content_name_sindhi", "content_name_sindhi"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("content_name_urdu", "content_name_urdu"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("path", "path"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("path_sindhi", "path_sindhi"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("path_urdu", "path_urdu"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("category_id", "category_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("content_type", "content_type"));
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