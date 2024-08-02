using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;

namespace Hands.Service.ShopKeeper
{
    public class ShopKeeperService : ServiceBase<Data.HandsDB.AppUser>, IShopKeeperService
    {
       private IAppUserService _appUserService;
        public ShopKeeperService()
        {
         
            _appUserService = new AppUserService();
        }
      

        public IEnumerable<Data.HandsDB.AppUser> GetAll(int projectid)
        {
            

            return _appUserService.GetAll("shopkeeper",projectid);

        }


        public Data.HandsDB.AppUser GetById(int appUserId)
        {
            
           return  _appUserService.GetById(appUserId);
        }

        public void Update(Data.HandsDB.AppUser existingModel)
        {
            _appUserService.Update(existingModel);
        }

        public void Insert(Data.HandsDB.AppUser appUser)
        {
            _appUserService.Insert(appUser);
        }

        public void SaveChanges()
        {
            _appUserService.SaveChanges();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(int projectid)
        {
            return _appUserService.GetAllActive("shopkeeper", projectid);
        }
        public IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int projectid)
        {
            return _appUserService.GetAllActive(userType, projectid);
        }


        public IEnumerable<Data.HandsDB.HcpListingWithNamesReturnModel> SearchLhv(int? district, int? taluqa, int? unioncouncil)
        {
            var query = GetAppshopkeeperWith();

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
        public IEnumerable<HcpListingWithNamesReturnModel> GetAppshopkeeperWith()
        {
            return _db.HcpListingWithNames("shopkeeper", HandSession.Current.ProjectId);
        }
    }
}

