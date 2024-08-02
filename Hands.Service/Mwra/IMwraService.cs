using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.Service.Mwra
{
    public interface IMwraService : IService<Data.HandsDB.Mwra>
    {

        List<SpMwrasListingReturnModel> GetMwraListing();
        List<SpMwraClientListingCsvReturnModel> GetMwraClientListing();
        List<SpNewUserClientListingCsvReturnModel> GetNewUserClientListing();
        List<SpContinuedUserClientListingCsvReturnModel> GetContinuedUserClientListing();

        List<SpMwraClientListingNewReturnModel> GetAllMwraClientList(int Id);
        List<SpMwraClientListingNewReturnModel> GetAllMwraClientListBYid(int id);

        List<SpMwraDropOutClientListingReturnModel> GetAllMwraDropOutClientListBYid(int id);
        List<SpNewUserClientListingReturnModel> GetAllNewUserClientListBYid(int id);
        List<SpContinuedUserClientListingReturnModel> GetAllContinuedUserClientListBYid(int id);
        IEnumerable<Data.HandsDB.Mwra> SearchLhv(int? district, int? taluqa, int? unioncouncil);
        int GetMWRAsCount();
        IEnumerable<Data.HandsDB.GetAllMwraWithRelationNamesReturnModel> GetMwras(int? marviId, int? lhvId, int? projectId, out int totalRecords,
            int pageNo = 1, int pageSize = 10);

        List<SpMwrasListingReturnModel> SearchMwra(int? district, int? taluqa, int? unioncouncil);
        List<SpMwraClientListingNewReturnModel> SearchMwraClient(int id,int? district, int? taluqa, int? unioncouncil);
        List<SpMwraDropOutClientListingReturnModel> SearchMwraDropOutClient(int id, int? district, int? taluqa, int? unioncouncil);

        List<SpNewUserClientListingReturnModel> SearchNewUserClient(int id, int? district, int? taluqa, int? unioncouncil);
        List<SpContinuedUserClientListingReturnModel> SearchContinuedUserClient(int id, int? district, int? taluqa, int? unioncouncil);
        IEnumerable<Data.HandsDB.Mwra> GetMwrasBymarviId(int marviId);
        IEnumerable<Data.HandsDB.GetAllMwraClientWithRelationNamesReturnModel> GetMwrasByLhvID(int? lhvId, int? projectId);
        GetMwraByIdWithNamesReturnModel GetMwraByIdWithNames(int mwraId);
        Data.HandsDB.Mwra GetMwraById(int mwraId);
        List<GetAllMwraWithRelationNamesReturnModel> GetAllMwraWithRelationNames(int projectId, int? lhvId, int? marviId);

        List<GetAllMwraClientWithRelationNamesReturnModel> GetAllMwraClientWithRelationNames();

        IEnumerable<Data.HandsDB.GetMwraClienWithNameReturnModel> GetMwrasClient(int? marviId, int? lhvId,int? projectId ,out int totalRecords,
            int pageNo = 1, int pageSize = 10);

        List<SpMwraWithMwraclientReturnModel> GetAllMwrawithMwraClients(int? appUserId);
        IEnumerable<Data.HandsDB.SpMwrasListingReturnModel> GetMwrasByLhvIdCount(int? lhvId,int? projectId);
        IEnumerable<Data.HandsDB.SpMwrasListingReturnModel> GetMwrasByMarviIdCount(int? lhvId, int? projectId);

        bool isMwraDuplicated(Data.HandsDB.Mwra mwra);
    }
}
