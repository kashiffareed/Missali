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
using Hands.Service.MwraClientNew;
using Hands.Service.Noor;

namespace Hands.Service.Mwra
{
    public class MwraService : ServiceBase<Data.HandsDB.Mwra>, IMwraService
    {
        private INoorService _noorService;
        private readonly IMwraClientNewService _mwraClientService;

        public MwraService()
        {
            _noorService = new NoorService();
            _mwraClientService = new MwraClientService();
        }
        public List<SpMwrasListingReturnModel> GetMwraListing()
        {
            return _db.SpMwrasListing(HandSession.Current.ProjectId).ToList();

        }
        public List<SpMwraClientListingCsvReturnModel> GetMwraClientListing()
        {
            return _db.SpMwraClientListingCsv(HandSession.Current.ProjectId).ToList();

        }
        public List<SpNewUserClientListingCsvReturnModel> GetNewUserClientListing()
        {
            return _db.SpNewUserClientListingCsv(HandSession.Current.ProjectId).ToList();

        }
        public List<SpContinuedUserClientListingCsvReturnModel> GetContinuedUserClientListing()
        {
            return _db.SpContinuedUserClientListingCsv(HandSession.Current.ProjectId).ToList();

        }

        public List<SpMwraClientListingNewReturnModel> GetAllMwraClientList(int Id)
        {
            return _db.SpMwraClientListingNew(Id, HandSession.Current.ProjectId);
        }

        public List<SpMwraClientListingNewReturnModel> GetAllMwraClientListBYid(int id)
        {
            return _db.SpMwraClientListingNew(id, HandSession.Current.ProjectId);
        }

        public List<SpMwraDropOutClientListingReturnModel> GetAllMwraDropOutClientListBYid(int id)
        {
            return _db.SpMwraDropOutClientListing(id, HandSession.Current.ProjectId);
        }
        public List<SpNewUserClientListingReturnModel> GetAllNewUserClientListBYid(int id)
        {
            return _db.SpNewUserClientListing(id, HandSession.Current.ProjectId);
        }
        public List<SpContinuedUserClientListingReturnModel> GetAllContinuedUserClientListBYid(int id)
        {
            return _db.SpContinuedUserClientListing(id, HandSession.Current.ProjectId);
        }

        public IEnumerable<Data.HandsDB.Mwra> SearchLhv(int? district, int? taluqa, int? unioncouncil)
        {
            {
                var query = _db.Mwras
                    .Where(x => x.IsActive).ToList();

                if (district.HasValue)
                {
                    query = query.Where(x =>
                        x.RegionId == district).ToList();
                }
                if (taluqa.HasValue)
                {
                    query = query.Where(x => x.TaluqaId == taluqa).ToList();
                }
                if (unioncouncil.HasValue)
                {
                    query = query.Where(x => x.UnionCouncilId == unioncouncil).ToList();
                }

                return query.ToList();

            }
        }

        public List<Data.HandsDB.Mwra> GetMwraByAssignedId(int id)
        {
            return _db.Mwras.Where(x => x.AssignedMarviId == id && x.IsActive == true).ToList();
        }

        public int GetMWRAsCount()
        {
            return _db.Mwras.Count();
        }

        //public IEnumerable<Data.HandsDB.Mwra> GetMwras(int? marviId, int? lhvId,
        //    out int totalRecords, int pageNo = 1, int pageSize = 10)
        //{
        //    var query = _db.Mwras.Select(m => m);
        //    if (lhvId.HasValue)
        //    {
        //        var marviIds = _noorService.GetLHvById(lhvId);
        //        query = query.Where(x => marviIds.Contains(x.AssignedMarviId));
        //    }
        //    if (marviId.HasValue)
        //    {
        //        query = query.Where(x => x.AssignedMarviId == marviId);
        //    }
        //    totalRecords = query.Count();
        //    query = query.Where(x => x.IsActive).OrderBy(x=>x.MwraId).Skip(pageSize*(pageNo - 1)).Take(pageSize);
        //    return query.ToList();
        //}
        public IEnumerable<Data.HandsDB.GetAllMwraWithRelationNamesReturnModel> GetMwras(int? marviId, int? lhvId, int? projectId, out int totalRecords, int pageNo = 1, int pageSize = 10)
        {
            //var query = GetAllMwraWithRelationNames(projectId.ToInt()).ToList();
            var query = GetAllMwraWithRelationNames(projectId.ToInt(), lhvId, marviId).ToList();
            totalRecords = query.Count;
            query = query.Where(x => x.IsActive).ToList();
            return query.ToList();
        }

        public List<SpMwrasListingReturnModel> SearchMwra(int? district, int? taluqa, int? unioncouncil)
        {
            var query = _db.SpMwrasListing(HandSession.Current.ProjectId).ToList();

            if (district.HasValue)
            {
                query = query.Where(x =>
                    x.region_id == district).ToList();
            }
            if (taluqa.HasValue)
            {
                query = query.Where(x => x.taluqa_id == taluqa).ToList();
            }
            if (unioncouncil.HasValue)
            {
                query = query.Where(x => x.union_council_id == unioncouncil).ToList();
            }

            return query.ToList();
        }

        public List<SpMwraClientListingNewReturnModel> SearchMwraClient(int id, int? district, int? taluqa, int? unioncouncil)
        {
            var query = _db.SpMwraClientListingNew(id, HandSession.Current.ProjectId).ToList();

            if (district.HasValue)
            {
                query = query.Where(x =>
                    x.region_id == district).ToList();
            }
            if (taluqa.HasValue)
            {
                query = query.Where(x => x.taluqa_id == taluqa).ToList();
            }
            if (unioncouncil.HasValue)
            {
                query = query.Where(x => x.union_council_id == unioncouncil).ToList();
            }

            return query.ToList();
        }

        public List<SpMwraDropOutClientListingReturnModel> SearchMwraDropOutClient(int id, int? district, int? taluqa, int? unioncouncil)
        {
            var query = _db.SpMwraDropOutClientListing(id, HandSession.Current.ProjectId).ToList();

            if (district.HasValue)
            {
                query = query.Where(x =>
                    x.region_id == district).ToList();
            }
            if (taluqa.HasValue)
            {
                query = query.Where(x => x.taluqa_id == taluqa).ToList();
            }
            if (unioncouncil.HasValue)
            {
                query = query.Where(x => x.union_council_id == unioncouncil).ToList();
            }

            return query.ToList();
        }

        public List<SpNewUserClientListingReturnModel> SearchNewUserClient(int id, int? district, int? taluqa, int? unioncouncil)
        {
            var query = _db.SpNewUserClientListing(id, HandSession.Current.ProjectId).ToList();

            if (district.HasValue)
            {
                query = query.Where(x =>
                    x.region_id == district).ToList();
            }
            if (taluqa.HasValue)
            {
                query = query.Where(x => x.taluqa_id == taluqa).ToList();
            }
            if (unioncouncil.HasValue)
            {
                query = query.Where(x => x.union_council_id == unioncouncil).ToList();
            }

            return query.ToList();
        }

        public List<SpContinuedUserClientListingReturnModel> SearchContinuedUserClient(int id, int? district, int? taluqa, int? unioncouncil)
        {
            var query = _db.SpContinuedUserClientListing(id, HandSession.Current.ProjectId).ToList();

            if (district.HasValue)
            {
                query = query.Where(x =>
                    x.region_id == district).ToList();
            }
            if (taluqa.HasValue)
            {
                query = query.Where(x => x.taluqa_id == taluqa).ToList();
            }
            if (unioncouncil.HasValue)
            {
                query = query.Where(x => x.union_council_id == unioncouncil).ToList();
            }

            return query.ToList();
        }

        public IEnumerable<Data.HandsDB.Mwra> GetMwrasBymarviId(int marviId)
        {
            return _db.Mwras.Where(x => x.AssignedMarviId == marviId && x.IsActive).ToList();
        }

      

        public IEnumerable<Data.HandsDB.GetAllMwraClientWithRelationNamesReturnModel> GetMwrasByLhvID(int? lhvId, int? projectId)
        {
            var query = _db.GetAllMwraClientWithRelationNames();
            if (lhvId.HasValue)
            {
                var marviIds = _noorService.GetLHvById(lhvId, projectId);
                query = query.Where(x => marviIds.Contains(x.assigned_marvi_id)).ToList();
            }
            return query.ToList();
        }

        public IEnumerable<Data.HandsDB.SpMwrasListingReturnModel> GetMwrasByLhvIdCount(int? lhvId, int? projectId)
        {
            var query = _db.SpMwrasListing(projectId);
            if (lhvId.HasValue)
            {
                List<int> marviIds = _noorService.GetLHvById(lhvId, projectId);
                query = query.Where(x => marviIds.Contains(x.assigned_marvi_id)).ToList();
            }
            return query.ToList();
        }
        public IEnumerable<Data.HandsDB.SpMwrasListingReturnModel> GetMwrasByMarviIdCount(int? lhvId, int? projectId)
        {
            var query = _db.SpMwrasListing(projectId);
            if (lhvId.HasValue)
            {
               
                query = query.Where(x => x.assigned_marvi_id == lhvId).ToList();
            }
            return query.ToList();
        }

        public GetMwraByIdWithNamesReturnModel GetMwraByIdWithNames(int mwraId)
        {
            return _db.GetMwraByIdWithNames(mwraId).SingleOrDefault();
        }

        public Data.HandsDB.Mwra GetMwraById(int mwraId)
        {
            return _db.Mwras.FirstOrDefault(x => x.MwraId == mwraId);
        }

        public List<GetAllMwraWithRelationNamesReturnModel> GetAllMwraWithRelationNames(int projectId,int? lhvId, int? marviId)
        {
            return _db.GetAllMwraWithRelationNames(projectId, lhvId, marviId);
        }

        public List<GetAllMwraClientWithRelationNamesReturnModel> GetAllMwraClientWithRelationNames()
        {
            return _db.GetAllMwraClientWithRelationNames();
        }

        public IEnumerable<Data.HandsDB.GetMwraClienWithNameReturnModel> GetMwrasClient(int? marviId, int? lhvId,int? projectId,
            out int totalRecords, int pageNo = 1, int pageSize = 10)
        {
            List<GetMwraClienWithNameReturnModel> client;
            if (lhvId.HasValue)
            {
                //var query = _db.Mwras.Select(m => m);
                //var marviIds = _noorService.GetLHvById(lhvId);
                //query = query.Where(x => marviIds.Contains(x.AssignedMarviId));
                //var mwras = query.Select(x => x.MwraId).ToList();
                //client = client.Where(x => mwras.Contains(x.MwraId.Value)).ToList();
                client = _mwraClientService.GetMwraClientwithName(lhvId.Value, projectId);

            }
            else if (marviId.HasValue)
            {
                //var query = _db.Mwras.Select(m => m);
                //var mwraList = query.Where(x => x.AssignedMarviId == marviId).Select(x => x.MwraId).ToList();
                //client = client.Where(x => mwraList.Contains(x.MwraId.Value)).ToList();
                client = _mwraClientService.GetMwraClientwithName(marviId.Value, projectId);
            }
            else
            {
                client = _mwraClientService.GetMwraClientwithName(0,0);
            }
            totalRecords = client.Count;
            return client;
        }

        public List<SpMwraWithMwraclientReturnModel> GetAllMwrawithMwraClients(int? appUserId)
        {
            return _db.SpMwraWithMwraclient(appUserId);
        }

        public bool isMwraDuplicated(Data.HandsDB.Mwra mwra)
        {
            return _db.Mwras.Any(
                           x => x.DeviceCreatedDate == mwra.DeviceCreatedDate
                           && x.Name == mwra.Name
                           && x.HusbandName == mwra.HusbandName
                           && x.AssignedMarviId == mwra.AssignedMarviId
                           && x.RegionId == mwra.RegionId
                           && x.TaluqaId == mwra.TaluqaId
                           && x.UnionCouncilId == mwra.UnionCouncilId
                           && x.ProjectId == mwra.ProjectId
                           );
        }
    }
}
