using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.Pms
{
    public  class Pms
    {
        public int CategoryId { get; set; } // category_id (Primary key)
        public int ContentId { get; set; } // content_id (Primary key)
     
        [StringLength(50, ErrorMessage = "ContentName Sindhi cannot be longer than 30 characters")]
        public string ContentName { get; set; } // content_name (length: 255)
        
        [StringLength(50, ErrorMessage = "ContentNameSindhi cannot be longer than 30 characters")]
        public string ContentNameSindhi { get; set; } // content_name_sindhi (length: 255)
      
        [StringLength(50, ErrorMessage = "ContentNameUrdu cannot be longer than 30 characters")]
        public string ContentNameUrdu { get; set; } // content_name_urdu (length: 255)
       
        public string Path { get; set; } // path (length: 255)
        
        public string PathSindhi { get; set; } // path_sindhi (length: 255)
       
        public string PathUrdu { get; set; } // path_urdu (length: 255)
        [Required]
        public string ContentType { get; set; } // content_type (length: 100)
        [Required]
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public IEnumerable<Data.HandsDB.Category> CategoryList { get; set; }
        public IEnumerable<Data.HandsDB.Pm> ContentTypeList { get; set; }


    }
}
