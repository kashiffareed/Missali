using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.AssignMenuToProject
{
   public class AssignMenuToProjectViewModel
    {

        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be greater than 6 characters")]
        public string Password { get; set; }
        public string id
        {
            get;
            set;
        }

        public string parent
        {
            get;
            set;
        }

        public string text
        {
            get;
            set;
        }


        //public int Id { get; set; } // Id (Primary key)
        public int? ProjectId { get; set; } // ProjectId
        public string Tittle { get; set; } // Tittle (length: 50)
        public int ParentId { get; set; } // ParentId
        public string Controller { get; set; } // Controller (length: 250)
        public string Action { get; set; } // Action (length: 50)
        public bool IsActive { get; set; } // IsActive

    }
}
