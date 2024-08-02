using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.SessionInventory
{
   public class SessionInventoryService : ServiceBase<Data.HandsDB.SessionInventory>, ISessionInventoryService
    {
        public Data.HandsDB.SessionInventory GetByReturnId(int ReturnId)
        {
            return _db.SessionInventories.FirstOrDefault(x => x.ReturnId == ReturnId);
        }
    }
}
