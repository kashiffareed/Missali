using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.UserAccessMenuModel
{
 public  class UserAccessMenu
    {
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
        public IEnumerable<Data.HandsDB.AspNetUser> Users { get; set; }

        public int UserId { get; set; }
    }
}
