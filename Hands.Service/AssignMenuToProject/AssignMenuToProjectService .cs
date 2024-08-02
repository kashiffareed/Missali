using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.AssignMenuToProject
{
    public class AssignMenuToProjectService : ServiceBase<Data.HandsDB.AssignMenuToProject>, IAssignMenuToProjectService
    {
        public List<Data.HandsDB.AssignMenuToProject> GetAllMenuByProjectId(int projectId)
        {
            return _db.AssignMenuToProjects.Where(x => x.ProjectId == projectId).ToList();
        }
        public List<SpGetProjectDetailsReturnModel> GetProjectDetails()
        {
            return _db.SpGetProjectDetails();
        }
    }
}
