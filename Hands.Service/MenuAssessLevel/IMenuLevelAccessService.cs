using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.MenuAssessLevel
{
   public interface IMenuLevelAccessService : IService<Data.HandsDB.MenuLevelAccess>
   {
       List<Data.HandsDB.MenuLevelAccess> GetListByMenuId(int menuId);


       List<Data.HandsDB.MenuLevelAccess> GetListByMenuIdString(string Id);
    }
}
