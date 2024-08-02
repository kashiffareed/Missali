using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.SessionFollowup
{
  public interface ISessionfollowupService : IService<Data.HandsDB.SessionFollowup>
    {
        Data.HandsDB.SessionFollowup GetBysessionId(int SessionId);

        Data.HandsDB.SessionFollowup GetBymwraId(int Id);

        List<Data.HandsDB.GetfollowupByNamesReturnModel> GetAllFollowups(int? projectId);

        List<Data.HandsDB.GetfollowupByNamesByLhvIdReturnModel> GetAllFollowupsByLhv(int? LhvId, int? projectId);
    
        int GetlhvIdByMwraId(int Id);

        bool isSessionFollowupDuplicated(Data.HandsDB.SessionFollowup sessionFollowup);
    }
}
