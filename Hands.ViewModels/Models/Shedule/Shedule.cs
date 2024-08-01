using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hands.Data.HandsDB;


namespace Hands.ViewModels.Models.Shedule

{
    public class Shedule
    {
        public List<GetSheduleActivityListReturnModel>SheduleList{ get; set; }
    }

    public class Items
    {

        public int session_id { get; set; }
        public System.DateTime created_at { get; set; }
        public string noorname { get; set; }
        public string lhvname { get; set; }
        public string user_type { get; set; }
        public string name { get; set; }

    }
}
