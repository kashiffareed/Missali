using System.CodeDom.Compiler;
using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.NewMwraClientListing
{
    public interface INewMwraClientService

    {
        //List<Data.HandsDB.SpMwraClientListingNewReturnModel> MwraClientlist(int?  mwraclientId);

        SpMwraClientListingNewReturnModel GetByMwraId(int? mwraclientId);


      }
}