using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.ProjectAcessMenuModel
{
    public class ProjectAcecssMenu
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
        public IEnumerable<Data.HandsDB.ProjectSolution> Projects { get; set; }

        public int ProjectId { get; set; }
    }
}
