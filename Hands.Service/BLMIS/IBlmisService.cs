using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.BLMIS
{
    public interface IBlmisService : IService<Data.HandsDB.BlmisUserstock>
    {
        List<SpBlmisInventoryReturnModel> GetBlmisInventory();

        List<SpBlmisUserStockReturnModel> GetBlmisUserStock(int? projectId, int? marviId);
    }
}