using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.AssignRoleToProject
{
   public class AssignRoleToProject
    {
        public int Id { get; set; } // Id (Primary key)

        [Required]
        public int ProjectId { get; set; } // ProjectId

        [Required]
        public string RoleId { get; set; } // RoleId

        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive

        public IEnumerable<Data.HandsDB.ProjectSolution> ProjectList { get; set; }

        public IEnumerable<Data.HandsDB.AspNetRole> RoleList { get; set; }
    }
}
