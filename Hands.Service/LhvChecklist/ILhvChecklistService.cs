using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.LhvChecklist
{
    public interface ILhvChecklistService : IService<Data.HandsDB.LhvChecklist>
    {

        IEnumerable<SpLhvCheckListReturnModel> GetAllLHvClCheckList();
    }
}