using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.RoleMenuAccess
{
   public interface IRoleMenuAccessService : IService<Data.HandsDB.RoleMenuAccess>
   {

       List<Data.HandsDB.RoleMenuAccess> getAllByRoleId(int RoleId, int projectId);

       List<Data.HandsDB.SpRoleMenuAccessReturnModel> getAllAssignMenutoRoles();
    }
}
