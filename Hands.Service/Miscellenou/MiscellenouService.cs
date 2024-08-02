using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Product;

namespace Hands.Service.Miscellenou
{
    public class MiscellenouService : ServiceBase<Data.HandsDB.InventoryReturn>, IMiscellenouService
    {
        public IEnumerable<Data.HandsDB.InventoryReturn> GetActiveData()
        {
            return null;
        }

        public List<GetAllMiscellenousReturnModel> GetAllMiscellenous()
        {
            return _db.GetAllMiscellenous().ToList();

        }

        public List<GetUsersByUserTypeReturnModel> GetUsersByUserType(string UserType,int? projectId)
        {
            return _db.GetUsersByUserType(UserType, projectId).ToList();

        }

        public List<GetProductByUserIdReturnModel> GetProductByUserId(int userId, int? regionId, int? projectId)
        {
            return _db.GetProductByUserId(userId, projectId, regionId).ToList();

        }

        public IEnumerable<GetProductByUserIdReturnModel> GetQuantityByUserIdAndProductId(int userId, int? regionId, int? projectId)
        {
            return _db.GetProductByUserId(userId, projectId, regionId);

        }

        public List<SpInventoryReturnReturnModel> GetAllInventoryReturn(int? projectId)
        {
            return _db.SpInventoryReturn(projectId);
        }
    }
}

