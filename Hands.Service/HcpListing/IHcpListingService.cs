using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.HcpListing
{
    public interface IHcpListingService:IService<Data.HandsDB.AppUser>
    {
        IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int projectid);
        IEnumerable<HcpListingWithNamesReturnModel> SearchLhv(int? district, int? taluqa, int? unioncouncil);

        IEnumerable<HcpListingWithNamesReturnModel> GetAppHCPsWith();

    }
}
