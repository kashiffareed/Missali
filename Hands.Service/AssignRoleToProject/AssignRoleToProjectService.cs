using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.AssignRoleToProject
{
   public class AssignRoleToProjectService : ServiceBase<Data.HandsDB.AssignRoleToProject>, IAssignRoleToProjectService
    {
        public List<SpAssignRoleToProjectReturnModel> SpAssignRoleToProject()
        {
            return _db.SpAssignRoleToProject();
        }

        public List<GetRoleByProjectReturnModel> GetRoleByProject(int projectId)
        {
            return _db.GetRoleByProject(projectId);
        }

        public IEnumerable<Data.HandsDB.AssignRoleToProject> GetAllActiveData()
        {
            return _db.AssignRoleToProjects.ToList();
        }
    }
}
