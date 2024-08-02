using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hands.Service.ClientChecklist;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class ClientCheckListController : ControllerBase
    {
        // GET: ClientCheckList

        private readonly IClientChecklistService _clientChecklistService;

        public ClientCheckListController()
        {
            _clientChecklistService = new ClientChecklistService();
        }
        public ActionResult Index()
        {
            var schemeList = _clientChecklistService.GetAllClientCheckList();
            if (Request.IsAjaxRequest())
            {
                return PartialView(schemeList);
            }

            return View(schemeList);
        }

        public ActionResult View(int id)
        {
            Data.HandsDB.ClientChecklist model = _clientChecklistService.GetById(id);
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
                    IGrid<Data.HandsDB.SpClientCheckListReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpClientCheckListReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.SpClientCheckListReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpClientCheckListReturnModel> grid = new Grid<Hands.Data.HandsDB.SpClientCheckListReturnModel>(_clientChecklistService.GetAllClientCheckList());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.full_name).Titled("Mwra Client Name");
            grid.Columns.Add(model => model.visitor_name).Titled("Visitor Name");
            grid.Columns.Add(model => model.region_name).Titled("Distirct");
            grid.Columns.Add(model => model.taluqa_name).Titled("Taluqa Name");
            grid.Columns.Add(model => model.union_council_name).Titled("Union Council");
            grid.Columns.Add(model => model.created_at).Titled("Created_Date");
            grid.Columns.Add(model => model.get_information).Titled("Get information and/or counseling about a contraceptive method");
            grid.Columns.Add(model => model.receive_prescribed).Titled("Receive, get prescribed or referred for a contraceptive method for the first time or for the first time at this site");
            grid.Columns.Add(model => model.restart_contraceptive_method).Titled("Restart contraceptive method use (after not using for 6 months or more");
            grid.Columns.Add(model => model.get_supplier).Titled("Get supplies for method already using or have a routine follow-up visit for method already using");
            grid.Columns.Add(model => model.switch_contraceptive_method).Titled("Switch contraceptive methods or restart a different method (after not using for less than 6 months");
            grid.Columns.Add(model => model.discuss_a_problem).Titled("Discuss a problem (side effect/ complications) about contraceptive method that you are currently using");
            grid.Columns.Add(model => model.other_non_family).Titled("Other, non-family planning");
            grid.Columns.Add(model => model.received_method_of_choice).Titled("Did you receive any method today? Name of the method");
            grid.Columns.Add(model => model.drop_down_methods_name).Titled("Did the provider offer options of FP methods before the decision of current method?");
            grid.Columns.Add(model => model.received_method_of_choice).Titled("Did you receive your method of choice?");
            grid.Columns.Add(model => model.for_the_method_you_just_accept).Titled("For the method you just decided to accept, did the provider: Explain to you how to use the method effectively?");
            grid.Columns.Add(model => model.provider_describe_side_effects).Titled("Did the provider describe possible side effects of your adopted method?");
            grid.Columns.Add(model => model.name_side_effects).Titled("If yes, name few side effects?");
            grid.Columns.Add(model => model.what_to_do_if_problem).Titled("Tell you what to do if you have any problem?");
            grid.Columns.Add(model => model.when_folloup_visit).Titled("Were you told when to return for follow up visit?");
            grid.Columns.Add(model => model.feel_comfortable_to_ask_questions).Titled("Did you feel comfortable to ask questions during the session?");
            grid.Columns.Add(model => model.did_you_feel_information).Titled("Do you feel the information given to you during your visit today was too little, too much, or just about right?");
            grid.Columns.Add(model => model.did_you_have_enough_privacy).Titled("Did you have enough privacy during your exam?");
            grid.Columns.Add(model => model.during_visit_treated).Titled("During your visit to the clinic how were you treated by the provider?");
            grid.Columns.Add(model => model.provider_encourage).Titled("Do the provider encourage you to ask any question about your health and FP methods?");
            grid.Columns.Add(model => model.fp_need_spacing).Titled("Was your FP need, Spacing or for limiting");
            grid.Columns.Add(model => model.adopting_fp_method).Titled("After adopting FP method are you satisfied?");



            grid.Pager = new GridPager<Hands.Data.HandsDB.SpClientCheckListReturnModel>(grid);
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