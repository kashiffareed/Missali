using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.CLMIS
{
    public class Clmis
    {
        public int StockId { get; set; } // stock_id (Primary key)
        public int ProductId { get; set; } // product_id
        [Required]
        public int Quantity { get; set; } // quantity
        [Required]

        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive



    }
}
