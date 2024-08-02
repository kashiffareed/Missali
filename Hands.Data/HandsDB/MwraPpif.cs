// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.8
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

namespace Hands.Data.HandsDB
{

    // The table 'mwra_ppif' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // mwra_ppif
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class MwraPpif : Entity
    {
        public double? MwraId { get; set; } // mwra_id
        public string Name { get; set; } // name (length: 255)
        public System.DateTime? Dob { get; set; } // dob
        public string Address { get; set; } // address (length: 255)
        public double? ContactNumber { get; set; } // contact_number
        public string DbMarviId { get; set; } // db_marvi_id (length: 255)
        public double? AssignedMarviId { get; set; } // assigned_marvi_id
        public string Longitude { get; set; } // longitude (length: 255)
        public string Latitude { get; set; } // latitude (length: 255)
        public string HusbandName { get; set; } // husband_name (length: 255)
        public double? Cnic { get; set; } // cnic
        public string MaritialStatus { get; set; } // maritial_status (length: 255)
        public double? Age { get; set; } // age
        public string Occupation { get; set; } // occupation (length: 255)
        public double? IsClient { get; set; } // is_client
        public System.DateTime? CreatedAt { get; set; } // created_at
        public double? RegionId { get; set; } // region_id
        public double? TaluqaId { get; set; } // taluqa_id
        public string DbUc { get; set; } // Db_UC (length: 255)
        public double? UnionCouncilId { get; set; } // union_council_id
        public double? EducationOfClass { get; set; } // education_of_class
        public double? DurationOfMarriage { get; set; } // duration_of_marriage
        public string CrrentlyPregnant { get; set; } // crrently_pregnant (length: 255)
        public string PregnantNoOfMonths { get; set; } // pregnant_no_of_months (length: 255)
        public double? NoOfAliveChildren { get; set; } // no_of_alive_children
        public double? NoOfAbortion { get; set; } // no_of_abortion
        public string NoOfChildrenDied { get; set; } // no_of_children_died (length: 255)
        public string ReasonOfDeath { get; set; } // reason_of_death (length: 255)
        public string AgeOfYoungestChildYears { get; set; } // age_of_youngest_child_years (length: 255)
        public double? AgeOfYoungestChildMonths { get; set; } // age_of_youngest_child_months
        public string HaveUsedFpMethod { get; set; } // have_used_fp_method (length: 255)
        public string NameOfFp { get; set; } // name_of_fp (length: 255)
        public string FpNotUsedInYears { get; set; } // fp_not_used_in_years (length: 255)
        public string ReasonOfDiscontinuation { get; set; } // reason_of_discontinuation (length: 255)
        public string FpNoReason { get; set; } // fp_no_reason (length: 255)
        public string WantToUseFp { get; set; } // want_to_use_fp (length: 255)
        public string FpPurpose { get; set; } // fp_purpose (length: 255)
        public string IsUserFp { get; set; } // is_user_fp (length: 255)
        public string FpMethodUsed { get; set; } // fp_method_used (length: 255)
        public System.DateTime? DateOfRegistration { get; set; } // date_of_registration

        public MwraPpif()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
