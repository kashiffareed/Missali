using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;


namespace Hands.Service.AppUser
{
    public class AppUserService : ServiceBase<Data.HandsDB.AppUser>, IAppUserService
    {

        public AppUserService() : base()
        {

        }
        public IEnumerable<Data.HandsDB.AppUser> GetAll(string userType, int? projectid)
        {

            return _db.AppUsers.Where(au => au.UserType == userType && au.IsActive && au.ProjectId == projectid).ToList();


        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllAppUser()
        {

            return _db.AppUsers.Where(au => au.IsActive).ToList();


        }

        public int GetCount(string userType)
        {

            return _db.AppUsers.Count(au => au.UserType == userType && au.IsActive);

        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int? projectid)
        {
            var query = _db.AppUsers.Select(a => a);

            if (!string.IsNullOrEmpty(userType)) query = query.Where(a => a.UserType == userType && a.ProjectId== projectid);   

            query = query.Where(a => a.IsActive);

            return query.ToList();

        }


        public IEnumerable<Data.HandsDB.AppUser> SearchLhv(int? district, int? taluqa, int? unioncouncil)
        {
            var query = _db.AppUsers
                .Where(x => x.UserType == "lhv" && x.IsActive && x.ProjectId == HandSession.Current.ProjectId);

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
        
        public Data.HandsDB.AppUser GetLoginAppUser(string username, string password)
        {
            return _db.AppUsers.FirstOrDefault(x => x.Username == username && x.PlainPassword == password && x.IsActive);
        }

        public Data.HandsDB.AppUser GetByUSerType(int Id ,int? projectid)
        {
            return _db.AppUsers.FirstOrDefault(x => x.AppUserId == Id && x.ProjectId == projectid && x.IsActive);
        }

        public IEnumerable<Data.HandsDB.SpGetMwraByLhvIdwithmwraCountReturnModel> GetMarviByLhvId(int lhvId, int? projectid)
        {
            return _db.SpGetMwraByLhvIdwithmwraCount(lhvId, projectid).ToList();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetMwrviCountByLhvId(int lhvId, int? projectid)
        {
            return _db.AppUsers.Where(x=>x.LhvAssigned == lhvId && x.ProjectId == projectid && x.UserType =="marvi").ToList();
        }

        


        public IEnumerable<Data.HandsDB.AppUser> GetAllMwrvi(int unioncouncil, int? projectid)
        {
            return _db.AppUsers.Where(uc => uc.UnionCouncilId == unioncouncil && uc.ProjectId == HandSession.Current.ProjectId).ToList();
        }

        public List<GetAppUserByIdReturnModel> GetByAppUserId(int Id,int? projectid)
        {
            return _db.GetAppUserById(Id, projectid).ToList();
        }

        public string GetNameById(int id)
        {
            return _db.AppUsers.Where(x => x.AppUserId == id).Select(y => y.FullName).FirstOrDefault();
        }
    }
}

