using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Taluqa
{
    public interface ITaluqaService: IService<Data.HandsDB.Taluqa>
    {
        IEnumerable<Data.HandsDB.Taluqa> GetAll(int regionId);

        List<TaluqaListingDataReturnModel> GetTaluqaListing();
        string GetNameById(int? appUserRegionId);
    }
}
