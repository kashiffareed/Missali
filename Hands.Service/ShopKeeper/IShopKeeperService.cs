using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.ShopKeeper
{
    public interface IShopKeeperService : IService<Data.HandsDB.AppUser>
    {
        IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int projectid);
        IEnumerable<Data.HandsDB.HcpListingWithNamesReturnModel> SearchLhv(int? district, int? taluqa, int? unioncouncil);
        IEnumerable<HcpListingWithNamesReturnModel> GetAppshopkeeperWith();
    }
}