using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.SessioFolowup
{
    public class SessionFollowup
    {
        public int SessionFollowupId { get; set; } // session_followup_id (Primary key)
        [Required]
        [StringLength(50, ErrorMessage = "Foul cannot be longer than 50 characters")]
        public string Foul { get; set; } // foul (length: 20)
        public string Advise { get; set; } // advise (length: 255)
        public string PainLower { get; set; } // pain_lower (length: 20)
        public string Danger { get; set; } // danger (length: 20)
        public int? Weight { get; set; } // weight
        public System.DateTime? LmpDate { get; set; } // lmp_date
        public string Complication { get; set; } // complication (length: 20)
        public string Bp { get; set; } // bp (length: 20)
        public string Jaundice { get; set; } // jaundice (length: 20)
        public string HistoryIrregular { get; set; } // history_irregular (length: 20)
        public string PolarAnemia { get; set; } // polar_anemia (length: 20)
        [Required]
        public int SessionId { get; set; } // session_id
        [Required]
        public int LhvId { get; set; } // lhv_id
        [Required]
        public int MarviId { get; set; } // marvi_id
        [Required]
        public int MwraId { get; set; } // mwra_id
        public System.DateTime CreatedAt { get; set; } // created_at
        public System.DateTime DateOfClientGeneration { get; set; } // DateOfFollowup
        public bool IsActive { get; set; } // IsActive

        [Required]
        public string Referral { get; set; } // referral (length: 50)
        [Required]
        public string Occasional { get; set; } // occasional (length: 50)

        public int? ProductId { get; set; } // ProductId

        public int? Quantity { get; set; } // Quantity

        public int? ProjectId { get; set; } // ProjectId
        public System.DateTime? DeviceCreatedDate { get; set; } // DeviceCreatedDate
        public Hands.Data.HandsDB.SessionFollowup SessionFollowupMapping()
        {
            Hands.Data.HandsDB.SessionFollowup sessionFollowup = new Data.HandsDB.SessionFollowup();
            //sessionFollowup.ProjectId = this.ProjectId; 
            sessionFollowup.SessionFollowupId = this.SessionFollowupId;
            sessionFollowup.Foul = this.Foul;
            sessionFollowup.Advise = this.Advise;
            sessionFollowup.PainLower = this.PainLower;
            sessionFollowup.Danger = this.Danger;
            sessionFollowup.Weight = this.Weight;
            sessionFollowup.LmpDate = this.LmpDate;
            sessionFollowup.Complication = this.Complication;
            sessionFollowup.Bp = this.Bp;
            sessionFollowup.Jaundice = this.Jaundice;
            sessionFollowup.HistoryIrregular = this.HistoryIrregular;
            sessionFollowup.PolarAnemia = this.PolarAnemia;
            sessionFollowup.SessionId = this.SessionId;
            sessionFollowup.LhvId = this.LhvId;
            sessionFollowup.MarviId = this.MarviId;
            sessionFollowup.MwraId = this.MwraId;
            sessionFollowup.DateOfFollowup = this.DateOfClientGeneration;
            sessionFollowup.CreatedAt = DateTime.Now;
            sessionFollowup.IsActive = this.IsActive;
            sessionFollowup.Referral = Referral;
            sessionFollowup.Occasional = Occasional;
            sessionFollowup.ProductId = ProductId;
            sessionFollowup.Quantity = Quantity;
            sessionFollowup.DeviceCreatedDate = this.DeviceCreatedDate;
            return sessionFollowup;
        }
    }
}
