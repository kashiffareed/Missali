using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.sessioncall
{
    public class Sessioncall
    {
        public List<GetSessionCallListReturnModel> SesstioncallList { get; set; }
        public List<GetSessionCallmarviListReturnModel> SesstioncallmarviList { get; set; }
        
        public int SessionId { get; set; } // session_id (Primary key)
        public string Longitude { get; set; } // longitude (length: 20)
        public string Latitude { get; set; } // latitude (length: 20)
        public List<SPsessionInventoryReturnModel> SesstionInventoryList{ get; set; }
        public List<SPsessionmwraReturnModel> SesstionMwraList { get; set; }

        public List<SpsessionContentReturnModel> SesstionContentList { get; set; }

        public List<SessionFollowup> SesstionFollowups{ get; set; }
    }

    public class Items
    {

        public int session_id { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime next_session_schedule { get; set; }
        public System.String noorname { get; set; }
        public System.String lhvname { get; set; }
        public System.DateTime session_start_datetime { get; set; }
        public System.DateTime session_end_datetime { get; set; }
        public System.String user_type { get; set; }
        public int is_group { get; set; }
        public int is_completed { get; set; }
        public System.String name { get; set; }
    }
}
