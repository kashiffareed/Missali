using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.SessionInventory
{
   public interface IStockService : IService<Data.HandsDB.Stock>
    {
        Data.HandsDB.Stock GetByReturnId(int ReturnId);

    }
}
