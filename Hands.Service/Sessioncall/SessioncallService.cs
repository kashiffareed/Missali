using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;


namespace Hands.Service.Session
{
    public class SessioncallService : ServiceBase<Data.HandsDB.Session>, ISessioncallService
    {
        public List<GetSessionCallListReturnModel> GetSessionsCall()
        {

            var data = _db.GetSessionCallList(HandSession.Current.ProjectId).OrderByDescending(x=>x.session_id).ToList();
            return data;
    
        }

        public List<SPsessionInventoryReturnModel> GetSessionInventoryBysessionId(int sessionId)
        {
            return _db.SPsessionInventory(sessionId).ToList();
        }

        public List<SPsessionmwraReturnModel> GetSessionMwraBysessionId(int sessionId)
        {
            return _db.SPsessionmwra(sessionId).ToList();
        }
        public List<SpsessionContentReturnModel> GetSessionContentBysessionId(int sessionId)
        {
            return _db.SpsessionContent(sessionId).ToList();
        }

        public List<SpGetAllInventoryByUserIdReturnModel> GetAllInventoryByUserId(int? projectId , int UserId)
        {
            return _db.SpGetAllInventoryByUserId(projectId, UserId).ToList();
        }

        public List<SpMissedSessionDateReturnModel> GetAllMissedSessions()
        {
            return _db.SpMissedSessionDate();
        }

        public List<SpMissedSessionsWithMwraNamesReturnModel> GetAllMissedSessionsWIthMwraNames()
        {
            return _db.SpMissedSessionsWithMwraNames(HandSession.Current.ProjectId);
        }

        public List<GetSessionCallmarviListReturnModel> GetAllSessionCallMarviWithNames()
        {
            return _db.GetSessionCallmarviList(HandSession.Current.ProjectId);
        }
    }

     
    }

