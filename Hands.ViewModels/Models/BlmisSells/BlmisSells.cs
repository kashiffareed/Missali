using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hands.ViewModels.Models.BlmisSells
{
    public class BlmisSells
    {
        public int Id { get; set; } // stock_id (Primary key)

        [Required]
        public string SellDate { get; set; } // SellDate

       [Required]
       [Range(1, (double)decimal.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive

        [Required]
        public int? UserId { get; set; }
        public int? ProductId { get; set; } // ProductId

        public string YesterdayDate { get; set; } // SellDate
        public decimal? DayWiseAmount { get; set; }

        public IEnumerable<Data.HandsDB.AppUser> UserList { get; set; }



        public int? ProjectId { get; set; } // ProjectId
    }
}
