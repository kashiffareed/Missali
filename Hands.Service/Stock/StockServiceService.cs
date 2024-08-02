using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.SessionInventory
{
   public class StockServiceService : ServiceBase<Data.HandsDB.Stock>, IStockService
    {
        

        Stock IStockService.GetByReturnId(int ReturnId)
        {
            return _db.Stocks.FirstOrDefault(x => x.ReturnId == ReturnId);
        }
    }
}
