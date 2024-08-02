using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel;
using Hands.Data.HandsDB;
using Hands.Service.NoorChecklist;
using Hands.Service.NrCheckList;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;

namespace Hands.Web.Controllers
{
    public class RealTimeCheckListController : ControllerBase
    {
        private readonly INoorChecklistService _noorChecklistService;
        private readonly INrCheckListService _nrCheckListService;
     


        public RealTimeCheckListController()
        {
            _noorChecklistService = new NoorChecklistService();
            _nrCheckListService =new NrCheckListService();


        }
        //public ActionResult NoorClientchecklist(int noor_checklist_id)
        //{
        //    var SchemeList = _nrCheckListService.GetNrCheckList(noor_checklist_id).OrderByDescending(x => x.noor_checklist_id).ToList();
        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("NoorClientLising", SchemeList);
        //    }
        //    return View(SchemeList);

        //}

        // GET: RealTimeCheckList
        public ActionResult MarviCheckList()
        {
            var model = new ViewModels.Models.RealTimeCheckList.NoorCheckList();
            model.NoorCheckListItems = _noorChecklistService.GetNoorCheckListReport();


            return View(model);
        }

        public ActionResult ViewMarviClient(int id)
        {

            var model = _nrCheckListService.GetNrCheckList(id);
            GetNoorClientCheckListingReturnModel ab = model.First();
            GetNoorClientCheckListingReturnModel datamodel = new GetNoorClientCheckListingReturnModel();
            return PartialView("ViewMarviClient", ab);
        }


        //public ActionResult NewMwra(int? mwraclientId)
        //{

        //    var model = new ViewModels.Models.RealTimeCheckList.NoorCheckList();
        //    model.MwraClientList = _newMwraClientListingService.MwraClientlist(mwraclientId);
        //    return View(model);


        //}

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
                    IGrid<Hands.Data.HandsDB.SpNoorCheckListReturnModel> grid = CreateExportableGrid();
                    ExcelWorksheet sheet = package.Workbook.Worksheets["Data"];

                    foreach (IGridColumn column in grid.Columns)
                    {
                        sheet.Cells[1, col].Value = column.Title;
                        sheet.Column(col++).Width = 18;
                    }

                    foreach (IGridRow<Hands.Data.HandsDB.SpNoorCheckListReturnModel> gridRow in grid.Rows)
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

        private IGrid<Hands.Data.HandsDB.SpNoorCheckListReturnModel> CreateExportableGrid()
        {
            IGrid<Hands.Data.HandsDB.SpNoorCheckListReturnModel> grid = new Grid<Hands.Data.HandsDB.SpNoorCheckListReturnModel>(_noorChecklistService.NoorCheckList()); 
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.QueryString;
            grid.Columns.Add(model => model.full_name).Titled("Marvi Name");
            grid.Columns.Add(model => model.visitor_name).Titled("Visitor Name");
            grid.Columns.Add(model => model.region_name).Titled("Distirct");
            //grid.Columns.Add(model => model.taluqa_name).Titled("Taluqa Name");
            //grid.Columns.Add(model => model.union_council_name).Titled("Union Council");
            grid.Columns.Add(model => model.created_at).Titled("Created_Date");
            grid.Columns.Add(model => model.noor_markaz_renovated).Titled("Marvi Markaz Renovated");
            grid.Columns.Add(model => model.sign_boards).Titled("Sign boards");
            grid.Columns.Add(model => model.examination_couch).Titled("Examination Couch");
            grid.Columns.Add(model => model.plastic_sheet).Titled("Plastic sheet");
            grid.Columns.Add(model => model.light).Titled("Light");
            grid.Columns.Add(model => model.privacy_assurance).Titled("Privacy Assurance");
            grid.Columns.Add(model => model.posters).Titled("Posters");
            grid.Columns.Add(model => model.sac_banner).Titled("Sac Banner");
            grid.Columns.Add(model => model.remarks1).Titled("Remarks");
            grid.Columns.Add(model => model.worker_bag_available).Titled("Worker Bag Available");
            grid.Columns.Add(model => model.phone_charger_available).Titled("Phone Charger Available");
            grid.Columns.Add(model => model.speakers_available).Titled("Speakers Available");
            grid.Columns.Add(model => model.mwra_booklet_available).Titled("MWRA Booklet Available");
            grid.Columns.Add(model => model.tracking_cards_available).Titled("Tracking cards Available");
            grid.Columns.Add(model => model.worker_bag_available).Titled("Worker Bag Functional");
            grid.Columns.Add(model => model.phone_charger_func).Titled("Phone Charger Func");
            grid.Columns.Add(model => model.speakers_func).Titled("Speakers func");
            grid.Columns.Add(model => model.mwra_booklet_func).Titled("MWRA Booklet func");
            grid.Columns.Add(model => model.tracking_cards_func).Titled("Tracking cards Func");
            grid.Columns.Add(model => model.remarks2).Titled("Remarks2");
            grid.Columns.Add(model => model.pills).Titled("Pills");
            grid.Columns.Add(model => model.condoms).Titled("Condoms");
            grid.Columns.Add(model => model.remarks3).Titled("Remarks 3");
            grid.Columns.Add(model => model.washing_soap).Titled("Washing soap");
            grid.Columns.Add(model => model.detergent).Titled("Detergent");
            grid.Columns.Add(model => model.hand_wash).Titled("Hand wash");
            grid.Columns.Add(model => model.bath_soap).Titled("Bath soap");
            grid.Columns.Add(model => model.sanitary_napkins).Titled("Sanitary Napkins");
            grid.Columns.Add(model => model.toothpaste).Titled("Tooth paste");
            grid.Columns.Add(model => model.toothpaste).Titled("Tooth brush");
            grid.Columns.Add(model => model.dettol).Titled("Dettol");
            grid.Columns.Add(model => model.slippers).Titled("Slippers");
            grid.Columns.Add(model => model.school_shoes_girls).Titled("School shoes girls");
            grid.Columns.Add(model => model.school_shoes_boys).Titled("School shoes boys");
            grid.Columns.Add(model => model.new_born_kits).Titled("New born kits");
            grid.Columns.Add(model => model.cloths).Titled("Cloths");
            grid.Columns.Add(model => model.pulses).Titled("Pulses");
            grid.Columns.Add(model => model.rice).Titled("Rice");
            grid.Columns.Add(model => model.sugar).Titled("Sugar");
            grid.Columns.Add(model => model.vegetable_edible_oil).Titled("Vegetable edible oil");
            grid.Columns.Add(model => model.eggs).Titled("Eggs");
            grid.Columns.Add(model => model.flour).Titled("Flour");
            grid.Columns.Add(model => model.lodized_salt).Titled("Lodized salt");
            grid.Columns.Add(model => model.syp_paracetamol).Titled("Syp Paracetamol");
            grid.Columns.Add(model => model.tab_paracetamol).Titled("Tab paracetamol");
            grid.Columns.Add(model => model.syp_amoxil).Titled("Syp amoxil");
            grid.Columns.Add(model => model.cap_amoxil).Titled("Cap amoxil");
            grid.Columns.Add(model => model.ors).Titled("ORS");
            grid.Columns.Add(model => model.tab_folic_acid).Titled("Tab folic acid");
            grid.Columns.Add(model => model.iron_tablets).Titled("Iron tablets");
            grid.Columns.Add(model => model.multi_vitamin).Titled("Multi vitamin");
            grid.Columns.Add(model => model.tab_metronidazole).Titled("Tab metronidazole");
            grid.Columns.Add(model => model.syp_metronidazole).Titled("Syp metronidazole");
            grid.Columns.Add(model => model.polyfax_skin).Titled("Polyfax skin");
            grid.Columns.Add(model => model.remarks4).Titled("Remarks 4");
            grid.Columns.Add(model => model.total_purchase_last_month).Titled("Total purchase last month");
            grid.Columns.Add(model => model.total_sale_last_month).Titled("Total sale last month");
            grid.Columns.Add(model => model.remarks5).Titled("Remarks 5");
            grid.Columns.Add(model => model.amount_received_last_stipend).Titled("Amount received last stipend");
            grid.Columns.Add(model => model.month_received_last_stipend).Titled("Month received last stipend");
            grid.Columns.Add(model => model.problem_receiving_stipend).Titled("Problem receiving stipend");
            grid.Columns.Add(model => model.remarks6).Titled("Remarks 6");
            grid.Columns.Add(model => model.saniplast).Titled("Saniplast");
            grid.Columns.Add(model => model.number_of_hh).Titled("Number of hh");
            grid.Columns.Add(model => model.number_of_visits_last_month).Titled("Number of visits last month");
            grid.Columns.Add(model => model.remarks7).Titled("Remarks 7");
            grid.Columns.Add(model => model.health_committee).Titled("Health committee");
            grid.Columns.Add(model => model.num_hc_members).Titled("Num hc members");
            grid.Columns.Add(model => model.monthly_meeting).Titled("Monthly meeting");
            grid.Columns.Add(model => model.remarks8).Titled("Remarks 8");
            grid.Columns.Add(model => model.fp_method_pills).Titled("FP method pills");
            grid.Columns.Add(model => model.fp_method_condoms).Titled("FP method condoms");
            grid.Columns.Add(model => model.fp_method_injectables).Titled("FP method injectables");
            grid.Columns.Add(model => model.fp_method_iucd).Titled("FP method iucd");
            grid.Columns.Add(model => model.fp_method_tl).Titled("FP method tl");
            grid.Columns.Add(model => model.fp_method_implant).Titled("FP method implant");
            grid.Columns.Add(model => model.know_appropriate_facility).Titled("Know appropriate facility");
            grid.Columns.Add(model => model.know_location_uc_hcp).Titled("Know location uc hcp");
            grid.Columns.Add(model => model.enlist_enroll_mwra).Titled("Enlist enroll MWRA");
            grid.Columns.Add(model => model.space_provide_noor_markaz).Titled("Space provide noor markaz");
            grid.Columns.Add(model => model.conduct_councel).Titled("Conduct councel");
            grid.Columns.Add(model => model.conduct_family_session).Titled("Conduct family session");
            grid.Columns.Add(model => model.enlist_potential_clients).Titled("Enlist potential clients");
            grid.Columns.Add(model => model.facilitate_lhv_arranging_clinic).Titled("Facilitate lhv arranging clinic");
            grid.Columns.Add(model => model.fp_supplies_condoms).Titled("FP supplies condoms");
            grid.Columns.Add(model => model.followup_clients_through_cards).Titled("Followup clients through cards");
            grid.Columns.Add(model => model.inform_lhv_if_side_effect).Titled("Inform lhv if side effect");
            grid.Columns.Add(model => model.reassure_till_lhv_arrives).Titled("Reassure till lhv arrives");
            grid.Columns.Add(model => model.facilitate_lhv_maintain).Titled("Facilitate lhv maintain");
            grid.Columns.Add(model => model.manage_hygiene_products).Titled("Manage hygiene products");
            grid.Columns.Add(model => model.social_market_promotions).Titled("Social market promotions");
            grid.Columns.Add(model => model.keep_sale_record).Titled("Keep sale record");
            grid.Columns.Add(model => model.with_lhv_support).Titled("With lhv support");
            grid.Columns.Add(model => model.conduct_muac_for_plw).Titled("Conduct muac for plw");
            grid.Columns.Add(model => model.conduct_muac_for_children).Titled("Conduct muac for children");
            grid.Columns.Add(model => model.with_lhv_support).Titled("Support Group Formation");
            grid.Columns.Add(model => model.remarks9).Titled("Remarks 9");
            grid.Columns.Add(model => model.know_session_on_mobile).Titled("Know session on mobile");
            grid.Columns.Add(model => model.mazbot_bandi).Titled("Mazbot bandi");
            grid.Columns.Add(model => model.sehat_khushi).Titled("Sehat khushi");
            grid.Columns.Add(model => model.sahi_waqt_sahi_faisla).Titled("Sahi waqt sahi faisla");
            grid.Columns.Add(model => model.khandani_mansoba_bandi).Titled("Khandani mansoba bandi");
            grid.Columns.Add(model => model.khandani_mansoba_kadeem).Titled("Khandani mansoba kadeem");
            grid.Columns.Add(model => model.bachon_ki_pedaish).Titled("Bachon ki pedaish");
            grid.Columns.Add(model => model.mustaqil_tariqay).Titled("Mustaqil tariqay");
            grid.Columns.Add(model => model.khandani_mansoba_bandi_tikay).Titled("Khandani mansoba bandi tikay");
            grid.Columns.Add(model => model.khandani_mansoba_bandi_implant).Titled("Khandani mansoba bandi implanty");
            grid.Columns.Add(model => model.khandani_mansoba_bandi_sawalat).Titled("Khandani mansoba bandi sawalat");
            grid.Columns.Add(model => model.remarks10).Titled("Remarks 10");
            grid.Columns.Add(model => model.visitor_name).Titled("Visitor name");
            grid.Columns.Add(model => model.date).Titled("Date");

            grid.Pager = new GridPager<Hands.Data.HandsDB.SpNoorCheckListReturnModel>(grid);
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
                        bulkInsert.DestinationTableName = "noor_checklist";
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("noor_checklist_id", "noor_checklist_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("app_user_id", "app_user_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("created_at", "created_at"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("noor_markaz_renovated", "noor_markaz_renovated"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("sign_boards", "sign_boards"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("examination_couch", "examination_couch"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("plastic_sheet", "plastic_sheet"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("light", "light"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("privacy_assurance", "privacy_assurance"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("posters", "posters"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks1", "remarks1"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("worker_bag_available", "worker_bag_available"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("phone_charger_available", "phone_charger_available"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("speakers_available", "speakers_available"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("mwra_booklet_available", "mwra_booklet_available"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("tracking_cards_available", "tracking_cards_available"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("phone_charger_func", "phone_charger_func"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("speakers_func", "speakers_func"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("mwra_booklet_func", "mwra_booklet_func"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("tracking_cards_func", "tracking_cards_func"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks2", "remarks2"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("pills", "pills"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("condoms", "condoms"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks3", "remarks3"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("washing_soap", "washing_soap"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("detergent", "detergent"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("hand_wash", "hand_wash"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("bath_soap", "bath_soap"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("sanitary_napkins", "sanitary_napkins"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("toothpaste", "toothpaste"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("tooth_brush", "tooth_brush"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("dettol", "dettol"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("slippers", "slippers"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("school_shoes_girls", "school_shoes_girls"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("school_shoes_boys", "school_shoes_boys"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("new_born_kits", "new_born_kits"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("cloths", "cloths"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("pulses", "pulses"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("rice", "rice"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("sugar", "sugar"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("vegetable_edible_oil", "vegetable_edible_oil"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("eggs", "eggs"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("flour", "flour"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("lodized_salt", "lodized_salt"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("syp_paracetamol", "syp_paracetamol"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("tab_paracetamol", "tab_paracetamol"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("syp_amoxil", "syp_amoxil"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("cap_amoxil", "cap_amoxil"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ors", "ors"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("tab_folic_acid", "tab_folic_acid"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("iron_tablets", "iron_tablets"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("multi_vitamin", "multi_vitamin"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("tab_metronidazole", "tab_metronidazole"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("syp_metronidazole", "syp_metronidazole"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("polyfax_skin", "polyfax_skin"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("saniplast", "saniplast"));         
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks4", "remarks4"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("total_purchase_last_month", "total_purchase_last_month"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("total_sale_last_month", "total_sale_last_month"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks5", "remarks5"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("amount_received_last_stipend", "amount_received_last_stipend"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("month_received_last_stipend", "month_received_last_stipend"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("problem_receiving_stipend", "problem_receiving_stipend"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks6", "remarks6"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("number_of_hh", "number_of_hh"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("number_of_visits_last_month", "number_of_visits_last_month"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks7", "remarks7"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("health_committee", "health_committee"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("num_hc_members", "num_hc_members"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("monthly_meeting", "monthly_meeting"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks8", "remarks8"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_method_pills", "fp_method_pills"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_method_condoms", "fp_method_condoms"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_method_injectables", "fp_method_injectables"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_method_iucd", "fp_method_iucd"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_method_tl", "fp_method_tl"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_method_implant", "fp_method_implant"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("know_appropriate_facility", "know_appropriate_facility"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("know_location_uc_hcp", "know_location_uc_hcp"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("enlist_enroll_mwra", "enlist_enroll_mwra"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("space_provide_noor_markaz", "space_provide_noor_markaz"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("conduct_councel", "conduct_councel"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("conduct_family_session", "conduct_family_session"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("enlist_potential_clients", "enlist_potential_clients"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("facilitate_lhv_arranging_clinic", "facilitate_lhv_arranging_clinic"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("fp_supplies_condoms", "fp_supplies_condoms"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("followup_clients_through_cards", "followup_clients_through_cards"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("inform_lhv_if_side_effect", "inform_lhv_if_side_effect"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("reassure_till_lhv_arrives", "reassure_till_lhv_arrives"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("facilitate_lhv_maintain", "facilitate_lhv_maintain"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("manage_hygiene_products", "manage_hygiene_products"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("social_market_promotions", "social_market_promotions"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("keep_sale_record", "keep_sale_record"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("with_lhv_support", "with_lhv_support"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("conduct_muac_for_plw", "conduct_muac_for_plw"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("conduct_muac_for_children", "conduct_muac_for_children"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks9", "remarks9"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("know_session_on_mobile", "know_session_on_mobile"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("mazbot_bandi", "mazbot_bandi"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("sehat_khushi", "sehat_khushi"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("sahi_waqt_sahi_faisla", "sahi_waqt_sahi_faisla"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("khandani_mansoba_bandi", "khandani_mansoba_bandi"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("khandani_mansoba_kadeem", "khandani_mansoba_kadeem"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("bachon_ki_pedaish", "bachon_ki_pedaish"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("mustaqil_tariqay", "mustaqil_tariqay"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("khawateen_iucd", "khawateen_iucd"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("khandani_mansoba_bandi_tikay", "khandani_mansoba_bandi_tikay"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("khandani_mansoba_bandi_implant", "khandani_mansoba_bandi_implant"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("khandani_mansoba_bandi_sawalat", "khandani_mansoba_bandi_sawalat"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("remarks10", "remarks10"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("lhv_id", "lhv_id"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("visitor_name", "visitor_name"));
                        bulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("date", "date"));

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