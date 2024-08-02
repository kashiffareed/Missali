using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hands.Data.HandsDB;
using Hands.Service.AppUser;
using Hands.Service.MwraClientNew;
using Hands.Service.Regions;
using Hands.Service.Taluqa;
using Hands.Service.UnionCouncil;

namespace Hands.WebAPI.mwras
{
    public class Mwra
    {
        public int MwraId { get; set; } // mwra_id (Primary key)
        public string Name { get; set; } // name (length: 255)
        public string Dob { get; set; } // dob (length: 50)
        public string Address { get; set; } // address (length: 255)
        public string ContactNumber { get; set; } // contact_number (length: 25)
        public string AssignedMarviName { get; set; } // assigned_marvi_id
        public int AssignedMarviId { get; set; } // assigned_marvi_id
        public string Longitude { get; set; } // longitude (length: 20)
        public string Latitude { get; set; } // latitude (length: 20)
        public string HusbandName { get; set; } // husband_name (length: 255)
        public string Cnic { get; set; } // cnic (length: 50)
        public string MaritialStatus { get; set; } // maritial_status (length: 100)
        public int? Age { get; set; } // age
        public string Occupation { get; set; } // occupation (length: 255)
        public short? IsClient { get; set; } // is_client
        public string CreatedAt { get; set; } // created_at
        public int? RegionId { get; set; } // region_id
        public int? TaluqaId { get; set; } // taluqa_id
        public int? UnionCouncilId { get; set; } // union_council_id
        public string RegionName { get; set; } // region_id
        public string TaluqaName { get; set; } // taluqa_id
        public string UnionCouncilName { get; set; } // union_council_id
        public string EducationOfClass { get; set; } // education_of_class (length: 10)
        public string DurationOfMarriage { get; set; } // duration_of_marriage (length: 10)
        public string CrrentlyPregnant { get; set; } // crrently_pregnant (length: 10)
        public string PregnantNoOfMonths { get; set; } // pregnant_no_of_months (length: 10)
        public string NoOfAliveChildren { get; set; } // no_of_alive_children (length: 10)
        public string NoOfAbortion { get; set; } // no_of_abortion (length: 10)
        public string NoOfChildrenDied { get; set; } // no_of_children_died (length: 10)
        public string ReasonOfDeath { get; set; } // reason_of_death (length: 255)
        public string AgeOfYoungestChildYears { get; set; } // age_of_youngest_child_years (length: 10)
        public string AgeOfYoungestChildMonths { get; set; } // age_of_youngest_child_months (length: 10)
        public string HaveUsedFpMethod { get; set; } // have_used_fp_method (length: 10)
        public string NameOfFp { get; set; } // name_of_fp (length: 255)
        public string FpNotUsedInYears { get; set; } // fp_not_used_in_years (length: 10)
        public string ReasonOfDiscontinuation { get; set; } // reason_of_discontinuation (length: 255)
        public string FpNoReason { get; set; } // fp_no_reason (length: 255)
        public string WantToUseFp { get; set; } // want_to_use_fp (length: 10)
        public string FpPurpose { get; set; } // fp_purpose (length: 255)
        public string IsUserFp { get; set; } // is_user_fp (length: 10)
        public string FpMethodUsed { get; set; } // fp_method_used (length: 255)
        public string DateOfRegistration { get; set; } // date_of_registration (length: 50)
        public bool IsActive { get; set; } // IsActive

        public string taluqaName { get; set; } // taluqa_id

        public List<GetMwraClienNametByMwraIdReturnModel> mwraClient { get; set; }

        public Mwra GetMapMwra(Data.HandsDB.Mwra mwra, IAppUserService appService, IRegionService regionService, ITaluqaService taluqaService, IUnionCouncilService unionCouncilService, IMwraClientNewService clientNewService, bool isclientData = false)
        {
            this.MwraId = mwra.MwraId;
            this.AssignedMarviId = mwra.AssignedMarviId;
            this.AssignedMarviName = appService.GetNameById(mwra.AssignedMarviId);
            this.Name = !string.IsNullOrEmpty(mwra.Name) ? mwra.Name : "N/A";
            this.Dob = !string.IsNullOrEmpty(mwra.Dob) ? Convert.ToDateTime(mwra.Dob).ToString("MM/dd/yyyy") : "N/A";
            this.Address = !string.IsNullOrEmpty(mwra.Address) ? mwra.Address : "N/A";
            this.ContactNumber = mwra.ContactNumber == "0" || mwra.ContactNumber == "" ? "N/A" : mwra.ContactNumber;
            this.MaritialStatus = !string.IsNullOrEmpty(mwra.MaritialStatus) ? mwra.MaritialStatus : "N/A";
            this.Cnic = mwra.Cnic == "0" || mwra.Cnic == "" ? "N/A" : mwra.Cnic;
            this.HusbandName = !string.IsNullOrEmpty(mwra.HusbandName) ? mwra.HusbandName : "N/A";
            this.Age = mwra.Age;
            this.Occupation = !string.IsNullOrEmpty(mwra.Occupation) ? mwra.Occupation : "N/A";
            this.RegionId = mwra.RegionId;
            this.TaluqaId = mwra.TaluqaId;
            this.UnionCouncilId = mwra.UnionCouncilId;
            this.RegionName = regionService.GetNameById(mwra.RegionId);
            this.TaluqaName = taluqaService.GetNameById(mwra.TaluqaId);
            this.UnionCouncilName = mwra.UnionCouncilId != null
                ? unionCouncilService.GetNameById(mwra.UnionCouncilId.Value)
                : "";

            this.Latitude = !string.IsNullOrEmpty(mwra.Latitude) ? mwra.Latitude : "N/A";
            this.Longitude = !string.IsNullOrEmpty(mwra.Longitude) ? mwra.Longitude : "N/A";
            this.IsClient = mwra.IsClient;
            this.EducationOfClass = !string.IsNullOrEmpty(mwra.EducationOfClass) ? mwra.EducationOfClass : "N/A";
            this.DurationOfMarriage = !string.IsNullOrEmpty(mwra.DurationOfMarriage) ? mwra.DurationOfMarriage : "N/A";
            this.CrrentlyPregnant = !string.IsNullOrEmpty(mwra.CrrentlyPregnant) ? mwra.CrrentlyPregnant : "N/A";
            this.PregnantNoOfMonths = !string.IsNullOrEmpty(mwra.PregnantNoOfMonths) ? mwra.PregnantNoOfMonths : "N/A";
            this.NoOfAliveChildren = !string.IsNullOrEmpty(mwra.NoOfAliveChildren) ? mwra.NoOfAliveChildren : "N/A";
            this.NoOfAbortion = !string.IsNullOrEmpty(mwra.NoOfAbortion) ? mwra.NoOfAbortion : "N/A";
            this.NoOfChildrenDied = !string.IsNullOrEmpty(mwra.NoOfChildrenDied) ? mwra.NoOfChildrenDied : "N/A";
            this.ReasonOfDeath = !string.IsNullOrEmpty(mwra.ReasonOfDeath) ? mwra.ReasonOfDeath : "N/A";
            this.AgeOfYoungestChildMonths = !string.IsNullOrEmpty(mwra.AgeOfYoungestChildMonths) ? mwra.AgeOfYoungestChildMonths : "N/A";
            this.AgeOfYoungestChildYears = !string.IsNullOrEmpty(mwra.AgeOfYoungestChildYears) ? mwra.AgeOfYoungestChildYears : "N/A";
            this.HaveUsedFpMethod = !string.IsNullOrEmpty(mwra.HaveUsedFpMethod) ? mwra.HaveUsedFpMethod : "N/A";
            this.NameOfFp = !string.IsNullOrEmpty(mwra.NameOfFp) ? mwra.NameOfFp : "N/A";
            this.FpNotUsedInYears = !string.IsNullOrEmpty(mwra.FpNotUsedInYears) ? mwra.FpNotUsedInYears : "N/A";
            this.ReasonOfDiscontinuation = !string.IsNullOrEmpty(mwra.ReasonOfDiscontinuation) ? mwra.ReasonOfDiscontinuation : "N/A";
            this.FpNoReason = !string.IsNullOrEmpty(mwra.FpNoReason) ? mwra.FpNoReason : "N/A";
            this.WantToUseFp = !string.IsNullOrEmpty(mwra.WantToUseFp) ? mwra.WantToUseFp : "N/A";
            this.FpPurpose = !string.IsNullOrEmpty(mwra.FpPurpose) ? mwra.FpPurpose : "N/A";
            this.IsUserFp = !string.IsNullOrEmpty(mwra.IsUserFp) ? mwra.IsUserFp : "N/A";
            this.FpMethodUsed = !string.IsNullOrEmpty(mwra.FpMethodUsed) ? mwra.FpMethodUsed : "N/A";
            this.DateOfRegistration = !string.IsNullOrEmpty(mwra.DateOfRegistration) ? mwra.DateOfRegistration : "N/A";
            this.IsActive = mwra.IsActive;
            this.CreatedAt = mwra.CreatedAt.ToString();
            if (isclientData)
            {
                this.mwraClient = clientNewService.GetMwraClientByMwraId(mwra.MwraId);
            }

            return this;
        }


        private Mwra GetMapMwraWithclient(Data.HandsDB.GetAllMwraWithRelationNamesReturnModel mwra, IMwraClientNewService clientNewService)
        {
            this.MwraId = mwra.mwra_id;
            this.AssignedMarviId = mwra.assigned_marvi_id;
            this.AssignedMarviName = !string.IsNullOrEmpty(mwra.marviName) ? mwra.marviName : "N/A";
            this.Name = !string.IsNullOrEmpty(mwra.name) ? mwra.name : "N/A";
            this.Dob = !string.IsNullOrEmpty(mwra.dob) ? Convert.ToDateTime(mwra.dob).ToString("MM/dd/yyyy") : "N/A";
            this.Address = !string.IsNullOrEmpty(mwra.address) ? mwra.address : "N/A";
            this.ContactNumber = mwra.contact_number == "0" || mwra.contact_number == "" ? "N/A" : mwra.contact_number;
            this.MaritialStatus = !string.IsNullOrEmpty(mwra.maritial_status) ? mwra.maritial_status : "N/A";
            this.Cnic = mwra.cnic == "0" || mwra.cnic == "" ? "N/A" : mwra.cnic;
            this.HusbandName = !string.IsNullOrEmpty(mwra.husband_name) ? mwra.husband_name : "N/A";
            this.Age = mwra.age;
            this.Occupation = !string.IsNullOrEmpty(mwra.occupation) ? mwra.occupation : "N/A";
            this.RegionId = mwra.region_id;
            this.TaluqaId = mwra.taluqa_id;
            this.UnionCouncilId = mwra.union_council_id;
            this.RegionName = !string.IsNullOrEmpty(mwra.region_name) ? mwra.region_name : "N/A";
            this.TaluqaName = !string.IsNullOrEmpty(mwra.taluqa_name) ? mwra.taluqa_name : "N/A";
            this.taluqaName= !string.IsNullOrEmpty(mwra.taluqa_name) ? mwra.taluqa_name : "N/A";
            this.UnionCouncilName = !string.IsNullOrEmpty(mwra.union_council_name) ? mwra.union_council_name : "N/A";
            this.Latitude = !string.IsNullOrEmpty(mwra.latitude) ? mwra.latitude : "N/A";
            this.Longitude = !string.IsNullOrEmpty(mwra.longitude) ? mwra.longitude : "N/A";
            this.IsClient = mwra.is_client;
            this.EducationOfClass = !string.IsNullOrEmpty(mwra.education_of_class) ? mwra.education_of_class : "N/A";
            this.DurationOfMarriage = !string.IsNullOrEmpty(mwra.duration_of_marriage) ? mwra.duration_of_marriage : "N/A";
            this.CrrentlyPregnant = !string.IsNullOrEmpty(mwra.crrently_pregnant) ? mwra.crrently_pregnant : "N/A";
            this.PregnantNoOfMonths = !string.IsNullOrEmpty(mwra.pregnant_no_of_months) ? mwra.pregnant_no_of_months : "N/A";
            this.NoOfAliveChildren = !string.IsNullOrEmpty(mwra.no_of_alive_children) ? mwra.no_of_alive_children : "N/A";
            this.NoOfAbortion = !string.IsNullOrEmpty(mwra.no_of_abortion) ? mwra.no_of_abortion : "N/A";
            this.NoOfChildrenDied = !string.IsNullOrEmpty(mwra.no_of_children_died) ? mwra.no_of_children_died : "N/A";
            this.ReasonOfDeath = !string.IsNullOrEmpty(mwra.reason_of_death) ? mwra.reason_of_death : "N/A";
            this.AgeOfYoungestChildMonths = !string.IsNullOrEmpty(mwra.age_of_youngest_child_months) ? mwra.age_of_youngest_child_months : "N/A";
            this.AgeOfYoungestChildYears = !string.IsNullOrEmpty(mwra.age_of_youngest_child_years) ? mwra.age_of_youngest_child_years : "N/A";
            this.HaveUsedFpMethod = !string.IsNullOrEmpty(mwra.have_used_fp_method) ? mwra.have_used_fp_method : "N/A";
            this.NameOfFp = !string.IsNullOrEmpty(mwra.name_of_fp) ? mwra.name_of_fp : "N/A";
            this.FpNotUsedInYears = !string.IsNullOrEmpty(mwra.fp_not_used_in_years) ? mwra.fp_not_used_in_years : "N/A";
            this.ReasonOfDiscontinuation = !string.IsNullOrEmpty(mwra.reason_of_discontinuation) ? mwra.reason_of_discontinuation : "N/A";
            this.FpNoReason = !string.IsNullOrEmpty(mwra.fp_no_reason) ? mwra.fp_no_reason : "N/A";
            this.WantToUseFp = !string.IsNullOrEmpty(mwra.want_to_use_fp) ? mwra.want_to_use_fp : "N/A";
            this.FpPurpose = !string.IsNullOrEmpty(mwra.fp_purpose) ? mwra.fp_purpose : "N/A";
            this.IsUserFp = !string.IsNullOrEmpty(mwra.is_user_fp) ? mwra.is_user_fp : "N/A";
            this.FpMethodUsed = !string.IsNullOrEmpty(mwra.fp_method_used) ? mwra.fp_method_used : "N/A";
            this.DateOfRegistration = !string.IsNullOrEmpty(mwra.date_of_registration) ? mwra.date_of_registration : "N/A";
            this.IsActive = mwra.IsActive;
            this.CreatedAt = mwra.created_at.ToString();
            if (mwra.is_client == 1)
            {
                this.mwraClient = clientNewService.GetMwraClientByMwraId(mwra.mwra_id);
            }
            return this;
        }



        public List<Mwra> PrepareViewList(IEnumerable<Data.HandsDB.Mwra> appUser, IAppUserService appUserService, IRegionService regionService, ITaluqaService taluqaService, IUnionCouncilService unionCouncilService, IMwraClientNewService clientNewService)
        {
            return appUser.Select(x => new Mwra().GetMapMwra(x, appUserService, regionService, taluqaService, unionCouncilService, clientNewService, true)).ToList();
        }
        public List<Mwra> ListMwrasWithClientform(IEnumerable<Data.HandsDB.GetAllMwraWithRelationNamesReturnModel> mwra, IMwraClientNewService clientNewService)
        {
            return mwra.Select(x => new Mwra().GetMapMwraWithclient(x, clientNewService)).ToList();
        }

        public List<Mwra> PrepareViewList(List<GetAllMwraClientWithRelationNamesReturnModel> mwras, IMwraClientNewService clientNewService)
        {
            return mwras.Select(m => new Mwra().GetMapMwra(m, clientNewService)).ToList();
        }

        private Mwra GetMapMwra(GetAllMwraClientWithRelationNamesReturnModel mwra, IMwraClientNewService clientNewService)
        {
            this.MwraId = mwra.mwra_id;
            this.AssignedMarviId = mwra.assigned_marvi_id;
            this.AssignedMarviName = mwra.marviName ?? "N/A";
            this.Name = mwra.name ?? "N/A";
            this.Dob = mwra.dob ?? "N/A";
            this.Address = mwra.address ?? "N/A";
            this.ContactNumber = mwra.contact_number ?? "N/A";
            this.MaritialStatus = mwra.maritial_status ?? "N/A";
            this.Cnic = mwra.cnic ?? "N/A";
            this.HusbandName = mwra.husband_name ?? "N/A";
            this.Age = mwra.age;
            this.Occupation = mwra.occupation ?? "N/A";
            this.RegionId = mwra.region_id;
            this.TaluqaId = mwra.taluqa_id;
            this.UnionCouncilId = mwra.union_council_id;
            this.RegionName = mwra.region_name ?? "N/A";
            this.TaluqaName = mwra.taluqa_name ?? "N/A";
            this.UnionCouncilName = mwra.union_council_name ?? "N/A";
            this.Latitude = mwra.latitude ?? "N/A";
            this.Longitude = mwra.longitude ?? "N/A";
            this.IsClient = mwra.is_client;
            this.EducationOfClass = mwra.education_of_class ?? "N/A";
            this.DurationOfMarriage = mwra.duration_of_marriage ?? "N/A";
            this.CrrentlyPregnant = mwra.crrently_pregnant ?? "N/A";
            this.PregnantNoOfMonths = mwra.pregnant_no_of_months ?? "N/A";
            this.NoOfAliveChildren = mwra.no_of_alive_children ?? "N/A";
            this.NoOfAbortion = mwra.no_of_abortion ?? "N/A";
            this.NoOfChildrenDied = mwra.no_of_children_died ?? "N/A";
            this.ReasonOfDeath = mwra.reason_of_death ?? "N/A";
            this.AgeOfYoungestChildMonths = mwra.age_of_youngest_child_months ?? "N/A";
            this.AgeOfYoungestChildYears = mwra.age_of_youngest_child_years ?? "N/A";
            this.HaveUsedFpMethod = mwra.have_used_fp_method ?? "N/A";
            this.NameOfFp = mwra.name_of_fp ?? "N/A";
            this.FpNotUsedInYears = mwra.fp_not_used_in_years ?? "N/A";
            this.ReasonOfDiscontinuation = mwra.reason_of_discontinuation ?? "N/A";
            this.FpNoReason = mwra.fp_no_reason ?? "N/A";
            this.WantToUseFp = mwra.want_to_use_fp ?? "N/A";
            this.FpPurpose = mwra.fp_purpose ?? "N/A";
            this.IsUserFp = mwra.is_user_fp ?? "N/A";
            this.FpMethodUsed = mwra.fp_method_used ?? "N/A";
            this.DateOfRegistration = mwra.date_of_registration ?? "N/A";
            this.IsActive = mwra.IsActive;
            this.mwraClient = clientNewService.GetMwraClientByMwraId(mwra.mwra_id);
            this.CreatedAt = mwra.created_at.ToString();
            return this;
        }
    }
}