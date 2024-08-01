using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.AssignMenuToApp
{
   public class AssignMenuToAppViewModel
    {

        [Required]
        public string ProjectName { get; set; }
       
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
        public string Tittle { get; set; } // Tittle (length: 255)
        public string ClickEvent { get; set; } // clickEvent (length: 255)
        public int ParentId { get; set; } // ParentId
        public bool IsActive { get; set; } // IsActive
        public string PrimaryColor { get; set; } // PrimaryColor (length: 255)
        public string SecoundaryColor { get; set; } // SecoundaryColor (length: 255)
        public string HeadingColor { get; set; } // HeadingColor (length: 255)
        public string SubHeadingColor { get; set; } // SubHeadingColor (length: 255)
        public string ProjectImagePath { get; set; } // ProjectImagePath (length: 255)

        public IEnumerable<Data.HandsDB.MultiProjectAppDetail> MultiProjectAppDetailsList { get; set; }

      
      
    }
}
