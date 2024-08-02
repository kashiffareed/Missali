using Hands.Data.HandsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Hands.Service.User
{
    public interface IUserService : IService<Data.HandsDB.User>
    {
        Data.HandsDB.User GetByEmail(string mail);

        Data.HandsDB.User GetUserById(int projectId);

        Data.HandsDB.User GetUserByUserId(int userId);
        List<SpUsersReturnModel> GetUsers();
    }
}
