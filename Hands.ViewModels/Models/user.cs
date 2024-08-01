using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;




namespace Hands.ViewModels.Models
{
    public class user
    {
        public int UserId { get; set; }
        [StringLength(50, ErrorMessage = "User Name cannot be longer than 50 characters")]
        public string FullName { get; set; } // full_name (length: 255)
        [Required]
        [StringLength(50, ErrorMessage = "Full Name cannot be longer than 50 characters")]
        public string UserName { get; set; } // user_name (length: 255)
        [Required]
        [MinLength(6, ErrorMessage = "Password must be greater than 6 characters")]
        public string Pwd { get; set; } // pwd (length: 255)
        public string PlainPassword { get; set; } // plain_password (length: 500)
        [Required]
        public string Email { get; set; } // email (length: 255)
        public bool IsActive { get; set; } // is_active
        [Required]
        public int RoleId { get; set; }
    
        //[Required]
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string RoleName { get; set; }// email (length: 255)
        [Required]
        [StringLength(50, ErrorMessage = "Designation cannot be longer than 50 characters")]
        public string Designation { get; set; } // designation (length: 500)
        public IList<Data.HandsDB.User> Users;

        public IEnumerable<Data.HandsDB.SpGetRolesByProjectIdReturnModel> Role { get; set; }
        public IEnumerable<Data.HandsDB.ProjectSolution> Project { get; set; }

        public IEnumerable<Data.HandsDB.AssignRoleToProject> ProjectRole { get; set; }
    }
}
