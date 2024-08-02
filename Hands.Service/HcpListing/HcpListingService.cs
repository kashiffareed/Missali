using System.Collections.Generic;
using Hands.Service.AppUser;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.HcpListing
{

    public class HcpListingService : ServiceBase<Data.HandsDB.AppUser>, IHcpListingService
    {
        private IAppUserService _appUserService;

        public HcpListingService()
        {
            _appUserService = new AppUserService();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAll(int projectid)
        {


            return _appUserService.GetAll("hcp", projectid);


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
             return _appUserService.GetAllActive("hcp", projectid);
        }
        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int projectid)
        {
            return _appUserService.GetAllActive(userType, projectid);
        }

        public IEnumerable<HcpListingWithNamesReturnModel> SearchLhv(int? district, int? taluqa, int? unioncouncil)
        {
            var query = GetAppHCPsWith();

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

        public IEnumerable<HcpListingWithNamesReturnModel> GetAppHCPsWith()
        {
            return _db.HcpListingWithNames("hcp",HandSession.Current.ProjectId);
        }
    }
}

