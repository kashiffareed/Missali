using System.Collections.Generic;
using Hands.Data.HandsDB;
using Hands.Service;

namespace Hands.Service.Session
{
    public interface ISessionMwraService : IService<Data.HandsDB.SessionMwra>
    {
        List<Data.HandsDB.SessionMwra> GetAllSessions();
    }
}