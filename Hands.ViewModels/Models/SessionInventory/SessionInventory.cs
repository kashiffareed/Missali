using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.SessionInventory
{
  public class SessionInventory
    {
        public int Id { get; set; } // Id (Primary key)
        [Required]
        public int? SessionId { get; set; } // SessionId
        [Required]
        public int? ProductId { get; set; } // ProductId
        [Required]
        public int? Quantity { get; set; } // Quantity
        [Required]
        public int? UserId { get; set; } // UserId
        [Required]
        public int? MwraId { get; set; } // MwraId
        public bool IsActive { get; set; } // IsActive

        public int? ProjectId { get; set; } // projectId
    }
}
