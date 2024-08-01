using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.Miscellenou
{
 public   class Miscellenou
    {
        public int? ProjectId { get; set; } // ProjectId

        public int Id { get; set; } // Id (Primary key)
        public int ReturnId { get; set; }

        [Required]
        public int ProductId { get; set; } // ProductId
        public int RegionId { get; set; } // Region

        [Required]
        public int Quantity { get; set; } // Quantity

        [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters")]
        public string Description { get; set; } // Description (length: 255)

        [Required]
        public int? UserId { get; set; } // LhvId

        [Required]
        public string UserType { get; set; } // UserType

        [Required]
        public int? ReturnType { get; set; } // ReturnType

        public System.DateTime? CreatedAt { get; set; } // created_at

        public bool IsActive { get; set; } // IsActive

        public IEnumerable<Data.HandsDB.Product> ProductList { get; set; }
        public IEnumerable<AppUser> LhvList { get; set; }
        public IEnumerable<Data.HandsDB.Region> RegionList { get; set; }
        public List<GetUsersByUserTypeReturnModel> UserByUserType { get; set; }

        public List<GetProductByUserIdReturnModel> ProductByUserId { get; set; }

        //public IEnumerable<GetQuantityByUserIdAndProductIdReturnModel> QuantityByUserIdAndProductId { get; set; }

    }
}
