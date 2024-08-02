using System.Collections.Generic;
using System.Linq;
using Hands.Data.HandsDB;

namespace Hands.Service.Role
{
    public class RoleService : ServiceBase<Data.HandsDB.Role>, IRoleService
    {
        public List<SpGetRolesByProjectIdReturnModel> GetRollByProjectId(int projectId)
        {
            return _db.SpGetRolesByProjectId(projectId);
        }


        public Data.HandsDB.Role GetRole(int projectid)
        {
            return _db.Roles.Where(x => x.ProjectId == projectid).FirstOrDefault();
        }

     
    }
}

