using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;
using Hands.Service.Mwra;

namespace Hands.Service.NewMwraClientListing
{
    public class NewMwraClientService : ServiceBase<Data.HandsDB.MwraClient>, INewMwraClientService

    {
       public SpMwraClientListingNewReturnModel GetByMwraId(int? mwraclientId)
       {
           return null;
           //_db.SpMwraClientListingNew(mwraclientId);

       }
    }

   
}
