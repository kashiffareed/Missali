using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;
using Hands.Service.Mwra;

namespace Hands.Service.MwraClientListing
{
    public class MwraClientListingService : ServiceBase<Data.HandsDB.MwraClientLisingDAta>, IMwraClientListingService
    {
        public MwraClientLisingDAta GetByIdData(int id)
        {
           // return _db.MwraClientLisingDAta.Find(id);
            return null;
        }
        //public MwraClientLisingDAta GetAllMwraClientLisingDAta()
        //{
        //    return _db.DbSet.Where(x=>x.IsActive ==true);
        //}
    }
}
