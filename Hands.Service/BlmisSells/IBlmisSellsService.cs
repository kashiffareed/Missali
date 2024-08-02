using System;
using System.Collections.Generic;
using Hands.Data.HandsDB;

namespace Hands.Service.BlmisSells
{
    public interface IBlmisSellsService : IService<Data.HandsDB.BlmisSellHistory>
    {
        List<SpBlmisSellsReturnModel> GetBlmisSells(int? projectId,int? marviId);
      
        List<GetBlmisSalesByMonthReturnModel> GetBlmisSalesByMonth(string date);

  
    }
}