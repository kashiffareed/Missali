using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.Cms
{
    public interface ICmsService : IService<Data.HandsDB.Content>
    {

        List<SpContentcmsReturnModel> GetAllcmsList(int projectId);

        IEnumerable<GetAllContentReturnModel> GetcontentByType(int? projectId);
    }
}