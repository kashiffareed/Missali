using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.MwraClientListing
{
    public interface IMwraClientListingService : IService<Data.HandsDB.MwraClientLisingDAta>
    {

        MwraClientLisingDAta GetByIdData(int id);
        //IEnumerable<MwraClientLisingDAta> GetAllMwraClientLisingDAta();
    }
}
