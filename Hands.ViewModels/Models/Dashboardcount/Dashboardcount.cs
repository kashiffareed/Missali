using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.Dashboardcount
{
    public class Dashboardcount
    {
        public List<GetDashboardcountReturnModel> Dashboardlist { get; set; }
    }

    public class Items
    {

        public System.Int32? Mawra { get; set; }
        public System.Int32? NoorWorker { get; set; }
        public System.Int32? LHV { get; set; }
        public System.Int32? Product { get; set; }
    }
}
