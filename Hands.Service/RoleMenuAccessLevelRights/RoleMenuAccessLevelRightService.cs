using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.RoleMenuAccessLevelRights
{
    public class RoleMenuAccessLevelRightService : ServiceBase<Data.HandsDB.RoleMenuAccessLevelRight>, IRoleMenuAccessLevelRightService
    {
        public List<RoleMenuAccessLevelRight> GetListByProjectAndRoleId(int projectId, int roleId)
        {
                
            return _db.RoleMenuAccessLevelRights.Where(x => x.ProjectId == projectId && x.RoleId == roleId).ToList();
        }
    }
}
