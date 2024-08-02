using System.Collections.Generic;

namespace Hands.WebAPI.Logging
{
    public interface IApiUsageRepository
    {
        IEnumerable<WebApiUsage> GetAll();
        IEnumerable<WebApiUsage> GetAll(string key);
        WebApiUsage Get(int id);
        WebApiUsage Add(WebApiUsage au);
    }
}
