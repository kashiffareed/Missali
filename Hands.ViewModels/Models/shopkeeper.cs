using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Hands.Data.HandsDB;
using Hands.ViewModels.Models.Search;

namespace Hands.ViewModels.Models
{
    public class shopkeeper
    {
        public shopkeeper()
        {
            Search = new SearchModel();
        }

        [Required]
        [StringLength(50, ErrorMessage = "Shop Name cannot be longer than 50 characters")]
        public string ShopName { get; set; } // full_name (length: 255)
     
        public string Longitude { get; set; } // Longitude (length: 500)
     
        public string Latitude { get; set; } // Latitude (length: 500)
    
        public int AppUserId { get; set; } // app_user_id (Primary key)

        [Required]
        [StringLength(50, ErrorMessage = "Full Name cannot be longer than 50 characters")]
        public string FullName { get; set; } // full_name (length: 255)

        public string FullNameUrdu { get; set; } // full_name_urdu (length: 255)

        public string FullNameSindhi { get; set; } // full_name_urdu (length: 255)

        public string Dob { get; set; } // dob (length: 50)

        [Required]
        public int? RegionId { get; set; } // region_id

        [Required]
        public int? TaluqaId { get; set; } // taluqa_id

        [Required]
        public int? UnionCouncilId { get; set; } // union_council_id

        [Required]
        [StringLength(50, ErrorMessage = "User Name cannot be longer than 50 characters")]
        public string Username { get; set; } // username (length: 255)

        [Required]
        [StringLength(50, ErrorMessage = "Password cannot be longer than 50 characters")]
        public string Pwd { get; set; } // pwd (length: 500

        public string PlainPassword { get; set; } // full_name_urdu (length: 255)

        [StringLength(200, ErrorMessage = "Password cannot be longer than 200 characters")]
        public string Address { get; set; } // address (length: 255)

        [StringLength(12, ErrorMessage = "Contact Number cannot be longer than 11 characters")]
        public string ContactNumber { get; set; } // contact_number (length: 15)

        public string MaritalStatus { get; set; } // marital_status (length: 255)

        [Required]
        [StringLength(100, ErrorMessage = "Father Husband Name cannot be longer than 100 characters")]
        public string FatherHusbandName { get; set; } // father_husband_name (length: 255)

        public int? AgePerCnic { get; set; } // age_per_cnic

        [DisplayName("CNIC")]
        [StringLength(13, ErrorMessage = " CNIC cannot be longer than 13 characters")]
        public string Cnic { get; set; } // cnic (length: 40)

        public string CnicValidtyEnd { get; set; } // cnic_validty_end (length: 50)

        public string Qualification { get; set; } // qualification (length: 255)

        public int LhvAssigned { get; set; } // Lhv_Assigned (length: 255)

        public int TotalMarviAssigned { get; set; } // Total_Marvi_Assigned (length: 255)

        public string UserType { get; set; } // User_Type (length: 255)

        public int? PopulcationCovered { get; set; } // populcation_covered

        public int? NoOfHouseHolds { get; set; } // no_of_house_holds

        public int? TargetMwras { get; set; } // target_mwras

        public string NearbyPublicFaculty { get; set; } // nearby_public_faculty

        public string NearbyPrivatefaculty { get; set; } // nearby_private_faculty


        public string DateOfJoin { get; set; } // date_of_join

        public string dateOfTrain { get; set; } // date_of_train

        public System.DateTime? CreatedAt { get; set; }

        public int? ProjectId { get; set; } // ProjectId
        public SearchModel Search { get; set; }

        public IEnumerable<Hands.Data.HandsDB.HcpListingWithNamesReturnModel> ShopkeeperList { get; set; }
        public IList<Data.HandsDB.AppUser> AppUsers;
        public IEnumerable<Region> Regions { get; set; }
        public IEnumerable<AppUser> Marvis { get; set; }


    }
}
