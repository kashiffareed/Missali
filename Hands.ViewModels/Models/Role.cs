using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models
{
   public class Role
    {
        public int RoleId { get; set; } // role_id (Primary key)

        [Required]
        [StringLength(50, ErrorMessage = "Roll Name be longer than 50 characters")]
         public string RoleName { get; set; } // role_name (length: 255)
        public int? ProjectId { get; set; } // ProjectId

    }

}
