using System.Collections.Generic;

namespace Hands.Service.CurrentLhv
{
    public interface ICurrentLhvService
    {
        List<Data.HandsDB.GetCurrentUserForEachLhvByRegionIdReturnModel>CurrentLhvByRegionId(int? regionId = null);

    }
}