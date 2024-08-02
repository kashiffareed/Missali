using Hands.Data.HandsDB;
using System.Collections.Generic;

namespace Hands.Service.Miscellenou
{
    public interface IMiscellenouService : IService<Data.HandsDB.InventoryReturn>
    {
        IEnumerable<Data.HandsDB.InventoryReturn> GetActiveData();
        List<GetAllMiscellenousReturnModel> GetAllMiscellenous();

        List<GetUsersByUserTypeReturnModel> GetUsersByUserType(string UserType,int? projectId);

        List<GetProductByUserIdReturnModel> GetProductByUserId(int userId, int? regionId, int? projectId);

        IEnumerable<GetProductByUserIdReturnModel> GetQuantityByUserIdAndProductId(int userId, int? regionId, int? projectId);


        List<SpInventoryReturnReturnModel> GetAllInventoryReturn(int? projectId);

    }
}

