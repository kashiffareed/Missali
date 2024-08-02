using System;
using System.Collections.Generic;
using Hands.Data.HandsDB;
using Hands.Service;

namespace Hands.Service.Session
{
    public interface ISessionService : IService<Data.HandsDB.Session>
    {
        List<Data.HandsDB.GetMwraSessionReturnModel> GetMwraSessions(int? projectId);

        IEnumerable<Data.HandsDB.Session> GetSessionsData(int? lhvId,int? projectId, DateTime? startDateTime, DateTime? enDateTimed,
            DateTime? nextsessionDateTime, out int totalRecords,
        int pageNo = 1, int pageSize = 10);
        
        Data.HandsDB.Session getSEssionDAta();
        IEnumerable<Data.HandsDB.GetFutureSessionDataReturnModel> GetSessionsFutureData(int appUserId, DateTime nextsessionDateTime,int? projectId);


       
        int GetMwraCountById(int mwraAssignedMarviId);

        List<SessionMwra> GetSessionMwraBySessionIds(int[] id);

        List<MwraSessionDto> GetMwraBySessionIds(int[] id);

        List<GetMwraNamesBySessionIdReturnModel> GetMwraNamesBySessionId(int sessionId);


        bool isSessionDuplicated(Data.HandsDB.Session session);
    }
}