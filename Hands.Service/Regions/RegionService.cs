using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.Regions
{
    public class RegionService : ServiceBase<Region>,IRegionService
    {
        public IEnumerable<Region> GetRegionsByName(string name)
        {
            return _db.Regions.Where(r => r.RegionName == name);
        }

        public string GetNameById(int? appUserRegionId)
        {
            return _db.Regions.Where(a => a.RegionsId == appUserRegionId).Select(x => x.RegionName).FirstOrDefault();
        }

        public List<Region> GetRegionList()
        {
            return _db.Regions.ToList();
        }
    }
}
