using System.Collections.Generic;

namespace Hands.Service.DashboardCount
{
    public interface IDashboardCountService
    {
         List<Data.HandsDB.GetDashboardcountReturnModel> GetDashboardList();
        //void Insert(Data.HandsDB.AppUser modelToSAve);
        //void Update(Data.HandsDB.AppUser existingModel);
        //Data.HandsDB.AppUser GetById(int id);
    }
}