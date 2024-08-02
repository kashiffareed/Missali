using System.Collections.Generic;
using Hands.Service.AppUser;

using Hands.Data.HandsDB;
using System.Linq;
using Hands.Common.Common;

namespace Hands.Service.Noor
{
    public class NoorService : ServiceBase<Data.HandsDB.AppUser>, INoorService
    {
        private IAppUserService _appUserService;

        public NoorService()
        {
            _appUserService = new AppUserService();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAll(int projectid)
        {


            return _appUserService.GetAll("marvi",projectid);


        }


        public int GetCount()
        {


            return _appUserService.GetCount("marvi");


        }

        public void Update(Data.HandsDB.AppUser existingModel)
        {
            _appUserService.Update(existingModel);
        }

        public void Insert(Data.HandsDB.AppUser appUser)
        {
            _appUserService.Insert(appUser);
        }


        public Data.HandsDB.AppUser GetById(int id)
        {
            return _appUserService.GetById(id);
        }
        public void SaveChanges()
        {
            _appUserService.SaveChanges();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(int projectid)
        {
            return _appUserService.GetAllActive("marvi",projectid);
        }
        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int projectid)
        {
            return _appUserService.GetAllActive(userType, projectid);
        }

       

        public IEnumerable<Data.HandsDB.SpGetAllMarviWithNamesReturnModel> SearchLhv(int? district, int? taluqa, int? unioncouncil)
        {
            var query = GetAllMarvisWithNames();
            if (district.HasValue)
            {
                query = query.Where(x =>
                    x.region_id == district);
            }
            if (taluqa.HasValue)
            {
                query = query.Where(x => x.taluqa_id == taluqa);
            }
            if (unioncouncil.HasValue)
            {
                query = query.Where(x => x.union_council_id == unioncouncil);
            }
            


            return query.ToList();

        }

        public List<int> GetLHvById(int? lhvId, int? projectId)
        {
            return _db.AppUsers.Where(x=>x.LhvAssigned == lhvId).Select(x=>x.AppUserId).ToList();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllActiveLHV()
        {
            return _db.AppUsers.Where(x => x.UserType== "lhv" && x.IsActive && x.ProjectId == HandSession.Current.ProjectId).ToList();
        }

        public IEnumerable<SpGetAllMarviWithNamesReturnModel> GetAllMarvisWithNames()
        {
            return _db.SpGetAllMarviWithNames(HandSession.Current.ProjectId);
        }
    }
}

