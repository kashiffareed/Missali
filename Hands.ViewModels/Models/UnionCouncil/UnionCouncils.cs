using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.UnionCouncil
{
    public class UnionCouncils
    {

        public int UnionCouncilId { get; set; } // union_council_id (Primary key)

        [Required]
        [StringLength(50, ErrorMessage = "UnionCounci lName cannot be longer than 50 characters")]
        public string UnionCouncilName { get; set; } // union_council_name (length: 255)
        [Required]
        public int TaluqaId { get; set; } // taluqa_id

        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public IEnumerable<Data.HandsDB.Taluqa> Taluqa { get; set; }
    }
}
