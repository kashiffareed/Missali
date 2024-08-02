using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.Asp.netRoles
{
   public class AspNetRolesService : ServiceBase<Data.HandsDB.AspNetRole>, IAspNetRolesService
    {
        public AspNetRole GetRoleByName(string name)
        {
            return _db.AspNetRoles.First(x => x.Name == name);
        }
    }
}
