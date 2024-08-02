using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.UnionCouncil
{
    public interface IUnionCouncilService: IService<Data.HandsDB.UnionCouncil>
    {
        IEnumerable<Data.HandsDB.UnionCouncil> GetAll(int taluqaId);
        List<UnionCouncilListingDataReturnModel> GetUnionCouncilListing();
        string GetNameById(int mwraUnionCouncilId);
    }
}