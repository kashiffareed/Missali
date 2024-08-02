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

    // The table 'mwra_client_ppif' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // mwra_client_ppif
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class MwraClientPpif : Entity
    {
        public double? MwraClientId { get; set; } // mwra_client_id
        public string Foul { get; set; } // foul (length: 255)
        public string PainLower { get; set; } // pain_lower (length: 255)
        public string Referral { get; set; } // referral (length: 255)
        public string History { get; set; } // history (length: 255)
        public string Contraceptual { get; set; } // contraceptual (length: 255)
        public System.DateTime? DateOfStarting { get; set; } // date_of_starting
        public double? Weight { get; set; } // weight
        public System.DateTime? LmpDate { get; set; } // lmp_date
        public System.DateTime? DateOfClientGeneration { get; set; } // date_of_client_generation
        public string Bp { get; set; } // bp (length: 255)
        public string Jaundice { get; set; } // jaundice (length: 255)
        public string Occasional { get; set; } // occasional (length: 255)
        public string PolarAnemia { get; set; } // polar_anemia (length: 255)
        public double? MwraId { get; set; } // mwra_id
        public double? Latitude { get; set; } // latitude
        public double? Longitude { get; set; } // longitude
        public System.DateTime? CreatedAt { get; set; } // created_at
        public string MwraName { get; set; } // mwra_name (length: 255)
        public double? AssignedMarviId { get; set; } // assigned_marvi_id
        public string HusbandName { get; set; } // husband_name (length: 255)

        public MwraClientPpif()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
