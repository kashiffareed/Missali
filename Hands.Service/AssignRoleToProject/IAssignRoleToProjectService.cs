using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.AssignRoleToProject
{
   public interface IAssignRoleToProjectService : IService<Data.HandsDB.AssignRoleToProject>
    {
        List<SpAssignRoleToProjectReturnModel> SpAssignRoleToProject();

        List<GetRoleByProjectReturnModel> GetRoleByProject(int projectId);

        IEnumerable<Data.HandsDB.AssignRoleToProject> GetAllActiveData();
    }
}
