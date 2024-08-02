using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.ApiLogs
{
   public interface IApilogsService : IService<ApiLog>
    {

        IEnumerable<ApiLog> GetAll(string key);
    }
}
