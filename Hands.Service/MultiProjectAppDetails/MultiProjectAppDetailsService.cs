using System.Collections.Generic;
using System.Linq;
using Hands.Data.HandsDB;
namespace Hands.Service.MultiProjectAppDetails
{
    public class MultiProjectAppDetailsService : ServiceBase<MultiProjectAppDetail>, IMultiProjectAppDetailsService
    {

        public List<Data.HandsDB.MultiProjectAppDetail> GetAllMenuDetailByProjectId(int projectId)
        {
            return _db.MultiProjectAppDetails.Where(x => x.ProjectId == projectId).ToList();
        }
        public List<SpMultiProjectAppDetailsReturnModel> GetMultiProjectAppDetails(int? projectId)
        {
            return _db.SpMultiProjectAppDetails(projectId).ToList();
        }
    }
}

