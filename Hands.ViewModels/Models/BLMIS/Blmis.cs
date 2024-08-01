using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.BLMIS
{
    public class Blmis
    {
        public int Id { get; set; } // stock_id (Primary key)
        [Required]
        public int ProductId { get; set; } // product_id
        [Required]
        public int Quantity { get; set; } // quantity
        [Required]
        public int UserId { get; set; } // userId
        [Required]
        public int Price { get; set; } // price


        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive



    }
}
