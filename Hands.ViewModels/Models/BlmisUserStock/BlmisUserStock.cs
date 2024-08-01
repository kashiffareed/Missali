using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.BlmisUserStock
{
    public  class BlmisUserStock
    {
        public int Id { get; set; } // users_stock_id (Primary key)
        public int ProductId { get; set; } // product_id

        public string ProductName { get; set; } 

        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; } // user_id

       [Required]
       [Range(1, (double)decimal.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; } // quantity

        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Price { get; set; } // Price
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public string Unit { get; set; }
        public string CategoryId { get; set; } // CategoryId (length: 50)
        public IEnumerable<Data.HandsDB.Product> ProductBlmisList { get; set; }
        public IEnumerable<Data.HandsDB.AppUser> UserList { get; set; }

        public int? LhvId { get; set; } // LhvId
 
        public int? ProjectId { get; set; } // ProjectId

    }

}
