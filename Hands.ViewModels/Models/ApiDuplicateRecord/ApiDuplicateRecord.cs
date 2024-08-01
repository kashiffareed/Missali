using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.ApiDuplicateRecord
{
    public class ApiDuplicateRecord
    {
        public int Id { get; set; } // Id (Primary key)
        public string RequestUrl { get; set; } // RequestUrl (length: 255)
        public string JsonString { get; set; } // JsonString
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public System.DateTime UpdatedDate { get; set; } // UpdatedDate
        public bool IsActive { get; set; } // IsActive
    }
}
