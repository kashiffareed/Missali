using System.Collections.Generic;

namespace Hands.Service.MethodwiseCount
{
    public interface IMethodwiseCountService
    {
         List<Data.HandsDB.SpCurrentUserMethodWiseReturnModel> CurrentUserMethodWiseList();
      
    }
}