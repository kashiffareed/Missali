using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Hands.ViewModels.Models.Log

{
    public class Log
    {
  
        public int? ProjectId { get; set; } // ProjectId
        public int LogId { get; set; } // log_id (Primary key)
        [Required]
        [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters")]
        public string Description { get; set; } // description
        [Required]
        [StringLength(250, ErrorMessage = "UserTypecannot be longer than 250 characters")]
        public string UserType { get; set; } // user_type (length: 20)
        public int UserId { get; set; } // user_id
        [Required]
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public string Duration { get; set; } // Duration (length: 50)
        public bool? IsComplete { get; set; } // IsComplete

        public Data.HandsDB.Log GetLogEntity()
        {
            Data.HandsDB.Log log = new Data.HandsDB.Log();
            log.ProjectId = ProjectId;
            log.Description = Description;
            log.UserType = UserType;
            log.UserId = UserId;
            log.CreatedAt = CreatedAt;
            log.IsActive = true;
            log.IsComplete = IsComplete;
            log.Duration = Duration;
            return log;
        }
    }
}

