using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Service.DashboardCount;

namespace Hands.Service.Dashboard
{
    public class DashboardCountService : ServiceBase<Data.HandsDB.AppUser>, IDashboardCountService
    {
       
        
      

        public List<Data.HandsDB.GetDashboardcountReturnModel> GetDashboardList()
        {
            return  _db.GetDashboardcount(HandSession.Current.ProjectId).ToList();
          
        }

       


        

       
        
    }
}

