using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models
{
   public class RoleMenuAccess
    {
        public string RoleId { get; set; } 
        public int MenuId { get; set; } 
        public bool IsActive { get; set; } 
        public short AccessLevelId { get; set; } 

    }
}
