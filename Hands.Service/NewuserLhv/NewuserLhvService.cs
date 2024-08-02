using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Service.NewuserLhv;


namespace Hands.Service.NewuserLhv
{
    public class NewuserLhvService : ServiceBase<Data.HandsDB.AppUser>, INewuserLhvService
    {
        
        public List<Data.HandsDB.NewUserEachLhvByRegionIdReturnModel> NewUserEachLhvByRegionId(int? regionId = null)
        {
            return  _db.NewUserEachLhvByRegionId(regionId, HandSession.Current.ProjectId).ToList();
        }
        
    }
}

