using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hands.Common.Common;
using Hands.Service.BlmisSells;
using Hands.Service.CLMIS;
using Hands.Service.CurrentLhv;
using Hands.Service.Dashboard;
using Hands.Service.DashboardCount;
using Hands.Service.MethodwiseCount;
using Hands.Service.MwraLhvwiseCount;
using Hands.Service.NewCurrentMonth;
using Hands.Service.NewuserLhv;
using Hands.Service.Regions;
using Hands.ViewModels.Models.Graph;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IDashboardCountService _dashboardCountService;
        private readonly IMwraLhvwiseCountService _mwraLhvwiseCountService;
        private readonly ICurrentLhvService _currentLhvService;
        private readonly INewuserLhvService _newuserLhvService;
        private readonly INewCurrentMonthLhvService _currentMonthLhvService;
        private readonly IMethodwiseCountService _methodwiseCountService;
        private readonly IBlmisSellsService _blmisSellsService;
        private readonly INewCurrentMonthLhvService _NewCurrentMonthLhvService;
        private readonly IRegionService _regionService;

        public HomeController()
        {
            _dashboardService = new DashboardService();
            _dashboardCountService = new DashboardCountService();
            _mwraLhvwiseCountService = new MwraLhvwiseCountService();
            _currentLhvService = new CurrentLhvService();
            _newuserLhvService = new NewuserLhvService();
            _currentMonthLhvService = new NewCurrentMonthLhvService();
            _methodwiseCountService = new MethodwiseCountService();
            _blmisSellsService = new BlmisSellsService();
            _NewCurrentMonthLhvService = new NewCurrentMonthLhvService();
            _regionService = new RegionService();
        }

        public ActionResult Index()
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            model.Regions = _regionService.GetAll().Where(x => x.ProjectId == HandSession.Current.ProjectId && x.IsActive == true).ToList();
            model.BibDate = DateTime.Now.ToString("MM/dd/yyyy");
            model.BlmisSalesList = _blmisSellsService.GetBlmisSalesByMonth(DateTime.Now.ToString());


            model.Dashboardlist = _dashboardCountService.GetDashboardList();
            model.liveProgressForVisitInCommunityDaysWiseList = _dashboardService.GetVisitInCommunityDaysWise();
            model.liveProgressForVisitInCommunityMonthsWiseList = _dashboardService.GetVisitInCommunityMonthsWise();
            model.liveProgressForVisitInCommunityYearsWiseList = _dashboardService.GetVisitInCommunityYearsWise();
            model.CurrentUserMethodWiselist = _methodwiseCountService.CurrentUserMethodWiseList();
            model.newUserCurrentMonths = _currentMonthLhvService.GetCurrentmonthUserLhvlist();
            model.spTestUnmetNeedInMwras = _dashboardService.GetSpTestUnmetNeedInMwras();
            model.spTestCurrentUsers = _dashboardService.GetSpTestCurrentUsers();
            model.NewUserMethodWiseList = _dashboardService.GetNewUserMethodWise();
            model.SppregnantWomanReturns = _dashboardService.GetSppregnantWomanReturns();
            model.newUserEachLhvReturns = _dashboardService.GetNewUserEachLhvReturns();
            model.McprList = _dashboardService.GetMcpr();
            model.liveProgressForVisitMarviInCommunityDaysWiseList = _dashboardService.GetVisitInCommunityMarviDaysWise();
            model.liveProgressForVisitMarviInCommunityMonthsWiseList = _dashboardService.GetVisitInCommunityMarviMonthsWise();
            model.liveProgressForVisitMarviInCommunityYearsWiseList = _dashboardService.GetVisitInCommunityMarviYearsWise();

            return View(model);
        }

        public JsonResult DashboardResult()
        {
            var models = new Hands.ViewModels.Models.Dashboardcount.Dashboardcount();
            models.Dashboardlist = _dashboardCountService.GetDashboardList();

            return Json(models, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult InventionArea(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.InterventionAreaList = _dashboardService.GetInterventionArea(int.Parse(regionId));
            }
            else
                model.InterventionAreaList = _dashboardService.GetInterventionArea();

            //if (exportToCSV == 1)
            //{
            //    ExportController.ExportExcel(
            //                                 ExportController.RenderViewToString(
            //                                                                     ControllerContext,
            //                                                                     "~/Views/Home/_InventionAreaPartial.cshtml",
            //                                                                     model),
            //                                 "",
            //                                 ""

            //                                );
            //    return null;
            //}

            return PartialView("_inventionAreaPartial", model);
        }

        [HttpGet]
        public ActionResult ExportInventionArea()
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
                    IGrid<Data.HandsDB.SpTestInterventionAreaReturnModel> grid = CreateExportableInventionAreaGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpTestInterventionAreaReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InventionArea.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpTestInterventionAreaReturnModel> CreateExportableInventionAreaGrid()
        {

            IGrid<Hands.Data.HandsDB.SpTestInterventionAreaReturnModel> grid = new Grid<Hands.Data.HandsDB.SpTestInterventionAreaReturnModel>(_dashboardService.GetInterventionArea());

            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.taluqa_name).Titled("Taluqa Name");
            grid.Columns.Add(model => model.NoOFNoors).Titled("No Of Noors");
            grid.Columns.Add(model => model.NoOfUcs).Titled("No Of UCS");
            
            grid.Pager = new GridPager<Hands.Data.HandsDB.SpTestInterventionAreaReturnModel>(grid);
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

        [HttpGet]
        public ActionResult AliveChildrenResult(string startDate, string endDate)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(startDate))
                startdate = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(endDate))
                enddate = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);

            enddate = enddate.AddDays(1);

            model.ALiveChildList = _dashboardService.GetChild(startdate, enddate).ToList();

            return PartialView("_AliveChildrenPartial", model.ALiveChildList);
        }

        [HttpGet]
        public ActionResult ExportAliveChildren()
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
                    IGrid<Data.HandsDB.SpAliveChildReturnModel> grid = CreateExportableAliveChildrenGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpAliveChildReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AliveChildren.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpAliveChildReturnModel> CreateExportableAliveChildrenGrid()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.SpAliveChildReturnModel> grid = new Grid<Hands.Data.HandsDB.SpAliveChildReturnModel>(_dashboardService.GetChild(startdate, enddate));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.no_of_alive_children).Titled("No of Alive Children");
            grid.Columns.Add(model => model.mwraCount).Titled("Mwra Count");
            grid.Columns.Add(model => model.CurrentUserCount).Titled("Current User Count");
            grid.Columns.Add(model => model.NewUserCount).Titled("New User Count");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpAliveChildReturnModel>(grid);
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

        [HttpGet]
        public ActionResult MarviWorkersLhvWiseResult(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.LhvListNames = _dashboardService.GetlhvMavraCountByRegionId(int.Parse(regionId));
            }
            else
                model.LhvListNames = _dashboardService.GetlhvMavraCountByRegionId();

            return PartialView("_MarviWorkersLhvWisePartial", model.LhvListNames);
        }

        [HttpGet]
        public ActionResult MwraForEachLhvResult(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.MwraLhvNameslist = _mwraLhvwiseCountService.MwraForEachLhvByRegionId(int.Parse(regionId));
            }
            else
                model.MwraLhvNameslist = _mwraLhvwiseCountService.MwraForEachLhvByRegionId();

            return PartialView("_MwraForEachLhvPartial", model.MwraLhvNameslist);
        }

        [HttpGet]
        public ActionResult ContinuedUsersLhvWiseResult(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.CurrentLhvlist = _currentLhvService.CurrentLhvByRegionId(int.Parse(regionId));
            }
            else
                model.CurrentLhvlist = _currentLhvService.CurrentLhvByRegionId();

            return PartialView("_ContinuedUsersLhvWisePartial", model.CurrentLhvlist);
        }

        [HttpGet]
        public ActionResult NewUserForEachLhvResult(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.NewUserLhvlist = _newuserLhvService.NewUserEachLhvByRegionId(int.Parse(regionId));
            }
            else
                model.NewUserLhvlist = _newuserLhvService.NewUserEachLhvByRegionId();


            return PartialView("_NewUserForEachLhvPartial", model.NewUserLhvlist);
        }



        [HttpGet]
        public ActionResult CurrentUserNewMonthResult(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.NewUserCurrentMonthsByAllRegions = _NewCurrentMonthLhvService.NewUserCurrentMonthByRegionId(int.Parse(regionId));
            }
            else
                model.NewUserCurrentMonthsByAllRegions = _NewCurrentMonthLhvService.NewUserCurrentMonthByRegionId();

            return PartialView("_NewUserCurrentMonth", model);
        }

        [HttpGet]
        public ActionResult ExportCurrentUserNewMonth()
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
                    IGrid<Data.HandsDB.NewUserCurrentMonthByRegionIdReturnModel> grid = CreateExportableCurrentUserNewMonth();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.NewUserCurrentMonthByRegionIdReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CurrentUserNewMonth.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.NewUserCurrentMonthByRegionIdReturnModel> CreateExportableCurrentUserNewMonth()
        {

            
            IGrid<Hands.Data.HandsDB.NewUserCurrentMonthByRegionIdReturnModel> grid = new Grid<Hands.Data.HandsDB.NewUserCurrentMonthByRegionIdReturnModel>(_NewCurrentMonthLhvService.NewUserCurrentMonthByRegionId());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.Users).Titled("Users");
            grid.Columns.Add(model => model.CurrentMonthUser).Titled("Current Month User");

            grid.Pager = new GridPager<Hands.Data.HandsDB.NewUserCurrentMonthByRegionIdReturnModel>(grid);
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


        [HttpGet]
        public ActionResult NewUserMethodWiseResult(string regionId, string startDate, string endDate)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(startDate))
                startdate = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(endDate))
                enddate = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(regionId))
            {
                model.SpNewUserMethodWiseByRegionId = _dashboardService.GetNewUserMethodWiseByRegionId(startdate, enddate, int.Parse(regionId));
            }
            else
                model.SpNewUserMethodWiseByRegionId = _dashboardService.GetNewUserMethodWiseByRegionId(startdate, enddate);

            return PartialView("_NewUserMethodWise", model);
        }

        [HttpGet]
        public ActionResult ExportNewUserMethodWise()
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
                    IGrid<Data.HandsDB.SpNewUserMethodWiseByRegionIdReturnModel> grid = CreateExportableNewUserMethodWise();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpNewUserMethodWiseByRegionIdReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NewUserMethodWise.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpNewUserMethodWiseByRegionIdReturnModel> CreateExportableNewUserMethodWise()
        {
            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;


            IGrid<Hands.Data.HandsDB.SpNewUserMethodWiseByRegionIdReturnModel> grid = new Grid<Hands.Data.HandsDB.SpNewUserMethodWiseByRegionIdReturnModel>(_dashboardService.GetNewUserMethodWiseByRegionId(startdate, enddate));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.product_name).Titled("Product Name");
            grid.Columns.Add(model => model.NewUser).Titled("Current Month User");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpNewUserMethodWiseByRegionIdReturnModel>(grid);
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

        [HttpGet]
        public ActionResult BlmisSaleResult(string date)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();
            DateTime selecteddate = DateTime.ParseExact(date, "MM/dd/yyyy", null);
            model.BlmisSalesList = _blmisSellsService.GetBlmisSalesByMonth(selecteddate.ToString()).ToList();

            return PartialView("_BlmisSalePartial", model.BlmisSalesList);
        }

        [HttpGet]
        public ActionResult ExportBlmisSale()
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
                    IGrid<Data.HandsDB.GetBlmisSalesByMonthReturnModel> grid = CreateExportableBlmisSale();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.GetBlmisSalesByMonthReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BlmisSale.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.GetBlmisSalesByMonthReturnModel> CreateExportableBlmisSale()
        {
            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;


            IGrid<Hands.Data.HandsDB.GetBlmisSalesByMonthReturnModel> grid = new Grid<Hands.Data.HandsDB.GetBlmisSalesByMonthReturnModel>(_blmisSellsService.GetBlmisSalesByMonth(DateTime.Now.ToString("MM/dd/yyyy")));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.full_name).Titled("Full Name");
            grid.Columns.Add(model => model.SellDate).Titled("Sell Date");
            grid.Columns.Add(model => model.Amount.ToString("N2")).Titled("Amount");

            grid.Pager = new GridPager<Hands.Data.HandsDB.GetBlmisSalesByMonthReturnModel>(grid);
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

        [HttpGet]
        public ActionResult MarviClientGenerationResult(string regionId, string startDate, string endDate)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(startDate))
                startdate = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(endDate))
                enddate = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);


            if (!string.IsNullOrEmpty(regionId))
            {
                model.MarviClientGenerationReportList = _dashboardService.GetMarviClientGenerationReport(startdate, enddate, int.Parse(regionId));
            }
            else
                model.MarviClientGenerationReportList = _dashboardService.GetMarviClientGenerationReport(startdate, enddate);

            return PartialView("_MarviClientGenerationReport", model);
        }

        [HttpGet]
        public ActionResult ExportMarviClientGeneration()
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
                    IGrid<Data.HandsDB.MarviClientGenerationReportReturnModel> grid = CreateExportableMarviClientGeneration();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.MarviClientGenerationReportReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MarviClientGeneration.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.MarviClientGenerationReportReturnModel> CreateExportableMarviClientGeneration()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.MarviClientGenerationReportReturnModel> grid = new Grid<Hands.Data.HandsDB.MarviClientGenerationReportReturnModel>(_dashboardService.GetMarviClientGenerationReport(startdate, enddate));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.LhvName).Titled("Lhv Name");
            grid.Columns.Add(model => model.MarviName).Titled("Marvi Name");
            grid.Columns.Add(model => model.TotalClient).Titled("Total Client");

            grid.Pager = new GridPager<Hands.Data.HandsDB.MarviClientGenerationReportReturnModel>(grid);
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


        [HttpGet]
        public ActionResult TargetPopulationResult(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.TargetPopuplationList = _dashboardService.GetTargetPopuplation(int.Parse(regionId));
            }
            else
                model.TargetPopuplationList = _dashboardService.GetTargetPopuplation();

            return PartialView("_targetPopulationPartial", model);
        }

        [HttpGet]
        public ActionResult ExportPopulationResult()
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
                    IGrid<Data.HandsDB.SpTargetPopuplationReturnModel> grid = CreateExportablePopulationResult();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpTargetPopuplationReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TargetPopulation.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpTargetPopuplationReturnModel> CreateExportablePopulationResult()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.SpTargetPopuplationReturnModel> grid = new Grid<Hands.Data.HandsDB.SpTargetPopuplationReturnModel>(_dashboardService.GetTargetPopuplation());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.taluqa_name).Titled("Taluqa Name");
            grid.Columns.Add(model => model.Mwras).Titled("Mwras");
            grid.Columns.Add(model => model.TotalPopulation).Titled("Total Population");
            grid.Columns.Add(model => model.noOfHouseHolds).Titled("no Of House Holds");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpTargetPopuplationReturnModel>(grid);
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


        [HttpGet]
        public ActionResult LhvDetailsResult(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.LhvDetailsList = _dashboardService.GetDetailslhv(int.Parse(regionId));
            }
            else
                model.LhvDetailsList = _dashboardService.GetDetailslhv();

            return PartialView("_LhvDetailsReport", model);
        }

        [HttpGet]
        public ActionResult ExportLhvDetails()
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
                    IGrid<Data.HandsDB.SpLhvDetailsReturnModel> grid = CreateExportableLhvDetails();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpLhvDetailsReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LhvDetails.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpLhvDetailsReturnModel> CreateExportableLhvDetails()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.SpLhvDetailsReturnModel> grid = new Grid<Hands.Data.HandsDB.SpLhvDetailsReturnModel>(_dashboardService.GetDetailslhv());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.LHVName).Titled("LHV Name");
            grid.Columns.Add(model => model.MarviCount).Titled("Marvi Count");
            grid.Columns.Add(model => model.mwraCount).Titled("Mwra Count");
            
            grid.Pager = new GridPager<Hands.Data.HandsDB.SpLhvDetailsReturnModel>(grid);
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


        [HttpGet]
        public ActionResult MwraAgeWiseReport(string regionId, string startDate, string endDate)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(startDate))
                startdate = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(endDate))
                enddate = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(regionId))
            {
                model.AgeWiseReportList = _dashboardService.GetSpMwras(startdate, enddate, int.Parse(regionId));
            }
            else
                model.AgeWiseReportList = _dashboardService.GetSpMwras(startdate, enddate);

            return PartialView("_MwraAgeWiseReport", model);
        }

        [HttpGet]
        public ActionResult ExportMwraAgeWise()
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
                    IGrid<Data.HandsDB.SpMwrasAgeWiseReportReturnModel> grid = CreateExportableMwraAgeWise();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpMwrasAgeWiseReportReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MwraAgeWise.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpMwrasAgeWiseReportReturnModel> CreateExportableMwraAgeWise()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.SpMwrasAgeWiseReportReturnModel> grid = new Grid<Hands.Data.HandsDB.SpMwrasAgeWiseReportReturnModel>(_dashboardService.GetSpMwras(startdate, enddate));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.ageGroup).Titled("ageGroup");
            grid.Columns.Add(model => model.MWRA).Titled("MWRA");
            grid.Columns.Add(model => model.CurrentUsers).Titled("CurrentUsers");
            grid.Columns.Add(model => model.NewUsers).Titled("NewUsers");
            grid.Columns.Add(model => model.UnmetNeed).Titled("UnmetNeed");
            grid.Columns.Add(model => model.NewUserUnmetNeed).Titled("NewUserUnmetNeed");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpMwrasAgeWiseReportReturnModel>(grid);
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


        [HttpGet]
        public ActionResult DetailsOfShiftedClientsResult(string regionId, string startDate, string endDate)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(startDate))
                startdate = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(endDate))
                enddate = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);


            if (!string.IsNullOrEmpty(regionId))
            {
                model.spTestDetailsOfShiftedClients = _dashboardService.GetSpTestDetails(startdate, enddate, int.Parse(regionId));
            }
            else
                model.spTestDetailsOfShiftedClients = _dashboardService.GetSpTestDetails(startdate, enddate);
            return PartialView("_DetailsOfShiftedClient", model);
        }


        [HttpGet]
        public ActionResult ExportDetailsOfShiftedClients()
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
                    IGrid<Data.HandsDB.SpTestDetailsOfShiftedClientsReturnModel> grid = CreateExportableDetailsOfShiftedClients();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpTestDetailsOfShiftedClientsReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DetailsOfShiftedClients.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpTestDetailsOfShiftedClientsReturnModel> CreateExportableDetailsOfShiftedClients()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.SpTestDetailsOfShiftedClientsReturnModel> grid = new Grid<Hands.Data.HandsDB.SpTestDetailsOfShiftedClientsReturnModel>(_dashboardService.GetSpTestDetails(startdate, enddate));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.Product).Titled("Product");
            grid.Columns.Add(model => model.Condom).Titled("Condom");
            grid.Columns.Add(model => model.Injection).Titled("Injection");
            grid.Columns.Add(model => model.IUCD).Titled("IUCD");
            grid.Columns.Add(model => model.Pills).Titled("Pills");
            grid.Columns.Add(model => model.TL).Titled("TL");
            grid.Columns.Add(model => model.Total).Titled("Total");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpTestDetailsOfShiftedClientsReturnModel>(grid);
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


        [HttpGet]
        public ActionResult FpUsersTalukaWiseResult(string startDate, string endDate)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(startDate))
                startdate = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(endDate))
                enddate = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);


            model.FpUsersList = _dashboardService.GetFpUser(startdate, enddate).ToList();
            return PartialView("_FpUserTaluqaWisePartial", model.FpUsersList);
        }


        [HttpGet]
        public ActionResult ExportFpUsersTalukaWise()
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
                    IGrid<Data.HandsDB.SpFpUsersTaluqaTehsilwiseReturnModel> grid = CreateExportableFpUsersTalukaWise();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpFpUsersTaluqaTehsilwiseReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "FpUsersTalukaWise.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpFpUsersTaluqaTehsilwiseReturnModel> CreateExportableFpUsersTalukaWise()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.SpFpUsersTaluqaTehsilwiseReturnModel> grid = new Grid<Hands.Data.HandsDB.SpFpUsersTaluqaTehsilwiseReturnModel>(_dashboardService.GetFpUser(startdate, enddate));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.taluqa_name).Titled("taluqa_name");
            grid.Columns.Add(model => model.NOOR).Titled("NOOR");
            grid.Columns.Add(model => model.CurrentUser).Titled("CurrentUser");
            grid.Columns.Add(model => model.NewUser).Titled("NewUser");
            grid.Columns.Add(model => model.NewUserCurrentMonth).Titled("NewUserCurrentMonth");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpFpUsersTaluqaTehsilwiseReturnModel>(grid);
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


        public JsonResult MarviResult()
        {
            var models = new Hands.ViewModels.Models.Dashboardcount.Dashboardcount();


            return Json(models, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult TrendAmongCurrentUserResult(string regionId)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            if (!string.IsNullOrEmpty(regionId))
            {
                model.SpTrendAmongCurrentUsers = _dashboardService.GetSpTrendAmongCurrentUsers(int.Parse(regionId));
            }
            else
                model.SpTrendAmongCurrentUsers = _dashboardService.GetSpTrendAmongCurrentUsers();
            return PartialView("_TrendAmongCurrentUser", model);
        }

        [HttpGet]
        public ActionResult ExportTrendAmongCurrentUser()
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
                    IGrid<Data.HandsDB.SpTrendAmongCurrentUsersReturnModel> grid = CreateExportableTrendAmongCurrentUser();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpTrendAmongCurrentUsersReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TrendAmongCurrentUser.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpTrendAmongCurrentUsersReturnModel> CreateExportableTrendAmongCurrentUser()
        {

            IGrid<Hands.Data.HandsDB.SpTrendAmongCurrentUsersReturnModel> grid = new Grid<Hands.Data.HandsDB.SpTrendAmongCurrentUsersReturnModel>(_dashboardService.GetSpTrendAmongCurrentUsers());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.lhvName).Titled("lhvName");
            grid.Columns.Add(model => model.Injection).Titled("Injection");
            grid.Columns.Add(model => model.IUCD).Titled("IUCD");
            grid.Columns.Add(model => model.Natural).Titled("Natural");
            grid.Columns.Add(model => model.Condom).Titled("Condom");
            grid.Columns.Add(model => model.Pills).Titled("Pills");
            grid.Columns.Add(model => model.Implant).Titled("Implant");
            grid.Columns.Add(model => model.TL).Titled("TL");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpTrendAmongCurrentUsersReturnModel>(grid);
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


        [HttpGet]
        public ActionResult TrendAmongNewUserResult(string regionId, string startDate, string endDate)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(startDate))
                startdate = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(endDate))
                enddate = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);




            if (!string.IsNullOrEmpty(regionId))
            {
                model.SpTrendAmongNewUser = _dashboardService.GetSpTrendAmongNewUsers(startdate, enddate, int.Parse(regionId));
            }
            else
                model.SpTrendAmongNewUser = _dashboardService.GetSpTrendAmongNewUsers(startdate, enddate);
            return PartialView("_TrendAmongNewUser", model);
        }

        [HttpGet]
        public ActionResult ExportTrendAmongNewUser()
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
                    IGrid<Data.HandsDB.SpTrendAmongNewUsersReturnModel> grid = CreateExportableTrendAmongNewUser();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpTrendAmongNewUsersReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TrendAmongNewUser.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpTrendAmongNewUsersReturnModel> CreateExportableTrendAmongNewUser()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.SpTrendAmongNewUsersReturnModel> grid = new Grid<Hands.Data.HandsDB.SpTrendAmongNewUsersReturnModel>(_dashboardService.GetSpTrendAmongNewUsers(startdate, enddate));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.lhvName).Titled("lhvName");
            grid.Columns.Add(model => model.Injection).Titled("Injection");
            grid.Columns.Add(model => model.IUCD).Titled("IUCD");
            grid.Columns.Add(model => model.Natural).Titled("Natural");
            grid.Columns.Add(model => model.Condom).Titled("Condom");
            grid.Columns.Add(model => model.Pills).Titled("Pills");
            grid.Columns.Add(model => model.Implant).Titled("Implant");
            grid.Columns.Add(model => model.TL).Titled("TL");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpTrendAmongNewUsersReturnModel>(grid);
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


        [HttpGet]
        public ActionResult DetailMcprResult(string regionId, string startDate, string endDate)
        {
            var model = new Hands.ViewModels.Models.Graph.Graph();

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            if (!string.IsNullOrEmpty(startDate))
                startdate = DateTime.ParseExact(startDate, "MM/dd/yyyy", null);

            if (!string.IsNullOrEmpty(endDate))
                enddate = DateTime.ParseExact(endDate, "MM/dd/yyyy", null);




            if (!string.IsNullOrEmpty(regionId))
            {
                model.spDetailMcprReturns = _dashboardService.SpDetailMcprReturns(startdate, enddate, int.Parse(regionId));
            }
            else
                model.spDetailMcprReturns = _dashboardService.SpDetailMcprReturns(startdate, enddate);
            return PartialView("_DetailsMcpr", model);
        }

        [HttpGet]
        public ActionResult ExportDetailMcpr()
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
                    IGrid<Data.HandsDB.SpDetailMcprReturnModel> grid = CreateExportableDetailMcpr();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpDetailMcprReturnModel> gridRow in grid.Rows)
                    {
                        col = 1;
                        foreach (IGridColumn column in grid.Columns)
                            sheet.Cells[row, col++].Value = column.ValueFor(gridRow);

                        row++;
                    }

                    return File(package.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DetailMcpr.xlsx");
                    //return File(
                    //    stream.ToArray(),
                    //    "application/xlsx",
                    //    ExcelName
                    //);
                }
            }
        }

        private IGrid<Hands.Data.HandsDB.SpDetailMcprReturnModel> CreateExportableDetailMcpr()
        {

            DateTime startdate = new DateTime();
            DateTime enddate = DateTime.Now;

            IGrid<Hands.Data.HandsDB.SpDetailMcprReturnModel> grid = new Grid<Hands.Data.HandsDB.SpDetailMcprReturnModel>(_dashboardService.SpDetailMcprReturns(startdate, enddate));
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.taluqa_name).Titled("taluqa_name");
            grid.Columns.Add(model => model.TotalFpUsers).Titled("TotalFpUsers");
            grid.Columns.Add(model => model.CurrentUser).Titled("CurrentUser");
            grid.Columns.Add(model => model.NewUser).Titled("NewUser");
            grid.Columns.Add(model => model.MWRA).Titled("MWRA");
            grid.Columns.Add(model => model.TotalFpUsers).Titled("TotalFpUsers");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpDetailMcprReturnModel>(grid);
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