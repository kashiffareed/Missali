using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.Product
{
 public   class Product
    {
        public int StockId { get; set; } // stock_id (Primary key)
        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; } // product_id
        
        [Required]
        public int Quantity { get; set; } // quantity
        [Required]
        [StringLength(250, ErrorMessage = "Notes cannot be longer than 250 characters")]
        public string Notes { get; set; } // notes
        public decimal? Price { get; set; } // Price
        public DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public int RegionId { get; set; } // product_id
        public IEnumerable<Data.HandsDB.Product> ProductsList { get; set; }
        public IEnumerable<Data.HandsDB.Region> RegionList { get; set; }
    }
}
