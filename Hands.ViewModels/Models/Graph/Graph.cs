using Hands.Data.HandsDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.Graph
{
    public  class Graph
    {
        public IEnumerable<Region> Regions { get; set; }
        public List<GetDashboardcountReturnModel> Dashboardlist { get; set; }
        public List<GetBlmisSalesByMonthReturnModel> BlmisSalesList { get; set; }
        public List<GetlhvMavraCountByRegionIdReturnModel> LhvListNames { get; set; }
        //public List<GetlhvMavraCountByRegionIdReturnModel> LhvListNamesBadin { get; set; }
        //public List<GetlhvMavraCountByRegionIdReturnModel> LhvListNamesShujawal { get; set; }
        //public List<GetlhvMavraCountByRegionIdReturnModel> LhvListNamesThatta { get; set; }

       

        public List<MwraForEachLhvByRegionIdReturnModel> MwraLhvNameslist { get; set; }
        //public List<MwraForEachLhvByRegionIdReturnModel> MwraLhvNameslistBadin { get; set; }
        //public List<MwraForEachLhvByRegionIdReturnModel> MwraLhvNameslistShujawal { get; set; }
        //public List<MwraForEachLhvByRegionIdReturnModel> MwraLhvNameslistThatta { get; set; }

        public List<GetCurrentUserForEachLhvByRegionIdReturnModel> CurrentLhvlist { get; set; }
        //public List<GetCurrentUserForEachLhvByRegionIdReturnModel> CurrentLhvlistBadin { get; set; }
        //public List<GetCurrentUserForEachLhvByRegionIdReturnModel> CurrentLhvlistShujawal { get; set; }
        //public List<GetCurrentUserForEachLhvByRegionIdReturnModel> CurrentLhvlistThatta { get; set; }

        public List<NewUserEachLhvByRegionIdReturnModel> NewUserLhvlist { get; set; }
        public List<NewUserEachLhvByRegionIdReturnModel> NewUserLhvlistBadin { get; set; }
        public List<NewUserEachLhvByRegionIdReturnModel> NewUserLhvlistShujawal { get; set; }
        public List<NewUserEachLhvByRegionIdReturnModel> NewUserLhvlistThatta { get; set; }


        public List<NewUserCurrentMonthReturnModel> newUserCurrentMonths { get; set; }
        public List<NewUserCurrentMonthByRegionIdReturnModel> NewUserCurrentMonthsByAllRegions { get; set; }
        public List<SpNewUserMethodWiseByRegionIdReturnModel> SpNewUserMethodWiseByRegionId { get; set; }

        public List<SpTestCurrentUserReturnModel> spTestCurrentUsers { get; set; }
        public List<SpTestDetailsOfShiftedClientsReturnModel> spTestDetailsOfShiftedClients { get; set; }
        public List<SpTestUnmetNeedInMwrasReturnModel> spTestUnmetNeedInMwras { get; set; }
        public List<SpCurrentUserMethodWiseReturnModel> CurrentUserMethodWiselist { get; set; }    
        public List<SpTestInterventionAreaReturnModel> InterventionAreaList { get; set; }
        public List<SppregnantWomanReturnModel> SppregnantWomanReturns { get; set; }
        public List<NewUserEachLhvByRegionIdReturnModel> newUserEachLhvReturns { get; set; }

        public List<SpTargetPopuplationReturnModel> TargetPopuplationList { get; set; }
        public List<SpNewUserMethodWiseReturnModel> NewUserMethodWiseList { get; set; }
        public List<SpMwrasAgeWiseReportReturnModel> AgeWiseReportList { get; set; }
        public List<SpTrendAmongCurrentUsersReturnModel> SpTrendAmongCurrentUsers { get; set; }
        public List<SpTrendAmongNewUsersReturnModel> SpTrendAmongNewUser { get; set; }
        public List<SpDetailMcprReturnModel> spDetailMcprReturns { get; set; }
        public List<SpLhvDetailsReturnModel> LhvDetailsList { get; set; }
        public List<SpFpUsersTaluqaTehsilwiseReturnModel> FpUsersList { get; set; }
        public List<SpMcprTaluqaWiseReturnModel> McprList { get; set; }
        public List<SpAliveChildReturnModel> ALiveChildList { get; set; }
        public List<SpLiveProgressForVisitInCommunityDaysWiseReturnModel> liveProgressForVisitInCommunityDaysWiseList { get; set; }
        public List<SpLiveProgressForVisitInCommunityMonthsWiseReturnModel> liveProgressForVisitInCommunityMonthsWiseList { get; set; }
        public List<SpLiveProgressForVisitInCommunityYearsWiseReturnModel> liveProgressForVisitInCommunityYearsWiseList { get; set; }
        public System.Int32? NewUser { get; set; }
        public System.String product_name { get; set; }

        public List<SpLiveProgressForVisitMarviInCommunityDaysWiseReturnModel> liveProgressForVisitMarviInCommunityDaysWiseList { get; set; }
        public List<SpLiveProgressForVisitMarviInCommunityMonthsWiseReturnModel> liveProgressForVisitMarviInCommunityMonthsWiseList { get; set; }
        public List<SpLiveProgressForVisitMarviInCommunityYearsWiseReturnModel> liveProgressForVisitMarviInCommunityYearsWiseList { get; set; }

        public List<MarviClientGenerationReportReturnModel> MarviClientGenerationReportList { get; set; }

        public string BibDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int RegionId { get; set; }
    }

    public class Items
    {


        public Nullable<int> itemcount { get; set; }
        public String itemname { get; set; }
        public int Total_Mwra { get; set; }
        public String full_name { get; set; }
        public System.Int32? Total_Current_User { get; set; }
        public System.String LhvName { get; set; }
        public System.Int32? Total_New_User { get; set; }
        public System.String newUser { get; set; }
        public System.Int32? CurrentMonthUser { get; set; }
        public System.String Users { get; set; }
        public System.Decimal? Percentage { get; set; }
        public System.String product_name { get; set; }
        public System.Int32 taluqa_id { get; set; }
        public System.String taluqa_name { get; set; }
        public System.Int32? mwraCount { get; set; }
        public System.Int32? populcation_covered { get; set; }
        public System.Int32? no_of_house_holds { get; set; }
        public System.String LHVName { get; set; }
        public System.Int32? MarviCount { get; set; }
        public System.Int32? mwraDetail { get; set; }

        public System.String taluqa { get; set; }
        public System.Int32? NOOR { get; set; }
        public System.Int32? CurrentUser { get; set; }
        public System.Int32? NewUser { get; set; }
        public System.Int32? NewUserCurrentMonth { get; set; }
        public System.String taluqaMcp { get; set; }
        public System.Int32 taluqaMcpr { get; set; }
        public System.Int32? MCPR { get; set; }
        public System.Int32? no_of_alive_children { get; set; }
        public System.Int32? AliveChildCount { get; set; }


    }
}
