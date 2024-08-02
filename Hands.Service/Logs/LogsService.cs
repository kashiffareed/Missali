using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Cms;
namespace Hands.Service.Logs
{
    public class LogsService : ServiceBase<Log>, ILogsService
    {
        public List<Log> GetAppPmsAnalytics()
        {
            return _db.Logs.Where(x => x.IsActive).OrderByDescending(x=>x.LogId).ToList();
        }

        public List<GetAllLogwithNameReturnModel> GetAllAnalytics(int? projectId)
        {
            return _db.GetAllLogwithName(projectId).OrderByDescending(x => x.log_id).ToList();
        }

        public List<SpPmsAnalticswithCountReturnModel> GetAllAnalyticsWithCount()
        {
            return _db.SpPmsAnalticswithCount(HandSession.Current.ProjectId);
        }
    }

     
    }

