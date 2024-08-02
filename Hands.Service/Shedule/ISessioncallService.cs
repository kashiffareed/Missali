using System.Collections.Generic;
using Hands.Data.HandsDB;
using Hands.Service;

namespace Hands.Service.Shedule
{
    public interface ISheduleService
    {
        List<GetSheduleActivityListReturnModel> GetSheduleActivityList();
    }
}