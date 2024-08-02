using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.ProjectMenuAccess
{
   public class ProjectMenuAccessService : ServiceBase<Data.HandsDB.ProjectMenuAccess>, IProjectMenuAccessService
    {
        public List<Data.HandsDB.ProjectMenuAccess> getAllByProjectId(int ProjectId)
        {
            return _db.ProjectMenuAccesses.Where(x => x.ProjectId == ProjectId).ToList(); 
        }

    }
}
