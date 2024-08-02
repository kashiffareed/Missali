using System.Collections.Generic;
using Hands.Data.HandsDB;
using Hands.Service;


namespace Hands.Service.Session
{
    public interface ISessioncallService : IService<Data.HandsDB.Session>
    {
        List<GetSessionCallListReturnModel> GetSessionsCall();

        List<SPsessionInventoryReturnModel> GetSessionInventoryBysessionId(int sessionId);
        List<SPsessionmwraReturnModel> GetSessionMwraBysessionId(int sessionId);

        List<SpsessionContentReturnModel> GetSessionContentBysessionId(int sessionId);
        List<SpGetAllInventoryByUserIdReturnModel> GetAllInventoryByUserId(int? projectId , int UserId);


        List<SpMissedSessionDateReturnModel> GetAllMissedSessions();

        List<SpMissedSessionsWithMwraNamesReturnModel> GetAllMissedSessionsWIthMwraNames();

        List<GetSessionCallmarviListReturnModel> GetAllSessionCallMarviWithNames();

    }
}