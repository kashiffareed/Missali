using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.Taluqa
{
    public class TaluqaService : ServiceBase<Data.HandsDB.Taluqa>, ITaluqaService
    {
        public IEnumerable<Data.HandsDB.Taluqa> GetAll(int regionId)
        {
        return _db.Taluqas.Where(t => t.RegionId == regionId && t.ProjectId == HandSession.Current.ProjectId).ToList();
        }

        public List<TaluqaListingDataReturnModel> GetTaluqaListing()
        {
            return _db.TaluqaListingData(HandSession.Current.ProjectId).ToList();
        }

        public string GetNameById(int? id)
        {
            return _db.Taluqas.Where(a => a.TaluqaId == id).Select(x => x.TaluqaName).FirstOrDefault();
        }
    }
}
