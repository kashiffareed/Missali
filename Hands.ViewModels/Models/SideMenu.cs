using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models
{
   public class SideMenu
    {
        public int Id { get; set; }

        public string Tittle { get; set; }
        
        [Required]
        public int ParentId { get; set; } 

        public string Controller { get; set; }

        public string Action { get; set; }

    }
}
