using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Menu
{
    public interface IMenuService : IService<Data.HandsDB.Menu>
    {

        List<AccessMenubyRoleIdReturnModel> GetMenuByRoleId(int RoleId , int projectId);
    }
}