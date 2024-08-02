using System;
using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Dashboard
{
    public interface IDashboardService 
    {
        

        List<Data.HandsDB.SpTestInterventionAreaReturnModel> GetInterventionArea(int? regionId = null);
        List<Data.HandsDB.SpTargetPopuplationReturnModel> GetTargetPopuplation(int? regionId = null);
        List<Data.HandsDB.SpLiveProgressForVisitInCommunityDaysWiseReturnModel> GetVisitInCommunityDaysWise();
        List<Data.HandsDB.SpLiveProgressForVisitInCommunityMonthsWiseReturnModel> GetVisitInCommunityMonthsWise();
        List<Data.HandsDB.SpLiveProgressForVisitInCommunityYearsWiseReturnModel> GetVisitInCommunityYearsWise();
        List<Data.HandsDB.SpNewUserMethodWiseByRegionIdReturnModel> GetNewUserMethodWiseByRegionId(DateTime startDate, DateTime endDate, int? regionId = null);
        List<Data.HandsDB.SpNewUserMethodWiseReturnModel> GetNewUserMethodWise();
        List<Data.HandsDB.SpMwrasAgeWiseReportReturnModel> GetSpMwras(DateTime startDate, DateTime endDate, int? regionId = null);
        List<Data.HandsDB.SpTrendAmongCurrentUsersReturnModel> GetSpTrendAmongCurrentUsers(int? regionId = null);
        List<Data.HandsDB.SpTrendAmongNewUsersReturnModel> GetSpTrendAmongNewUsers(DateTime startDate, DateTime endDate, int? regionId = null);
        List<Data.HandsDB.SpDetailMcprReturnModel> SpDetailMcprReturns(DateTime startDate, DateTime endDate, int? regionId = null);
        List<Data.HandsDB.SppregnantWomanReturnModel> GetSppregnantWomanReturns();
        List<Data.HandsDB.NewUserEachLhvByRegionIdReturnModel> GetNewUserEachLhvReturns(int? regionId = null);
        List<Data.HandsDB.NewUserCurrentMonthReturnModel> GetNewUserCurrentMonths();
        List<SpLhvDetailsReturnModel> GetDetailslhv(int? regionId = null);
        List<SpTestUnmetNeedInMwrasReturnModel> GetSpTestUnmetNeedInMwras();
        List<SpTestCurrentUserReturnModel> GetSpTestCurrentUsers();
        List<SpTestDetailsOfShiftedClientsReturnModel> GetSpTestDetails(DateTime startDate, DateTime endDate, int? regionId = null);
        List<SpFpUsersTaluqaTehsilwiseReturnModel> GetFpUser(DateTime startDate, DateTime endDate);
        List<SpMcprTaluqaWiseReturnModel> GetMcpr();
        List<SpAliveChildReturnModel> GetChild(DateTime startDate, DateTime endDate);
        List<Data.HandsDB.SpLiveProgressForVisitMarviInCommunityDaysWiseReturnModel> GetVisitInCommunityMarviDaysWise();
        List<Data.HandsDB.SpLiveProgressForVisitMarviInCommunityMonthsWiseReturnModel> GetVisitInCommunityMarviMonthsWise();
        List<Data.HandsDB.SpLiveProgressForVisitMarviInCommunityYearsWiseReturnModel> GetVisitInCommunityMarviYearsWise();
        List<Data.HandsDB.MarviClientGenerationReportReturnModel> GetMarviClientGenerationReport(DateTime startDate, DateTime endDate, int? regionId = null);
        List<GetlhvMavraCountByRegionIdReturnModel> GetlhvMavraCountByRegionId(int? regionId=null);

      
    }
}