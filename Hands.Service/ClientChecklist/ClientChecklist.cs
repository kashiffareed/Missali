using System.Collections.Generic;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.ClientChecklist
{
    public class ClientChecklistService : ServiceBase<Data.HandsDB.ClientChecklist>, IClientChecklistService
    {
        public IEnumerable<SpClientCheckListReturnModel> GetAllClientCheckList()
        {
            return _db.SpClientCheckList(HandSession.Current.ProjectId);
        }
    }
}
