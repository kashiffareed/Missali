using System.Collections.Generic;
using System.Data.Entity;
using Hands.Data.HandsDB;
using System.Linq;
using Hands.Common.Common;
using Hands.Service.AppUser;

namespace Hands.Service.Real
{
    public class RealService : ServiceBase<Data.HandsDB.AppUser>, IRealService
    {
        private IAppUserService _appUserService;

        public RealService()
        {
            _appUserService = new AppUserService();
        }
        public IEnumerable<Data.HandsDB.AppUser> GetAll(int projectid)
        {


            return _appUserService.GetAll("realtime",projectid);


        }

        public void Insert(Data.HandsDB.AppUser appUser)
        {
            _appUserService.Insert(appUser);
        }

        public Data.HandsDB.AppUser GetById(int id)
        {
            return _appUserService.GetById(id);
        }

        public void Update(Data.HandsDB.AppUser appUser)
        {
            _appUserService.Update(appUser);
        }

        public void SaveChanges()
        {
            _appUserService.SaveChanges();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(int projectid)
        {
            return _appUserService.GetAllActive("realtime", projectid);
        }
        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int projectid)
        {
            return _appUserService.GetAllActive(userType, projectid);
        }

        public IEnumerable<Data.HandsDB.AppUser> SearchLhv(int? district, int? taluqa, int? unioncouncil)
        {
            var query = _db.AppUsers
                .Where(x => x.UserType == "realtime" && x.IsActive && x.ProjectId == HandSession.Current.ProjectId);

            if (district.HasValue)
            {
                query = query.Where(x =>
                    x.RegionId == district);
            }
            if (taluqa.HasValue)
            {
                query = query.Where(x => x.TaluqaId == taluqa);
            }
            if (unioncouncil.HasValue)
            {
                query = query.Where(x => x.UnionCouncilId == unioncouncil);
            }

            return query.ToList();

        }


        public GetRealtimeChecklistReturnModel GetRealTimeCheckListDataForService(int userId, int regionId, int? projectid)
        { 
            return _db.GetRealtimeChecklist(userId, regionId, projectid); 
        }

        public GetClientlistReturnModel GetCleintList(int regionId, int? projectid)
        {
            return _db.GetClientlist( regionId, projectid);
        }
    }
}


