using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.AppMenu
{
    public interface IAppMenuService : IService<Data.HandsDB.AppMenu>
    {
        //List<AccessMenubyRoleIdReturnModel> GetMenuByRoleId(int RoleId , int projectId);
    }
}