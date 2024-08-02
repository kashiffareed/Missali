using System.Collections.Generic;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.Product
{
    public class ProductService : ServiceBase<Data.HandsDB.Stock>, IProductService
    {
        public List<SpClmisHandStockReturnModel> GetClmisHandStock()
        {
            return _db.SpClmisHandStock(HandSession.Current.ProjectId);
        }

        public List<SpTotalClmisHandStockReturnModel> GetTotalClmisHandStock()
        {
            return _db.SpTotalClmisHandStock(HandSession.Current.ProjectId);
        }

        public List<SpTotalClmisStocksReturnModel> ClmisHandStock( int Regionid)
        {
            return _db.SpTotalClmisStocks( HandSession.Current.ProjectId, Regionid);

        }
    }

}