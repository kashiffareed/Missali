using System.Collections.Generic;
using System.Linq;


namespace Hands.Service.Session
{
    public class SessionMwraService : ServiceBase<Data.HandsDB.SessionMwra>, ISessionMwraService
    {
        public List<Data.HandsDB.SessionMwra> GetAllSessions()
        {
            return _db.SessionMwras.ToList();
        }

    }

     
    }

