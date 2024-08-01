using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.UserStock
{
    public  class userStock
    {
        public int UsersStockId { get; set; } // users_stock_id (Primary key)
        public int ProductId { get; set; } // product_id
        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; } // user_id
       [Required]
       public int Quantity { get; set; } // quantity
        public int RegionId { get; set; } // Region

        public decimal? Price { get; set; } // Price
        [Required]
        public string userType { get; set; } // quantity
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public IEnumerable<Data.HandsDB.Product> ProductsList { get; set; }
        public IEnumerable<Data.HandsDB.Region> RegionList { get; set; }
      
        public IEnumerable<Data.HandsDB.SpTotalClmisHandStockReturnModel> ProductClmisList { get; set; }
        public IEnumerable<Data.HandsDB.AppUser> HlvList { get; set; }
        public IEnumerable<Data.HandsDB.SpStockListReturnModel> StockList;

        public int? ProjectId { get; set; } // ProjectId
    }
}
