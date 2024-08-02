using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.SessionInventory
{
   public interface ISessionInventoryService : IService<Data.HandsDB.SessionInventory>
    {
        Data.HandsDB.SessionInventory GetByReturnId(int ReturnId);

    }
}
