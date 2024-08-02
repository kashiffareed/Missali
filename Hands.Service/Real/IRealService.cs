using System.Collections.Generic;
using Hands.Data.HandsDB;
using Hands.Service.Product;

namespace Hands.Service.Real
{
    public interface IRealService : IService<Data.HandsDB.AppUser>
    {
        IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int projectid);
        IEnumerable<Data.HandsDB.AppUser> SearchLhv(int? district, int? taluqa, int? unioncouncil);
        GetRealtimeChecklistReturnModel GetRealTimeCheckListDataForService(int userId, int regionId, int? projectid);
        GetClientlistReturnModel GetCleintList(int regionId, int? projectid);
    }
}