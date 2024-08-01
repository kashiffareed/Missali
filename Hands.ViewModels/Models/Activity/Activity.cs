using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.Activity
{
    public class Activity
    {
        public List<GetMwraSessionReturnModel> MwraSesstionlist { get; set; }
    }

    public class Items
    {

        public string name { get; set; } // session_id (Primary key)
        public System.DateTime next_session_schedule { get; set; }
    }
}
