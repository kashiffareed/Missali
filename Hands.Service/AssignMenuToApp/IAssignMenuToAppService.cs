using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.AssignMenuToApp
{
   public interface IAssignMenuToAppService : IService<Data.HandsDB.AssignMenuToApp>
    {
        List<Data.HandsDB.AssignMenuToApp> GetAllMenuByProjectId(int projectId);
        List<SpGetProjectDetailsReturnModel> GetProjectDetails();
    }
}
