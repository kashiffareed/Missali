using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.Service.AspnetUsers
{
    public interface IAspnetUserService : IService<Data.HandsDB.AspNetUser>
    {

        Data.HandsDB.AspNetUser GetByEmailAspnetUser(string mail);
    }
}
