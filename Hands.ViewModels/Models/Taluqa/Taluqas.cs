using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.Taluqa
{
   public class Taluqas
    {
        public int TaluqaId { get; set; }

        [Required]
        public string TaluqaName { get; set; } // taluqa_name (length: 255)
        [Required]
        public int RegionId { get; set; } // region_id
     
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
      
        public IEnumerable<Region> Regions { get; set; }
    }
}
