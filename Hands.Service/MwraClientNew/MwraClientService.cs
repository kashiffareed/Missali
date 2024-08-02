using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.MwraClientNew
{
    public class MwraClientService : ServiceBase<Data.HandsDB.MwraClient>, IMwraClientNewService
    {
        public MwraClient GetByIdData(int id)
        {
            return _db.MwraClients.Find(id);
        }

        public MwraClient GetClientByMwraId(int id)
        {
            return _db.MwraClients.Where(x=>x.IsActive == true).FirstOrDefault(x=>x.MwraId == id);
        }

        public List<GetMwraClienNametByMwraIdReturnModel> GetMwraClientByMwraId(int id)
        {
            var data =  _db.GetMwraClienNametByMwraId(id);
            return data.OrderByDescending(x => x.mwra_client_id).ToList();
        }
        public List<GetMwraClienWithNameReturnModel> GetMwraClientwithName(int? appUserId=null, int? projectId=null)
        {
           return _db.GetMwraClienWithName(appUserId, projectId);
            
        }

        public bool isMwraClientDuplicated(Data.HandsDB.MwraClient mwraClient)
        {
            return _db.MwraClients.Any(
                           x => x.DeviceCreatedDate == mwraClient.DeviceCreatedDate
                           && x.MwraId == mwraClient.MwraId
                           && x.ProductId == mwraClient.ProductId
                           && x.Quantity == mwraClient.Quantity
                           && x.ProjectId == mwraClient.ProjectId
                           );
        }
    }
}
