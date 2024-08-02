using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hands.Common.Common;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.Regions;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;

namespace Hands.WebAPI.Models
{
    public class AppUser
    {
        public string PrimaryColor { get; set; } // PrimaryColor (length: 255)
        public string SecoundaryColor { get; set; } // SecoundaryColor (length: 255)
        public string HeadingColor { get; set; } // HeadingColor (length: 255)
        public string SubHeadingColor { get; set; } // SubHeadingColor (length: 255)
        public int? ProjectId { get; set; } // ProjectId
        public int AppUserId { get; set; } // app_user_id (Primary key)
        public string FullName { get; set; } // full_name (length: 255)
        public string FullNameUrdu { get; set; } // full_name_urdu (length: 255)
        public string FullNameSindhi { get; set; } // full_name_sindhi (length: 255)
        public string Dob { get; set; } // dob (length: 50)
        public int? RegionId { get; set; } // region_id
        public int? TaluqaId { get; set; } // taluqa_id

        public string RegionName { get; set; }
        public string TaluqaName { get; set; }
        public int? UnionCouncilId { get; set; } // union_council_id
        public string Username { get; set; } // username (length: 255)
        public string Pwd { get; set; } // pwd (length: 500)
        public string PlainPassword { get; set; } // plain_password (length: 500)
        public string Address { get; set; } // address (length: 255)
        public string ContactNumber { get; set; } // contact_number (length: 15)
        public string MaritalStatus { get; set; } // marital_status (length: 255)
        public string FatherHusbandName { get; set; } // father_husband_name (length: 255)
        public int? AgePerCnic { get; set; } // age_per_cnic
        public string UnionCouncilName { get; set; }

        public string Cnic { get; set; } // cnic (length: 40)
        public string CnicValidtyEnd { get; set; } // cnic_validty_end (length: 50)
        public string Qualification { get; set; } // qualification (length: 255)
        public int? LhvAssigned { get; set; } // lhv_assigned
        public int? TotalMarviAssigned { get; set; } // total_marvi_assigned
        public string UserType { get; set; } // user_type (length: 255)
        public int? PopulcationCovered { get; set; } // populcation_covered
        public int? NoOfHouseHolds { get; set; } // no_of_house_holds
        public int? TargetMwras { get; set; } // target_mwras
        public string NearbyPublicFaculty { get; set; } // nearby_public_faculty (length: 255)
        public string NearbyPrivateFaculty { get; set; } // nearby_private_faculty (length: 255)
        public string DateOfJoin { get; set; } // date_of_join (length: 50)
        public string DateOfTrain { get; set; } // date_of_train (length: 50)
        public string CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public string CurrentStatusOfService { get; set; } // currentStatusOfService (length: 500)
        public string lhvAssignedName { get; set; }
        public AppUser GetMapAppUser(Data.HandsDB.AppUser AppUser, IRegionService regionService, ITaluqaService taluqaService, IUnionCouncilService unionCouncilService, IAppUserService _appUserService)
        {
            this.ProjectId = AppUser.ProjectId;
            this.AppUserId = AppUser.AppUserId;
            this.FullName = AppUser.FullName;
            if (FullName == "")
                FullName = AppUser.FullNameUrdu;
            if (FullName == "")
                FullName = AppUser.FullNameSindhi;
            //  this.FullNameUrdu = AppUser.FullNameUrdu;
            // this.FullNameSindhi = AppUser.FullNameSindhi;
            this.Dob = !string.IsNullOrEmpty(AppUser.Dob) ? AppUser.Dob : "N/A";
            this.RegionId = AppUser.RegionId;
            this.TaluqaId = AppUser.TaluqaId;
            this.UnionCouncilId = AppUser.UnionCouncilId;
            this.Username = !string.IsNullOrEmpty(AppUser.Username) ? AppUser.Username : "N/A";
            this.PlainPassword = !string.IsNullOrEmpty(AppUser.PlainPassword) ? AppUser.PlainPassword : "N/A";
            this.Address = !string.IsNullOrEmpty(AppUser.Address) ? AppUser.Address : "N/A";
            this.ContactNumber = AppUser.ContactNumber == "0" || AppUser.ContactNumber == "" ? "N/A" : AppUser.ContactNumber;
            this.MaritalStatus = !string.IsNullOrEmpty(AppUser.MaritalStatus) ? AppUser.MaritalStatus : "N/A";
            this.FatherHusbandName = !string.IsNullOrEmpty(AppUser.FatherHusbandName) ? AppUser.FatherHusbandName : "N/A";
            this.AgePerCnic = AppUser.AgePerCnic;
            this.Cnic = AppUser.Cnic == "0" || AppUser.Cnic == "" ? "N/A" : AppUser.Cnic;
            this.CnicValidtyEnd = !string.IsNullOrEmpty(AppUser.CnicValidtyEnd) ? AppUser.CnicValidtyEnd : "N/A";
            this.Qualification = !string.IsNullOrEmpty(AppUser.Qualification) ? AppUser.Qualification : "N/A";
            this.UserType = !string.IsNullOrEmpty(AppUser.UserType) ? AppUser.UserType : "N/A";
            this.PopulcationCovered = AppUser.PopulcationCovered;
            this.NoOfHouseHolds = AppUser.NoOfHouseHolds;
            this.TotalMarviAssigned = AppUser.TotalMarviAssigned ?? null;
            this.TargetMwras = AppUser.TargetMwras;
            this.LhvAssigned = AppUser.LhvAssigned ?? null;
            this.lhvAssignedName = AppUser.LhvAssigned != null ? _appUserService.GetNameById(AppUser.LhvAssigned.Value) : "N/A";
            this.NearbyPublicFaculty = !string.IsNullOrEmpty(AppUser.NearbyPublicFaculty) ? AppUser.NearbyPublicFaculty : "N/A";
            this.NearbyPrivateFaculty = !string.IsNullOrEmpty(AppUser.NearbyPrivateFaculty) ? AppUser.NearbyPrivateFaculty : "N/A";
            this.DateOfJoin = !string.IsNullOrEmpty(AppUser.DateOfJoin) ? AppUser.DateOfJoin : "N/A";
            this.DateOfTrain = !string.IsNullOrEmpty(AppUser.DateOfTrain) ? AppUser.DateOfTrain : "N/A";
            this.CreatedAt = AppUser.CreatedAt.ToString();
            this.IsActive = AppUser.IsActive;
            CurrentStatusOfService = AppUser.CurrentStatusOfService;
            this.RegionName = regionService.GetNameById(RegionId);
            this.TaluqaName = taluqaService.GetNameById(TaluqaId);
            this.UnionCouncilName = AppUser.UnionCouncilId != null ? unionCouncilService.GetNameById(AppUser.UnionCouncilId.Value) : "N/A";
            return this;
        }



        public List<AppUser> PrepareViewList(IEnumerable<Data.HandsDB.AppUser> appUser, IRegionService regionService, ITaluqaService taluqaService, IUnionCouncilService unionCouncilService, IAppUserService _appUserService)
        {
            return appUser.Select(x => new AppUser().GetMapAppUser(x, regionService, taluqaService, unionCouncilService, _appUserService)).ToList();
        }
    }
}