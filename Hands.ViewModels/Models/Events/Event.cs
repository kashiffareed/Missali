using System.ComponentModel.DataAnnotations;

namespace Hands.ViewModels.Models.Events
{
 public   class Event
    {
        public int EventId { get; set; } // event_id (Primary key)
        [Required]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 50 characters")]
        public string Title { get; set; } // title (length: 255)
        [Required]
        [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters")]
        public string Description { get; set; } // description (length: 255)
       
        public System.DateTime? CreatedAt { get; set; } // created_at
    }
}
