using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Menu
{
    public class MenuService : ServiceBase<Data.HandsDB.Menu>,IMenuService
    {
        public List<AccessMenubyRoleIdReturnModel> GetMenuByRoleId(int RoleId , int projectId)
        {
            return _db.AccessMenubyRoleId(RoleId , projectId);
        }
    }
}

