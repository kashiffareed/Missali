using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Logs

{
    public interface ILogsService : IService<Data.HandsDB.Log>
    {

        List<Log> GetAppPmsAnalytics();

        List<GetAllLogwithNameReturnModel> GetAllAnalytics(int? projectId);

        List<SpPmsAnalticswithCountReturnModel> GetAllAnalyticsWithCount();
    }
}