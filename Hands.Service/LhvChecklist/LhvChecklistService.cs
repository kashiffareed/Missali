using System.Collections.Generic;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.LHV;

namespace Hands.Service.LhvChecklist
{
    public class LhvChecklistService : ServiceBase<Data.HandsDB.LhvChecklist>, ILhvChecklistService
    {
        public IEnumerable<SpLhvCheckListReturnModel> GetAllLHvClCheckList()
        {
            return _db.SpLhvCheckList(HandSession.Current.ProjectId);
        }
    }
}
