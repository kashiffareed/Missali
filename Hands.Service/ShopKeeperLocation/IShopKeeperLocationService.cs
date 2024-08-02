using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.ShopKeeperLocation
{
    public interface IShopKeeperLocationService : IService<Hands.Data.HandsDB.AppUser>
    {
         IEnumerable<Data.HandsDB.AppUser> GetAllShopkeeperLocations();
        IEnumerable<Data.HandsDB.AppUser> GetAllLhvLocations();
        IEnumerable<Data.HandsDB.AppUser> GetAllMarviLocations();
        IEnumerable<Data.HandsDB.AppUser> GetAllHcpLocations();


    }
}