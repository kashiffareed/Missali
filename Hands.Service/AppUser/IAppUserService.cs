using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.AppUser
{
    public interface IAppUserService : IService<Data.HandsDB.AppUser>
    {
        IEnumerable<Data.HandsDB.AppUser> GetAll(string userType, int? projectid);

        IEnumerable<Data.HandsDB.AppUser> GetAllAppUser();
        int GetCount(string c);
        IEnumerable<Data.HandsDB.AppUser> GetAllActive(string userType, int? projectid);

        IEnumerable<Data.HandsDB.AppUser> SearchLhv(int? district, int? taluqa, int? unioncouncil);

        Data.HandsDB.AppUser GetLoginAppUser(string username, string password);
        Data.HandsDB.AppUser GetByUSerType(int Id, int? projectid);
        IEnumerable<Data.HandsDB.SpGetMwraByLhvIdwithmwraCountReturnModel> GetMarviByLhvId(int lhvId, int? projectid);

        IEnumerable<Data.HandsDB.AppUser> GetMwrviCountByLhvId(int lhvId, int? projectid);
        IEnumerable<Data.HandsDB.AppUser> GetAllMwrvi(int unioncouncil, int? projectid);
        List<Data.HandsDB.GetAppUserByIdReturnModel> GetByAppUserId(int Id ,int? projectid);
        string GetNameById(int mwraAssignedMarviId);
    }
}

 
