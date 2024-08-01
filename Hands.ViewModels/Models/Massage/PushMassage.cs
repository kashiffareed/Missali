using System.ComponentModel.DataAnnotations;

namespace Hands.ViewModels.Models.Massage
{
 public   class PushMassage
    {
        public int MessageId { get; set; } // message_id (Primary key)
        [Required]
        [StringLength(250, ErrorMessage = "Message cannot be longer than 250 characters")]
        public string Message { get; set; } // message (length: 255)

      
        public System.DateTime? CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive (Primary key)
    }
}
