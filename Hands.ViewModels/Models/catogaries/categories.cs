using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.catogaries
{
    public  class categories
    {
        public int CategoryId { get; set; } // category_id (Primary key)
        [Required]
        [StringLength(50, ErrorMessage = "Category Name cannot be longer than 30 characters")]
        public string CategoryName { get; set; } // category_name (length: 255)
        [Required]
        [StringLength(50, ErrorMessage = "Category Name Sindhi cannot be longer than 30 characters")]
        public string CategoryNameSindhi { get; set; } // category_name_sindhi (length: 255)
        [Required]
        [StringLength(50, ErrorMessage = "Category Name Urdu cannot be longer than 30 characters")]
        public string CategoryNameUrdu { get; set; } // category_name_urdu (length: 255)
        [Required]

        public System.DateTime CreatedAt { get; set; } // created_at


    }
}
