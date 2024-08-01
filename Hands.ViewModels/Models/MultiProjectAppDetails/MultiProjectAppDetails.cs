using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Hands.ViewModels.Models.MultiProjectAppDetails
{
    public class MultiProjectAppDetails
    {
        public int Id { get; set; } // Id (Primary key)
        public int ProjectId { get; set; } // ProjectId
        public string PrimaryColor { get; set; } // PrimaryColor (length: 255)
        public string SecoundaryColor { get; set; } // SecoundaryColor (length: 255)
        public string HeadingColor { get; set; } // HeadingColor (length: 255)
        public string SubHeadingColor { get; set; } // SubHeadingColor (length: 255)
        public string ProjectImagePath { get; set; } // ProjectImagePath (length: 255)
        public bool IsActive { get; set; } // isActive
    }
}
