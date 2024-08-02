using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.RoleMenuAccess
{
   public class RoleMenuAccessService : ServiceBase<Data.HandsDB.RoleMenuAccess>, IRoleMenuAccessService
    {
        public List<Data.HandsDB.RoleMenuAccess> getAllByRoleId(int RoleId , int projectId)
        {
            return _db.RoleMenuAccesses.Where(x => x.RoleId == RoleId && x.ProjectId == projectId).ToList();
        }

        public List<SpRoleMenuAccessReturnModel> getAllAssignMenutoRoles()
        {
            return _db.SpRoleMenuAccess();
        }
    }
}
