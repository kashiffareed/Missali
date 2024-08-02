using System.Collections.Generic;

namespace Hands.Service.NewCurrentMonth
{
    public interface INewCurrentMonthLhvService
    {
        List<Data.HandsDB.NewUserCurrentMonthReturnModel> GetCurrentmonthUserLhvlist();

        List<Data.HandsDB.NewUserCurrentMonthByRegionIdReturnModel> NewUserCurrentMonthByRegionId(int? regionId = null);
    }
}