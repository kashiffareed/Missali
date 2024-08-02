using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.RoleMenuAccessLevelRights
{
   public interface IRoleMenuAccessLevelRightService : IService<Data.HandsDB.RoleMenuAccessLevelRight>
    {
        List<RoleMenuAccessLevelRight> GetListByProjectAndRoleId(int projectId, int roleId);
    }
}
