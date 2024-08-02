using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.User
{
    public class UserService : ServiceBase<Data.HandsDB.User>, IUserService
    {
        public Data.HandsDB.User GetByEmail(string mail)
        {
            return _db.Users.FirstOrDefault(x => x.Email == mail);
        }

        public Data.HandsDB.User GetUserById(int projectId)
        {
            return _db.Users.Where(x => x.ProjectId == projectId).FirstOrDefault();
        }

        public Data.HandsDB.User GetUserByUserId(int userId)
        {
            return _db.Users.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public List<SpUsersReturnModel> GetUsers()
        {
            return _db.SpUsers(HandSession.Current.ProjectId).ToList();
        }
    }
}
