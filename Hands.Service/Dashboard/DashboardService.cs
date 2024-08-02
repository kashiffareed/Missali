using System;
using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;

namespace Hands.Service.Dashboard
{
    public class DashboardService : ServiceBase<Content>, IDashboardService
    {

        
        public List<SpNewUserMethodWiseByRegionIdReturnModel> GetNewUserMethodWiseByRegionId(DateTime startDate, DateTime endDate, int? regionId = null)
        {
            var data = _db.SpNewUserMethodWiseByRegionId(startDate, endDate, regionId, HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<GetlhvMavraCountByRegionIdReturnModel> GetlhvMavraCountByRegionId(int? regionId = null)
        {
            var data = _db.GetlhvMavraCountByRegionId(regionId,HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<Data.HandsDB.SpTestInterventionAreaReturnModel> GetInterventionArea(int? regionId = null)
        {
            var data = _db.SpTestInterventionArea(regionId,HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.SpTargetPopuplationReturnModel> GetTargetPopuplation(int? regionId = null)
        {
            var data = _db.SpTargetPopuplation(regionId,HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<Data.HandsDB.SpLiveProgressForVisitInCommunityDaysWiseReturnModel> GetVisitInCommunityDaysWise()
        {
            var data = _db.SpLiveProgressForVisitInCommunityDaysWise(HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<Data.HandsDB.SpLiveProgressForVisitInCommunityMonthsWiseReturnModel> GetVisitInCommunityMonthsWise()
        {
            var data = _db.SpLiveProgressForVisitInCommunityMonthsWise(HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<Data.HandsDB.SpLiveProgressForVisitInCommunityYearsWiseReturnModel> GetVisitInCommunityYearsWise()
        {
            var data = _db.SpLiveProgressForVisitInCommunityYearsWise(HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<Data.HandsDB.SpNewUserMethodWiseReturnModel> GetNewUserMethodWise()
        {
            var data = _db.SpNewUserMethodWise(HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.SpMwrasAgeWiseReportReturnModel> GetSpMwras(DateTime startDate, DateTime endDate, int? regionId = null)
        {
            var data = _db.SpMwrasAgeWiseReport(startDate, endDate, regionId, HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.SpTrendAmongCurrentUsersReturnModel> GetSpTrendAmongCurrentUsers(int? regionId = null)
        {
            var data = _db.SpTrendAmongCurrentUsers(regionId, HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.SpTrendAmongNewUsersReturnModel> GetSpTrendAmongNewUsers(DateTime startDate, DateTime endDate, int? regionId = null)
        {
            var data = _db.SpTrendAmongNewUsers(startDate, endDate, regionId, HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.SpDetailMcprReturnModel> SpDetailMcprReturns(DateTime startDate, DateTime endDate, int? regionId = null)
        {
            var data = _db.SpDetailMcpr(startDate, endDate, regionId, HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.SppregnantWomanReturnModel> GetSppregnantWomanReturns()
        {
            var data = _db.SppregnantWoman().ToList();
            return data;
        }
        public List<Data.HandsDB.NewUserEachLhvByRegionIdReturnModel> GetNewUserEachLhvReturns(int? regionId = null)
        {
            var data = _db.NewUserEachLhvByRegionId(regionId, HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.NewUserCurrentMonthReturnModel> GetNewUserCurrentMonths()
        {
            var data = _db.NewUserCurrentMonth(HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<Data.HandsDB.SpTestUnmetNeedInMwrasReturnModel> GetSpTestUnmetNeedInMwras()
        {
            var data = _db.SpTestUnmetNeedInMwras(HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.SpTestCurrentUserReturnModel> GetSpTestCurrentUsers()
        {
            var data = _db.SpTestCurrentUser(HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<Data.HandsDB.SpTestDetailsOfShiftedClientsReturnModel> GetSpTestDetails(DateTime startDate, DateTime endDate, int? regionId = null)
        {
            var data = _db.SpTestDetailsOfShiftedClients(startDate, endDate, regionId, HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<SpLhvDetailsReturnModel> GetDetailslhv(int? regionId = null)
        {
            var data = _db.SpLhvDetails(regionId,HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<SpFpUsersTaluqaTehsilwiseReturnModel> GetFpUser(DateTime startDate, DateTime endDate)
        {
           var data=_db.SpFpUsersTaluqaTehsilwise(startDate, endDate, HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<SpMcprTaluqaWiseReturnModel> GetMcpr()
        {

            var data = _db.SpMcprTaluqaWise(HandSession.Current.ProjectId).ToList();
            return data;
        }
        public List<SpAliveChildReturnModel> GetChild(DateTime statDate, DateTime endDate)
        {

            var data = _db.SpAliveChild(HandSession.Current.ProjectId, statDate, endDate).ToList();
            return data;
        }

        public List<Data.HandsDB.SpLiveProgressForVisitMarviInCommunityDaysWiseReturnModel> GetVisitInCommunityMarviDaysWise()
        {
           return _db.SpLiveProgressForVisitMarviInCommunityDaysWise(HandSession.Current.ProjectId).ToList();
           
        }

        public List<Data.HandsDB.SpLiveProgressForVisitMarviInCommunityMonthsWiseReturnModel> GetVisitInCommunityMarviMonthsWise()
        {
            var data = _db.SpLiveProgressForVisitMarviInCommunityMonthsWise(HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<Data.HandsDB.SpLiveProgressForVisitMarviInCommunityYearsWiseReturnModel> GetVisitInCommunityMarviYearsWise()
        {
            var data = _db.SpLiveProgressForVisitMarviInCommunityYearsWise(HandSession.Current.ProjectId).ToList();
            return data;
        }

        public List<Data.HandsDB.MarviClientGenerationReportReturnModel> GetMarviClientGenerationReport(DateTime startDate, DateTime endDate, int? regionId = null)
        {
            var data = _db.MarviClientGenerationReport(startDate, endDate,regionId, HandSession.Current.ProjectId).ToList();
            return data;
        }

    }
}

