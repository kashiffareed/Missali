using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.Project
{
   public class Projects
    {


        public int Id { get; set; } // Id (Primary key)
        [Required]

        public string Name { get; set; } // ProjectName (length: 500)

        public bool IsActive { get; set; } // IsActive
    }
}
