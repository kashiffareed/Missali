using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;


namespace Hands.Service.Shedule
{
    public class SheduleService : ServiceBase<Data.HandsDB.Session>, ISheduleService
    {
        public List<GetSheduleActivityListReturnModel> GetSheduleActivityList()
        {

            var data = _db.GetSheduleActivityList(HandSession.Current.ProjectId).ToList();
            return data;
    
        }

    }

     
    }

