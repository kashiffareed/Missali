using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.ProjectMenuAccess
{
    public interface IProjectMenuAccessService : IService<Data.HandsDB.ProjectMenuAccess>
    {
        List<Data.HandsDB.ProjectMenuAccess> getAllByProjectId(int projectId);
    }
}
