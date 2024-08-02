using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;


namespace Hands.Service.MethodwiseCount
{
    public class MethodwiseCountService : ServiceBase<Data.HandsDB.AppUser>, IMethodwiseCountService
    {
        public List<Data.HandsDB.SpCurrentUserMethodWiseReturnModel> CurrentUserMethodWiseList()
        {
            return  _db.SpCurrentUserMethodWise(HandSession.Current.ProjectId).ToList();
        }
    }
}

