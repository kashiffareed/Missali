using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using Hands.Data.HandsDB;


namespace Hands.ViewModels.Models
{
    public class region
    {
        public int RegionsId { get; set; } // regions_id (Primary key)
        [Required]
        [StringLength(50, ErrorMessage = "Region Name cannot be longer than 50 characters")]
        public string RegionName { get; set; } // region_name (length: 255)
        public int? ClientId { get; set; } // client_id

        public System.DateTime CreatedAt { get; set; } // created_at
    }
}
