using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Regions
{
   public interface IRegionService : IService<Region>
    {
        string GetNameById(int? appUserRegionId);

        List<Data.HandsDB.Region> GetRegionList();

    }
}
