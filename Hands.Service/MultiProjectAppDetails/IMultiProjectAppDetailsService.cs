using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.MultiProjectAppDetails
{
    public interface IMultiProjectAppDetailsService : IService<Data.HandsDB.MultiProjectAppDetail>
    {
        List<Data.HandsDB.MultiProjectAppDetail> GetAllMenuDetailByProjectId(int projectId);
        List<SpMultiProjectAppDetailsReturnModel> GetMultiProjectAppDetails(int? projectId);
    }
}