using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hands.Data.HandsDB;

namespace Hands.ViewModels.Models.ShopkeeperLocation
{
    public class ShopkeeperLocation
    { 
        public string Longitude { get; set; } // longitude (length: 20)
        public string Latitude { get; set; } // latitude (length: 20)
        public string UserType { get; set; } // User_Type (length: 255)

        public List<Hands.Data.HandsDB.AppUser> AppUsers { get; set; }

    }

}

