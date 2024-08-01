using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.SessionMwraWithContent
{
    public class SessionMwraWithContent
    {
        public int SessionFollowUpId { get; set; }
        public int SessionId { get; set; } // session_id (Primary key)
        public System.DateTime NextSessionSchedule { get; set; } // next_session_schedule
        public short IsCompleted { get; set; } // is_completed
        [Required]
        [StringLength(50, ErrorMessage = "Longitude cannot be longer than 50 characters")]
        public string Longitude { get; set; } // longitude (length: 20)
        [Required]
        [StringLength(50, ErrorMessage = "Latitude cannot be longer than 50 characters")]
        public string Latitude { get; set; } // latitude (length: 20)

        public System.DateTime SessionStartDatetime { get; set; } // session_start_datetime
        public System.DateTime SessionEndDatetime { get; set; } // session_end_datetime
        public short IsGroup { get; set; } // is_group
        [Required]

        public int MarviId { get; set; } // marvi_id
        [Required]

        public int MobileSessionId { get; set; } // mobile_session_id
        [Required]
        public int LhvId { get; set; } // lhv_id
        [Required]
        [StringLength(50, ErrorMessage = "UserType cannot be longer than 50 characters")]
        public string UserType { get; set; } // user_type (length: 50)
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive

        public int? ProjectId { get; set; } // ProjectId
        public IList<SessionMwra> SessionMwra { get; set; }
        public IList<SessionContent> SessionContent { get; set; }
        public Hands.ViewModels.Models.SessioFolowup.SessionFollowup SessionFollowup { get; set; }
        public IList<Data.HandsDB.SessionInventory> SessionInventory { get; set; }

        public Hands.Data.HandsDB.Session SessionMapping()
        {
            Hands.Data.HandsDB.Session session = new Data.HandsDB.Session();

            //session.ProjectId = ProjectId;
            session.SessionId = this.SessionId;
            session.NextSessionSchedule = this.NextSessionSchedule;
            session.IsCompleted = this.IsCompleted;
            session.Longitude = this.Longitude;
            session.Latitude = this.Latitude;
            session.SessionStartDatetime = this.SessionStartDatetime;
            session.SessionEndDatetime = this.SessionEndDatetime;
            session.IsGroup = this.IsGroup;
            session.MarviId = this.MarviId;
            session.MobileSessionId = this.MobileSessionId;
            session.LhvId = this.LhvId;
            session.UserType = this.UserType;
            session.SessionId = this.SessionId;
            session.LhvId = this.LhvId;
            session.MarviId = this.MarviId;
            session.CreatedAt = DateTime.Now;
            session.IsActive = this.IsActive;
            return session;
        }

    }
}
