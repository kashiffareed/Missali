using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Product;

namespace Hands.Service.CLMIS
{
    public class ClmisService : ServiceBase<UsersStock>, IClmisService
    {
        public ClmisService() : base()
        {

        }
        public IEnumerable<UsersStock> GetAllActiveDAta()
        {
            return null;
        }

        public List<SpStockListReturnModel> GetStock()
        {
            return _db.SpStockList().ToList();

        }

        public IEnumerable<SpStockListReturnModel> GetStocks(int? ProductId, int? UserId, int? projectId, out int totalRecords, int pageNo = 1, int pageSize = int.MaxValue)
        {
            var query = _db.SpStockList().Select(m => m);
            if (ProductId.HasValue)
            {
              
                query = query.Where(x => x.product_id == ProductId);
            }

            if (UserId.HasValue)
            {
                query = query.Where(x => x.user_id == UserId);
            }
            totalRecords = query.ToList().Count;
            query = query.Where(x =>x.ProjectId== projectId && x.IsActive);
            return query.Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
        }

        public List<SpClmisInventoryReturnModel> GetClmisInventory()
        {
            return _db.SpClmisInventory(HandSession.Current.ProjectId);
        }
        public List<ClmisInventoryStatusReturnModel> ClmisInventoryStatus()
        {
           return _db.ClmisInventoryStatus(HandSession.Current.ProjectId);
        }
        public List<ClmisTotalMwraReturnModel> ClmisInventoryLog()
        {
            return _db.ClmisTotalMwra(HandSession.Current.ProjectId);
        }

        public int? GetStockQuantity(int ProductId)
        {
            return _db.GetStockQuantity(ProductId).Select(x=>x.Quantity).FirstOrDefault();
        }
    }

     
    }

