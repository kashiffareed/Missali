using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.UserMenuAccess
{
   public class UserMenuAccessService : ServiceBase<Data.HandsDB.UserMenuAccess>, IUserMenuAccessService
    {
        public List<Data.HandsDB.UserMenuAccess> getAllByUserId(string UserId)
        {
            return _db.UserMenuAccesses.Where(x => x.UserId == UserId).ToList();
        }
    }
}
