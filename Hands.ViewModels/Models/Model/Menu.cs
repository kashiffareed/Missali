using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.Model
{

    public class Menu
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
        public IEnumerable<Data.HandsDB.SpGetRolesByProjectIdReturnModel> Roles { get; set; }

        public int RoleId { get; set; }

        public IEnumerable<Data.HandsDB.PushMessage> Messages{ get; set; }

        public IEnumerable<Data.HandsDB.AccessMenubyRoleIdReturnModel> Menus { get; set; }

        public IEnumerable<Data.HandsDB.ProjectSolution> ProjectList { get; set; }

        public int ProjectId { get; set; }
    }
}
