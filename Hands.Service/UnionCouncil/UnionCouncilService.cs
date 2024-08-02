using System;
using System.Collections.Generic;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.UnionCouncil
{
    public class UnionCouncilService : ServiceBase<Data.HandsDB.UnionCouncil>, IUnionCouncilService
    {
        public IEnumerable<Data.HandsDB.UnionCouncil> GetAll(int taluqaId)
        {
           return _db.UnionCouncils.Where(uc => uc.TaluqaId == taluqaId && uc.ProjectId == HandSession.Current.ProjectId).ToList();
        }

        public List<UnionCouncilListingDataReturnModel> GetUnionCouncilListing()
        {
            return _db.UnionCouncilListingData(HandSession.Current.ProjectId).ToList();
        }

        public string GetNameById(int mwraUnionCouncilId)
        {
            return _db.UnionCouncils.Where(x => x.UnionCouncilId == mwraUnionCouncilId).Select(y => y.UnionCouncilName).FirstOrDefault();
        }
    }
}
