using System.Collections.Generic;

namespace Hands.Service.Noor
{
    public interface INoorService : IService<Data.HandsDB.AppUser>
    {
        
        IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int projectid);

        IEnumerable<Data.HandsDB.SpGetAllMarviWithNamesReturnModel> SearchLhv(int? district, int? taluqa, int? unioncouncil);

        List<int> GetLHvById(int? lhvId, int? projectId);
        IEnumerable<Data.HandsDB.AppUser> GetAllActiveLHV();

        IEnumerable<Data.HandsDB.SpGetAllMarviWithNamesReturnModel> GetAllMarvisWithNames();
    }
}