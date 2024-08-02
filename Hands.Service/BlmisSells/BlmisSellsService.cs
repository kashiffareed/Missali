using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.Product;

namespace Hands.Service.BlmisSells
{
    
    public class BlmisSellsService : ServiceBase<Data.HandsDB.BlmisSellHistory>, IBlmisSellsService
    {
        public List<SpBlmisSellsReturnModel> GetBlmisSells(int? projectId,int? marviId)
        {
            return _db.SpBlmisSells(projectId,marviId);
        }

        public List<GetBlmisSalesByMonthReturnModel> GetBlmisSalesByMonth(string date)
        {
            var data = _db.GetBlmisSalesByMonth(date,HandSession.Current.ProjectId).ToList();
            return data;
        }

    }

 }

