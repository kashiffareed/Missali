using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Role
{
    public interface IRoleService : IService<Data.HandsDB.Role>
    {

        List<SpGetRolesByProjectIdReturnModel> GetRollByProjectId(int projectId);
       Data.HandsDB.Role GetRole( int projectid);
    }
}

