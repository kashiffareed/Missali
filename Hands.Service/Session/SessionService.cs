using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Hands.Common.Common;
using Hands.Data.HandsDB;


namespace Hands.Service.Session
{
    public class SessionService : ServiceBase<Data.HandsDB.Session>, ISessionService
    {
        public List<Data.HandsDB.GetMwraSessionReturnModel> GetMwraSessions(int? projectId)
        {

            return _db.GetMwraSession(projectId).ToList();
        }

        public IEnumerable<Data.HandsDB.Session> GetSessionsData(int? appUserId, int? projectId, DateTime? startDateTime, DateTime? enDateTimed, DateTime? nextsessionDateTime, out int totalRecords, int pageNo = 1, int pageSize = 10)
        {
            var query = _db.Sessions.Select(m => m);
            if (appUserId.HasValue)
            {
                query = query.Where(x => x.LhvId == appUserId || x.MarviId == appUserId);
            }
            if (startDateTime.HasValue)
            {
                query = query.Where(x => x.SessionStartDatetime == startDateTime);
            }
            if (enDateTimed.HasValue)
            {
                query = query.Where(x => x.SessionEndDatetime == enDateTimed);
            }
            if (nextsessionDateTime.HasValue)
            {
                var datenextsessionDateTime = Convert.ToDateTime(nextsessionDateTime.Value.Date);
                query = query.Where(c => c.NextSessionSchedule >= datenextsessionDateTime);
            }

            totalRecords = query.Count();
            query = query.Where(x => x.ProjectId == projectId && x.IsActive).OrderBy(x => x.SessionId);
            return query.OrderBy(x => x.SessionId).Skip(pageSize * (pageNo - 1)).Take(pageSize).ToList();
        }

        public Data.HandsDB.Session getSEssionDAta()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.HandsDB.GetFutureSessionDataReturnModel> GetSessionsFutureData(int appUserId, DateTime nextsessionDateTime, int? projectId)
        {
            return _db.GetFutureSessionData(appUserId, nextsessionDateTime, projectId);



        }

        public int GetMwraCountById(int id)
        {
            return _db.SessionMwras.Where(x => x.SessionId == id).ToList().Count();
        }

        public List<SessionMwra> GetSessionMwraBySessionIds(int[] id)
        {
            return _db.SessionMwras.Where(x => id.Contains(x.SessionId)).ToList();
        }

        public List<MwraSessionDto> GetMwraBySessionIds(int[] id)
        {
            return (from sessMrwa in _db.SessionMwras
                join mrwa in _db.Mwras on sessMrwa.MwraId equals mrwa.MwraId
                where id.Contains(sessMrwa.SessionId)
                select new MwraSessionDto()
                {
                    SessionId = sessMrwa.SessionId,
                    MwraName = mrwa.Name
                }).ToList();
        }

        public List<GetMwraNamesBySessionIdReturnModel> GetMwraNamesBySessionId(int sessionId)
        {
            return _db.GetMwraNamesBySessionId(sessionId);
        }


        public bool isSessionDuplicated(Data.HandsDB.Session session)
        {
            return _db.Sessions.Any(x => x.SessionStartDatetime == session.SessionStartDatetime &&
                                      x.SessionEndDatetime == session.SessionEndDatetime
                                      && x.MarviId == session.MarviId
                                      && session.LhvId == session.LhvId
                                      && session.UserType == session.UserType
                                      && session.ProjectId == session.ProjectId);
        }
    }

    public class MwraSessionDto
    {
        public int SessionId { get; set; }
        public string MwraName { get; set; }
    }
}

