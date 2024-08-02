using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hands.WebAPI.Models
{
    public class MwraClient
    {
        public int? ProjectId { get; set; } // ProjectId
        public int MwraClientId { get; set; } // mwra_client_id (Primary key)
        [Required]
        [StringLength(50, ErrorMessage = "Foul cannot be longer than 50 characters")]
        public string Foul { get; set; } // foul (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "PainLower cannot be longer than 50 characters")]
        public string PainLower { get; set; } // pain_lower (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "Referral cannot be longer than 50 characters")]
        public string Referral { get; set; } // referral (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "History cannot be longer than 50 characters")]
        public string History { get; set; } // history (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "Contraceptual cannot be longer than 50 characters")]
        public string Contraceptual { get; set; } // contraceptual (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "DateOfStarting cannot be longer than 50 characters")]
        public string DateOfStarting { get; set; } // date_of_starting (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "Weight cannot be longer than 50 characters")]
        public string Weight { get; set; } // weight (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "LmpDate cannot be longer than 50 characters")]
        public string LmpDate { get; set; } // lmp_date (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "DateOfClientGeneration cannot be longer than 50 characters")]
        public string DateOfClientGeneration { get; set; } // date_of_client_generation (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "Bp cannot be longer than 50 characters")]
        public string Bp { get; set; } // bp (length: 50)
        public string Jaundice { get; set; } // jaundice (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "Occasional cannot be longer than 50 characters")]
        public string Occasional { get; set; } // occasional (length: 50)
        [Required]
        [StringLength(50, ErrorMessage = "PolarAnemia cannot be longer than 50 characters")]
        public string PolarAnemia { get; set; } // polar_anemia (length: 50)
        [Required]
        
        public int MwraId { get; set; } // mwra_id
       
        public int? ProductId { get; set; } // ProductId
      
        public int? Quantity { get; set; } // Quantity

        public System.DateTime? CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public int LoggedInUserId { get; set; }
        public string Longitude { get; set; } // longitude (length: 20)
        public string Latitude { get; set; } // latitude (length: 20)

        public Hands.Data.HandsDB.MwraClient MwraClientMapping()
        {
            Hands.Data.HandsDB.MwraClient mwraClient = new Data.HandsDB.MwraClient();
            //mwraClient.ProjectId = this.ProjectId;
            mwraClient.MwraClientId = this.MwraClientId;
            mwraClient.Foul = this.Foul;
            mwraClient.PainLower = this.PainLower;
            mwraClient.Referral = this.Referral;
            mwraClient.History = this.History;
            mwraClient.Contraceptual = this.Contraceptual;
            mwraClient.DateOfStarting = this.DateOfStarting;
            mwraClient.Weight = this.Weight;
            mwraClient.LmpDate = this.LmpDate;
            mwraClient.DateOfClientGeneration = this.DateOfClientGeneration;
            mwraClient.Bp = this.Bp;
            mwraClient.Jaundice = this.Jaundice;
            mwraClient.Occasional = this.Occasional;
            mwraClient.PolarAnemia = this.PolarAnemia;
            mwraClient.MwraId = this.MwraId;
            mwraClient.ProductId = ProductId;
            mwraClient.Quantity = Quantity;
            mwraClient.CreatedAt = DateTime.Now;
            mwraClient.IsActive = true;
            mwraClient.Longitude = Longitude;
            mwraClient.Latitude = Latitude;
            return mwraClient;
        }
    }
}