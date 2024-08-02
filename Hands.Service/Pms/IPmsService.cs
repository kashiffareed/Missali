using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Pms
{
    public interface IPmsService : IService<Data.HandsDB.Pm>
    {
       List<SpContentpmsReturnModel> GetAllPmsList();

        List<GetAllContentPmsReturnModel> GetAllContentPms(int? projectId);
    }
}