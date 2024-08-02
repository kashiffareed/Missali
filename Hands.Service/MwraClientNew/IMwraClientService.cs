using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.MwraClientNew
{
    public interface IMwraClientNewService : IService<Data.HandsDB.MwraClient>
    {

        MwraClient GetByIdData(int id);

        MwraClient GetClientByMwraId(int id);

         List<GetMwraClienNametByMwraIdReturnModel> GetMwraClientByMwraId(int id);
        
        List<GetMwraClienWithNameReturnModel> GetMwraClientwithName(int? appUserId=null, int? projectId= null);

        bool isMwraClientDuplicated(Data.HandsDB.MwraClient mwraClient);
    }
}
