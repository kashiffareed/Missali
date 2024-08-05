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
using Hands.ViewModels.Models.Search;

namespace Hands.ViewModels.Models
{
    public class Mwra
    {
        public Mwra()
        {
            Search = new SearchModel();
        }

        public int MwraId { get; set; } // mwra_id (Primary key)\
        [DisplayName("Full Name")]

        [Required]
        [StringLength(50, ErrorMessage = "User Name cannot be longer than 50 characters")]
        public string Name { get; set; } // name (length: 255)

        [DisplayName("D.O.B")]
        //[Required]
        public string Dob { get; set; } // dob (length: 50)

        [DisplayName("Address")]
        [Required]
        public string Address { get; set; } // address (length: 255)

        [DisplayName("Contact Number")]
        //[Required]
        public string ContactNumber { get; set; } // contact_number (length: 25)

        [DisplayName("Marvi Assigned")]
        [Required]
        public int AssignedMarviId { get; set; } // assigned_marvi_id

        public string Longitude { get; set; } // longitude (length: 20)
        public string Latitude { get; set; } // latitude (length: 20)

        [DisplayName("Husband Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Husband Name cannot be longer than 50 characters")]
        public string HusbandName { get; set; } // husband_name (length: 255)

        [DisplayName("CNIC")]
        //[Required]
        [StringLength(13, ErrorMessage = " CNIC cannot be longer than 13 characters")]
        public string Cnic { get; set; } // cnic (length: 50)

        //[Required]
        public string MaritialStatus { get; set; } // maritial_status (length: 100)

        [DisplayName("Age")]
        //[Required]
        public int? Age { get; set; } // age

        [DisplayName("Occupation")]
        //[Required]
        public string Occupation { get; set; } // occupation (length: 255)

        public short? IsClient { get; set; } // is_client
        public System.DateTime? CreatedAt { get; set; } // created_at

        [DisplayName("District")]
        [Required]
        public int? RegionId { get; set; } // region_id

        [DisplayName("Taluqa")]
        [Required]
        public int? TaluqaId { get; set; } // taluqa_id

        [DisplayName("Union Council")]
        [Required]
        public int? UnionCouncilId { get; set; } // union_council_id

        //[Required]
        public string EducationOfClass { get; set; } // education_of_class (length: 10)

        //[Required]
        public string DurationOfMarriage { get; set; } // duration_of_marriage (length: 10)
        
        //[Required]
        public string CrrentlyPregnant { get; set; } // crrently_pregnant (length: 10)
       
        // [Required]
        public string PregnantNoOfMonths { get; set; } // pregnant_no_of_months (length: 10)

        //[Required]
        public string NoOfAliveChildren { get; set; } // no_of_alive_children (length: 10)

        //[Required]
        public string NoOfAbortion { get; set; } // no_of_abortion (length: 10)

        //[Required]
        public string NoOfChildrenDied { get; set; } // no_of_children_died (length: 10)

        //[Required]
        public string ReasonOfDeath { get; set; } // reason_of_death (length: 255)

        //[Required]
        public string AgeOfYoungestChildYears { get; set; } // age_of_youngest_child_years (length: 10)

        //[Required]
        public string AgeOfYoungestChildMonths { get; set; } // age_of_youngest_child_months (length: 10)
       
        // [Required]
        public string HaveUsedFpMethod { get; set; } // have_used_fp_method (length: 10)

        // [Required]
        public string FpMethodUsed { get; set; } // fp_method_used (length: 255)
        
        // [Required]
        public string FpNotUsedInYears { get; set; } // fp_not_used_in_years (length: 10)
        
        // [Required]
        public string ReasonOfDiscontinuation { get; set; } // reason_of_discontinuation (length: 255)
       
        // [Required]
        public string FpNoReason { get; set; } // fp_no_reason (length: 255)

        [Required]
        public string WantToUseFp { get; set; } // want_to_use_fp (length: 10)
        
        // [Required]
        public string FpPurpose { get; set; } // fp_purpose (length: 255)
        
        // [Required]
        public string IsUserFp { get; set; } // is_user_fp (length: 10)

        // [Required]
        public string NameOfFp { get; set; } // name_of_fp (length: 255)

        public int? ProjectId { get; set; } // is_user_fp (length: 10)

        [Required]
        public string DateOfRegistration { get; set; } // date_of_registration (length: 50)


        public DateTime? DeviceCreatedDate { get; set; }

        public IEnumerable<Data.HandsDB.Mwra> MwraList;
        public IEnumerable<Data.HandsDB.SpMwrasListingReturnModel> MwraListt { get; set; }
        public IEnumerable<Data.HandsDB.SpMwraClientListingNewReturnModel> MwrasClientListt { get; set; }
        public IEnumerable<Data.HandsDB.SpNewUserClientListingReturnModel> NewUserClientList { get; set; }
        public IEnumerable<Data.HandsDB.SpContinuedUserClientListingReturnModel> ContinuedUserClientList { get; set; }

        public IEnumerable<Data.HandsDB.SpMwraDropOutClientListingReturnModel> DropOutClientListing { get; set; }

        public IEnumerable<Region> Regions { get; set; }
        public IEnumerable<AppUser> Marvis { get; set; }
        public SearchModel Search { get; set; }
        public Data.HandsDB.Mwra GetMwraEntity()
        {
            Data.HandsDB.Mwra mwra = new Data.HandsDB.Mwra();

            mwra.AssignedMarviId = AssignedMarviId;
            mwra.Name = Name;
            mwra.Dob = Dob;
            mwra.Address = Address;
            mwra.ContactNumber = ContactNumber;
            mwra.MaritialStatus = "";
            mwra.Cnic = Cnic;
            mwra.HusbandName = HusbandName;
            mwra.Age = Age;
            mwra.Occupation = Occupation;
            mwra.RegionId = RegionId.Value;
            mwra.TaluqaId = TaluqaId.Value;
            mwra.UnionCouncilId = UnionCouncilId.Value;
            mwra.CreatedAt = DateTime.Now;
            mwra.Latitude = Latitude;
            mwra.Longitude = Longitude;
            mwra.IsClient = 0;
            mwra.EducationOfClass = EducationOfClass;
            mwra.DurationOfMarriage = DurationOfMarriage;
            mwra.CrrentlyPregnant = CrrentlyPregnant;
            mwra.PregnantNoOfMonths = PregnantNoOfMonths;
            mwra.NoOfAliveChildren = NoOfAliveChildren;
            mwra.NoOfAbortion = NoOfAbortion;
            mwra.NoOfChildrenDied = NoOfChildrenDied;
            mwra.ReasonOfDeath = ReasonOfDeath;
            mwra.AgeOfYoungestChildMonths = AgeOfYoungestChildMonths;
            mwra.AgeOfYoungestChildYears = AgeOfYoungestChildYears;
            mwra.HaveUsedFpMethod = HaveUsedFpMethod;
            mwra.NameOfFp = NameOfFp;
            mwra.FpNotUsedInYears = FpNotUsedInYears;
            mwra.ReasonOfDiscontinuation = ReasonOfDiscontinuation;
            mwra.FpNoReason = FpNoReason;
            mwra.WantToUseFp = WantToUseFp;
            mwra.FpPurpose = FpPurpose;
            mwra.IsUserFp = IsUserFp;
            if (FpMethodUsed != null)
            {
                mwra.FpMethodUsed = FpMethodUsed.Replace("\n", " ");
            }
            else
            {
                mwra.FpMethodUsed = FpMethodUsed;
            }
            mwra.DateOfRegistration = DateOfRegistration;
            mwra.IsActive = true;
            return mwra;

        }
        public void UpdateAge()
        {
            if (DateTime.TryParseExact(Dob, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDob))
            {
                Age = CalculateAge(parsedDob);
            }
            else
            {
                Age = null;
            }
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            var age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }
    }
}
