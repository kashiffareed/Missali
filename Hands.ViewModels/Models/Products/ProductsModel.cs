using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.Products
{
    public class ProductsModel
    {
        public int ProductId { get; set; } // product_id (Primary key)
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; } // product_name (length: 255)
      
        public string Generic { get; set; } // generic (length: 255)
        [Required(ErrorMessage = "Reg No is required")]
        public string RegNo { get; set; } // reg_no (length: 50)
        [Required(ErrorMessage = "Pack Size is required")]
        public string PackSize { get; set; } // pack_size (length: 100)
        public string Path { get; set; } // path
        public decimal TP { get; set; } // t_p
        [Required(ErrorMessage = "RP is required")]
        public decimal RP { get; set; } // r_p
        public int ClientId { get; set; } // client_id

        public List<Category> Categories { get; set; }
        public string Producttype { get; set; } // Producttype (length: 150Category
        [Required(ErrorMessage = "Product Category is required")]
        public string ProductCategory { get; set; } // ProductCategory (length: 250)
        [Required(ErrorMessage = "Brand Name is required")]
        public string BrandName { get; set; } // BrandName (length: 250)
        [Required(ErrorMessage = "Measurement Unit is required")]
        public string MeasurementUnit { get; set; } // MeasurementUnit (length: 150)
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
    }
}
