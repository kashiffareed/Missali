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
    public class hcp
    {

        public hcp()
        {
            Search = new SearchModel();
        }
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

        public string Longitude { get; set; } // Longitude (length: 500)
        public string Latitude { get; set; } // Latitude (length: 500)

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
        public string ClinicName { get; set; } // date_of_train
        public string PrivateHCP { get; set; } // date_of_train
        public int? ProjectId { get; set; } // ProjectId

        public System.DateTime? CreatedAt { get; set; }

        public IEnumerable<Hands.Data.HandsDB.HcpListingWithNamesReturnModel> HcpList { get; set; }

        public IList<Data.HandsDB.AppUser> AppUsers;
        public IEnumerable<Region> Regions { get; set; }
        public IEnumerable<AppUser> Marvis { get; set; }
        public SearchModel Search { get; set; }

        public string RefferredbyMarvi { get; set; } // RefferredbyMarvi (length: 255)
        public string DateofClientGeneration { get; set; } // DateofClientGeneration (length: 255)
        public string Occasion { get; set; } // Occasion (length: 255)
        public string Referral { get; set; } // Referral (length: 255)
        public string IncaseOfCurrentUserDateOfStarting { get; set; } // IncaseOfCurrentUserDateOfStarting (length: 500)
        public string HistoryOfIrregularMenstrualcycle { get; set; } // HistoryOfIrregularMenstrualcycle (length: 500)
        public string LmpDate { get; set; } // LmpDate (length: 255)
        public string Bp { get; set; } // BP (length: 255)
        public string Weight { get; set; } // weight (length: 255)
        public string Jaundice { get; set; } // jaundice (length: 255)
        public string PollarAnemia { get; set; } // PollarAnemia (length: 255)
        public string Foulsmellingvaginaldischarge { get; set; } // Foulsmellingvaginaldischarge (length: 255)
        public string Painlowerabdomen { get; set; } // Painlowerabdomen (length: 255)
        public string Pills { get; set; } // Pills (length: 255)
        public string Condoms { get; set; } // Condoms (length: 255)
        public string Injectables1Month { get; set; } // Injectables1month (length: 255)
        public string Injectables2Month { get; set; } // Injectables2month (length: 255)
        public string Injectables3Month { get; set; } // Injectables3month (length: 255)
        public string Iucd5Year { get; set; } // IUCD5year (length: 255)
        public string Iucd10Year { get; set; } // IUCD10year (length: 255)
        public string Implant { get; set; } // Implant (length: 255)
        public string Tl { get; set; } // TL (length: 255)


    }
}
