using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;


namespace Hands.Service.MwraLhvwiseCount
{
    public class MwraLhvwiseCountService : ServiceBase<Data.HandsDB.AppUser>, IMwraLhvwiseCountService
    {

        public List<Data.HandsDB.MwraForEachLhvByRegionIdReturnModel> MwraForEachLhvByRegionId(int? regionId = null)
        {
            return _db.MwraForEachLhvByRegionId(regionId,HandSession.Current.ProjectId).ToList();

        }



    }
}

