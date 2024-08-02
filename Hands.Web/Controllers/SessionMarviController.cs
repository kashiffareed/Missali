using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Service.Session;
using Hands.Service.SessionFollowup;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class SessionMarviController : ControllerBase
    {
        // GET: SessionMarvi
        private readonly ISessioncallService _sessioncallService;
        private readonly ISessionfollowupService _sessionfollowupService;

        public SessionMarviController()
        {
            _sessioncallService = new SessioncallService();
            _sessionfollowupService = new SessionfollowupService();

        }
        public ActionResult Index()
        {

            var model = new Hands.ViewModels.Models.sessioncall.Sessioncall();
            model.SesstioncallmarviList = _sessioncallService.GetAllSessionCallMarviWithNames();
            return View(model.SesstioncallmarviList);
        }

        public ActionResult Edit(int id)
        {
            Data.HandsDB.Session user = _sessioncallService.GetById(id);
            var model = new ViewModels.Models.sessioncall.Sessioncall();
            model.SessionId = user.SessionId;
            model.Longitude = user.Longitude;
            model.Latitude = user.Latitude;

            return View(model);
        }

        public ActionResult SessionContent(int sessionId)
        {

            var model = new Hands.ViewModels.Models.sessioncall.Sessioncall();
            model.SesstionContentList = _sessioncallService.GetSessionContentBysessionId(sessionId);
            return View(model.SesstionContentList);

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
                    IGrid<Hands.Data.HandsDB.GetSessionCallmarviListReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.GetSessionCallmarviListReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.GetSessionCallmarviListReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.GetSessionCallmarviListReturnModel> grid = new Grid<Hands.Data.HandsDB.GetSessionCallmarviListReturnModel>(_sessioncallService.GetAllSessionCallMarviWithNames());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.RowNumber).Titled("S.NO");
            grid.Columns.Add(model => model.created_at).Titled("Session Date");
            grid.Columns.Add(model => model.session_start_datetime).Titled("Start Time");
            grid.Columns.Add(model => model.session_end_datetime).Titled("End Time");
            grid.Columns.Add(model => model.user_type).Titled("Session Type");
            grid.Columns.Add(model => model.noorName).Titled("Marvi");
            grid.Columns.Add(model => model.lhvName).Titled("Lhv");
            grid.Columns.Add(model => model.is_group).Titled("Group Session");
            grid.Columns.Add(model => model.next_session_schedule).Titled("Next Session Date");


            grid.Pager = new GridPager<Hands.Data.HandsDB.GetSessionCallmarviListReturnModel>(grid);
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
    }
}