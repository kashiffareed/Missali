using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.AssignMenuToProject
{
   public interface IAssignMenuToProjectService : IService<Data.HandsDB.AssignMenuToProject>
    {
        List<Data.HandsDB.AssignMenuToProject> GetAllMenuByProjectId(int projectId);
        List<SpGetProjectDetailsReturnModel> GetProjectDetails();
    }
}
