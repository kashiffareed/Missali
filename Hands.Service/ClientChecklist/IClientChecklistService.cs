using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.ClientChecklist
{
    public interface IClientChecklistService : IService<Data.HandsDB.ClientChecklist>
    {
        IEnumerable<SpClientCheckListReturnModel> GetAllClientCheckList();
    }
}