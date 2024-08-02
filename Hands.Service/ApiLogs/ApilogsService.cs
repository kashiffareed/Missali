using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;
namespace Hands.Service.ApiLogs
{
    public class ApilogsService : ServiceBase<ApiLog>, IApilogsService
    {
        public IEnumerable<ApiLog> GetAll(string key)
        {
            return _db.ApiLogs.Where(x => x.ApiKey == key).ToList();
        }
    }
}
