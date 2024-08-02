using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.UserMenuAccess
{
    public interface IUserMenuAccessService : IService<Data.HandsDB.UserMenuAccess>
    {
        List<Data.HandsDB.UserMenuAccess> getAllByUserId(string UserId);
    }
}
