using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hands.Data.HandsDB;

namespace Hands.WebAPI.Logging
{
    public class ApiUsageRepositoryDb : IApiUsageRepository
    {
        //private int _nextId = 1;
        //private static ConcurrentQueue<WebApiUsage> _aus = new ConcurrentQueue<WebApiUsage>();
        private HandsDBContext _db;

        public ApiUsageRepositoryDb()
        {
            _db = new HandsDBContext();
        }

        public IEnumerable<WebApiUsage> GetAll()
        {
            return null; //_aus.AsQueryable();
        }

        public WebApiUsage Get(int id)
        {
            var apiLog = _db.ApiLogs.Where(l=>l.Id==id).Select(l => l).FirstOrDefault();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ApiLog,WebApiUsage>(); });
            IMapper iMapper = config.CreateMapper();
            var webApiUsage = iMapper.Map<ApiLog,WebApiUsage >(apiLog);
            return webApiUsage;
        }

        public IEnumerable<WebApiUsage> GetAll(string key)
        {
            return null; // _aus.ToList().FindAll(i => i.ApiKey == key);
        }

        public WebApiUsage Add(WebApiUsage aus)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<WebApiUsage, ApiLog>(); });
            IMapper iMapper = config.CreateMapper();
            var apiLog = iMapper.Map<WebApiUsage,ApiLog >(aus);
            _db.ApiLogs.Add(apiLog);
            _db.SaveChanges();
            
            return aus;
        }
    }
}