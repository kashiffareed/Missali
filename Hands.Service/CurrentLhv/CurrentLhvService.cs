using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;


namespace Hands.Service.CurrentLhv
{
    public class CurrentLhvService : ServiceBase<Data.HandsDB.AppUser>, ICurrentLhvService
    {
        public List<Data.HandsDB.GetCurrentUserForEachLhvByRegionIdReturnModel> CurrentLhvByRegionId(int? regionId = null)
        {
            return  _db.GetCurrentUserForEachLhvByRegionId(regionId, HandSession.Current.ProjectId).ToList();
          
        }
    }
}

