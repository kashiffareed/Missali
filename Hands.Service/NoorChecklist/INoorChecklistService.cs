using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.NoorChecklist
{
    public interface INoorChecklistService: IService<Data.HandsDB.NoorChecklist>
    {
        List<NoorCheckListReturnModel> GetNoorCheckListReport();

        IEnumerable<SpNoorCheckListReturnModel> NoorCheckList();
    }
}