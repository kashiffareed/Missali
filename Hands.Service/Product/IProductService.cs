using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Product
{
    public interface IProductService : IService<Data.HandsDB.Stock>
    {

         List<SpClmisHandStockReturnModel> GetClmisHandStock();

        List<SpTotalClmisHandStockReturnModel> GetTotalClmisHandStock();
        List<SpTotalClmisStocksReturnModel> ClmisHandStock(int Regionid);


    }
}