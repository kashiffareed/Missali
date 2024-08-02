using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.MenuAssessLevel
{
    public class MenuLevelAccessService : ServiceBase<Data.HandsDB.MenuLevelAccess>, IMenuLevelAccessService
    {
        public List<MenuLevelAccess> GetListByMenuId(int menuId)
        {
            return _db.MenuLevelAccesses.Where(x => x.MenuId == menuId).ToList();
        }

        public List<MenuLevelAccess> GetListByMenuIdString(string Id)
        {
            return _db.MenuLevelAccesses.Where(x => x.Id == Id).ToList();
        }
    }
}
