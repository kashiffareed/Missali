using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.NrCheckList
{
    public interface INrCheckListService : IService<Data.HandsDB.NoorChecklist>
    {

        List<GetNoorClientCheckListingReturnModel> GetNrCheckList(int id);

    
    }
}
