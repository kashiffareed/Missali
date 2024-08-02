using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Service.NewCurrentMonth;


namespace Hands.Service.NewCurrentMonth
{
    public class NewCurrentMonthLhvService : ServiceBase<Data.HandsDB.AppUser>, INewCurrentMonthLhvService
    {
        public List<Data.HandsDB.NewUserCurrentMonthByRegionIdReturnModel> NewUserCurrentMonthByRegionId(int? regionId = null)
        {
            return _db.NewUserCurrentMonthByRegionId(regionId, HandSession.Current.ProjectId).ToList();
        }

        public List<Data.HandsDB.NewUserCurrentMonthReturnModel> GetCurrentmonthUserLhvlist()
        {
            return _db.NewUserCurrentMonth(HandSession.Current.ProjectId).ToList();

        }
    }
}

