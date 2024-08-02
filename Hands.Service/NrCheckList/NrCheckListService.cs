using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Common.Common;
using Hands.Data.HandsDB;

namespace Hands.Service.NrCheckList
{
    public class NrCheckListService : ServiceBase<Data.HandsDB.NoorChecklist>, INrCheckListService
    {
        public List<GetNoorClientCheckListingReturnModel> GetNrCheckList(int id)
        {
            return _db.GetNoorClientCheckListing(id);
        }
    }
}
