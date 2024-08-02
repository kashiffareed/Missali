using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.AssignMenuToApp
{
    public class AssignMenuToAppService : ServiceBase<Data.HandsDB.AssignMenuToApp>, IAssignMenuToAppService
    {
        public List<Data.HandsDB.AssignMenuToApp> GetAllMenuByProjectId(int projectId)
        {
            return _db.AssignMenuToApps.Where(x => x.ProjectId == projectId).ToList();
        }
        public List<SpGetProjectDetailsReturnModel> GetProjectDetails()
        {
            return _db.SpGetProjectDetails();
        }
    }
}
