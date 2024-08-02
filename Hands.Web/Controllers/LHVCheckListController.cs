using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Service.LhvChecklist;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class LHVCheckListController : ControllerBase
    {
        // GET: LHVCheckList


        private readonly ILhvChecklistService _lhvChecklistService;

        public LHVCheckListController()
        {
            _lhvChecklistService = new LhvChecklistService();
        }

        public ActionResult Index()
        {
            var schemeList = _lhvChecklistService.GetAllLHvClCheckList();
            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }


        public ActionResult View(int id)
        {
            Data.HandsDB.LhvChecklist model = _lhvChecklistService.GetById(id);
            return PartialView("View", model);
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
                    IGrid<Data.HandsDB.SpLhvCheckListReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpLhvCheckListReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.SpLhvCheckListReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpLhvCheckListReturnModel> grid = new Grid<Hands.Data.HandsDB.SpLhvCheckListReturnModel>(_lhvChecklistService.GetAllLHvClCheckList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.full_name).Titled("Lhv Name");
            grid.Columns.Add(model => model.visitor_name).Titled("Visitor Name");
            grid.Columns.Add(model => model.region_name).Titled("Distirct");
            //grid.Columns.Add(model => model.taluqa_name).Titled("Taluqa Name");
            //grid.Columns.Add(model => model.union_council_name).Titled("Union Council");
            grid.Columns.Add(model => model.created_at).Titled("Created_Date");
            grid.Columns.Add(model => model.iucd_insertion_kit).Titled("IUCD insertion kit");
            grid.Columns.Add(model => model.muac_tape).Titled("MUAC tape");
            grid.Columns.Add(model => model.measuring_tape).Titled("Measuring tape");
            grid.Columns.Add(model => model.stethoscope).Titled("Stethoscope");
            grid.Columns.Add(model => model.bp_apparatus).Titled("BP apparatus");
            grid.Columns.Add(model => model.weighing_machcine).Titled("Weighing machine");
            grid.Columns.Add(model => model.fetoscope).Titled("Fetoscope");
            grid.Columns.Add(model => model.thermometer).Titled("Thermometer");
            grid.Columns.Add(model => model.infection_prevention_kit).Titled("Infection prevention kit");
            grid.Columns.Add(model => model.tablet_with_charger).Titled("Tablet with charger");
            grid.Columns.Add(model => model.danger_box).Titled("Danger Box");
            grid.Columns.Add(model => model.lhv_apron).Titled("LHV apron");
            grid.Columns.Add(model => model.mec_wheel).Titled("MEC Wheel");
            grid.Columns.Add(model => model.iucd_insertion_kit_func).Titled("IUCD insertion kit");
            grid.Columns.Add(model => model.bp_apparatus_func).Titled("BP apparatus");
            grid.Columns.Add(model => model.weighing_func).Titled("Weighing machine");
            grid.Columns.Add(model => model.thermometer_func).Titled("Thermometer");
            grid.Columns.Add(model => model.infection_prevention_kit_func).Titled("Infection prevention kit");
            grid.Columns.Add(model => model.tablet_with_charger_func).Titled("Tablet with charger");
            grid.Columns.Add(model => model.danger_box_func).Titled("Danger Box");
            grid.Columns.Add(model => model.lhv_apron_func).Titled("LHV apron");
            grid.Columns.Add(model => model.provide_counseling_about_fp).Titled("Did the provider greet the client and provide counseling about FP to the client?");
            grid.Columns.Add(model => model.oral_pills).Titled("Oral pills");
            grid.Columns.Add(model => model.injections).Titled("Injections");
            grid.Columns.Add(model => model.iud).Titled("IUD");
            grid.Columns.Add(model => model.condoms).Titled("Condoms");
            grid.Columns.Add(model => model.implant).Titled("Implant");
            grid.Columns.Add(model => model.any_natural_method).Titled("Any natural method");
            grid.Columns.Add(model => model.permanent_method).Titled("Permanent method");
            grid.Columns.Add(model => model.any_other).Titled("Any other");
            grid.Columns.Add(model => model.provider_use_mec_wheel).Titled("Did the provider use the MEC wheel for support in decision of the method");
            grid.Columns.Add(model => model.check_bp).Titled("Check B.P");
            grid.Columns.Add(model => model.check_anemia_and_jaundice).Titled("Check for Anemia and Jaundice");
            grid.Columns.Add(model => model.measurement_of_weight).Titled("Measurement of weight");
            grid.Columns.Add(model => model.pelvic_exam).Titled("Pelvic exam (if IUCD) is decided");
            grid.Columns.Add(model => model.client_receive_method_choice).Titled("Did the client receive the method of choice?");
            grid.Columns.Add(model => model.explain_how_to_use).Titled("Explain how to use the method effectively");
            grid.Columns.Add(model => model.about_possible_side_effects).Titled("About possible side effects");
            grid.Columns.Add(model => model.experience_side_effect).Titled("What to do if experience any side effect");
            grid.Columns.Add(model => model.check_bp).Titled("Check B.P");
            grid.Columns.Add(model => model.follow_up).Titled("When to come for follow up?");
            grid.Columns.Add(model => model.give_tracking_card).Titled("Give follow up tracking card");
            grid.Columns.Add(model => model.total_num_of_mwras).Titled("Total Number of Mwra's");
            grid.Columns.Add(model => model.how_many_verified_by_team).Titled("How many Mwra's verified by team (LHV & DPM) till to date");
            grid.Columns.Add(model => model.remarks1).Titled("Remarks");


            grid.Pager = new GridPager<Hands.Data.HandsDB.SpLhvCheckListReturnModel>(grid);
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