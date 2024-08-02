using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.AspnetUsers
{
  public  class AspnetUserService : ServiceBase<Data.HandsDB.AspNetUser>, IAspnetUserService
    {
        public AspNetUser GetByEmailAspnetUser(string mail)
        {
            return _db.AspNetUsers.FirstOrDefault(x => x.Email == mail);
        }
    }
}
