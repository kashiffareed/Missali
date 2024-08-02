using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Common.Common;

namespace Hands.Service.SessionFollowup
{
    public class SessionfollowupService : ServiceBase<Data.HandsDB.SessionFollowup>, ISessionfollowupService
    {
        public Data.HandsDB.SessionFollowup GetBysessionId(int SessionId)
        {
            return _db.SessionFollowups.FirstOrDefault(x => x.SessionId == SessionId);
        }

        public Data.HandsDB.SessionFollowup GetBymwraId(int Id)
        {
            return _db.SessionFollowups.FirstOrDefault(x => x.MwraId == Id);
        }

        public List<Data.HandsDB.GetfollowupByNamesReturnModel> GetAllFollowups(int? projectId)
        {
            return _db.GetfollowupByNames(projectId).OrderByDescending(x => x.session_followup_id).ToList();

        }

        public List<Data.HandsDB.GetfollowupByNamesByLhvIdReturnModel> GetAllFollowupsByLhv(int? LhvId,int? projectId)
        {
            return _db.GetfollowupByNamesByLhvId(LhvId, projectId).OrderByDescending(x => x.session_followup_id).ToList();

        }

        public int GetlhvIdByMwraId(int Id)
        {
            var mwra = _db.Mwras.FirstOrDefault(x => x.MwraId == Id);
            var marvi = _db.AppUsers.FirstOrDefault(x => x.AppUserId == mwra.AssignedMarviId);
            if (marvi.LhvAssigned != 0)
            {
                return  _db.AppUsers.FirstOrDefault(x => x.AppUserId == marvi.LhvAssigned).AppUserId;                 
            }
            return 0;

        }

        public bool isSessionFollowupDuplicated(Data.HandsDB.SessionFollowup sessionFollowup)
        {
            return _db.SessionFollowups.Any(
                           x => x.DeviceCreatedDate == sessionFollowup.DeviceCreatedDate
                           && x.MwraId == sessionFollowup.MwraId
                           && x.ProductId == sessionFollowup.ProductId
                           && x.Quantity == sessionFollowup.Quantity
                           && x.ProjectId == sessionFollowup.ProjectId
                           );
        }
    }
}
