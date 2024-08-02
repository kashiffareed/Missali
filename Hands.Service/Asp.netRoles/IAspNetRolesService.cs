using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.Asp.netRoles
{
   public interface IAspNetRolesService : IService<Data.HandsDB.AspNetRole>
   {
       AspNetRole GetRoleByName(string name);
   }
}
