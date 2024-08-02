using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.Pms
{
    public class PmsService : ServiceBase<Pm>, IPmsService
    {
        public List<SpContentpmsReturnModel> GetAllPmsList()
        {
            return _db.SpContentpms(HandSession.Current.ProjectId).ToList();
        }

        public List<GetAllContentPmsReturnModel> GetAllContentPms(int? projectId)
        {
            return _db.GetAllContentPms(projectId).OrderByDescending(x=>x.content_id).ToList();
        }
    }
}

