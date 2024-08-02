using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.ShopKeeperLocation
{
    public class ShopKeeperLocationService : ServiceBase<Hands.Data.HandsDB.AppUser>, IShopKeeperLocationService
    {
        public IEnumerable<Data.HandsDB.AppUser> GetAllShopkeeperLocations()
        {
            return _db.AppUsers.Where(au => au.UserType == "shopkeeper" && au.IsActive && au.ProjectId == HandSession.Current.ProjectId).ToList();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllLhvLocations()
        {
            return _db.AppUsers.Where(au => au.UserType == "lhv" && au.IsActive && au.ProjectId == HandSession.Current.ProjectId).ToList();
        }

        public IEnumerable<Data.HandsDB.AppUser> GetAllMarviLocations()
        {
            return _db.AppUsers.Where(au => au.UserType == "marvi" && au.IsActive && au.ProjectId == HandSession.Current.ProjectId).ToList();
        }
        public IEnumerable<Data.HandsDB.AppUser> GetAllHcpLocations()
        {
            return _db.AppUsers.Where(au => au.UserType == "hcp" && au.IsActive && au.ProjectId == HandSession.Current.ProjectId).ToList();
        }
    }
}

