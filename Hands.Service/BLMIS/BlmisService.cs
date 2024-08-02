using System.Collections.Generic;
using System.Data.Entity;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Product;

namespace Hands.Service.BLMIS
{
    
    public class BlmisService : ServiceBase<Data.HandsDB.BlmisUserstock>, IBlmisService
    {
        public List<SpBlmisInventoryReturnModel> GetBlmisInventory()
        {
            return _db.SpBlmisInventory(HandSession.Current.ProjectId);
        }

        public List<SpBlmisUserStockReturnModel> GetBlmisUserStock(int? projectId,int? marviId)
        {
            return _db.SpBlmisUserStock(projectId, marviId);
        }
    }

 }

