using System.Collections.Generic;

namespace Hands.Service.NewuserLhv
{
    public interface INewuserLhvService
    {
        List<Data.HandsDB.NewUserEachLhvByRegionIdReturnModel> NewUserEachLhvByRegionId(int? regionId = null);
    }
}