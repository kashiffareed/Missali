using System.Collections.Generic;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.NoorChecklist
{
    public class NoorChecklistService :ServiceBase<Data.HandsDB.NoorChecklist>,INoorChecklistService
    {
        public List<NoorCheckListReturnModel> GetNoorCheckListReport()
        {
            return  _db.NoorCheckList(HandSession.Current.ProjectId);
        }
    
        public IEnumerable<SpNoorCheckListReturnModel> NoorCheckList()
        {
            return _db.SpNoorCheckList();
        }
    }
}
