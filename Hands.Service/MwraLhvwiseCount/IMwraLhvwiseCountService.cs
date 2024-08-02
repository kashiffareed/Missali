using System.Collections.Generic;

namespace Hands.Service.MwraLhvwiseCount
{
    public interface IMwraLhvwiseCountService
    {
         List<Data.HandsDB.MwraForEachLhvByRegionIdReturnModel> MwraForEachLhvByRegionId(int? regionId = null);
    }
}