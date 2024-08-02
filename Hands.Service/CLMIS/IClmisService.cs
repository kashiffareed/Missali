using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.CLMIS
{
    public interface IClmisService : IService<Data.HandsDB.UsersStock>
    {

        IEnumerable<Data.HandsDB.UsersStock> GetAllActiveDAta();
        List<SpStockListReturnModel> GetStock();

        IEnumerable<Data.HandsDB.SpStockListReturnModel> GetStocks(int? ProductId, int? UserId,int? projectId ,out int totalRecords,
            int pageNo = 1, int pageSize = 10);

        List<ClmisInventoryStatusReturnModel> ClmisInventoryStatus();

        List<ClmisTotalMwraReturnModel> ClmisInventoryLog();

        int? GetStockQuantity(int ProductId);

        List<SpClmisInventoryReturnModel> GetClmisInventory();
    }
}