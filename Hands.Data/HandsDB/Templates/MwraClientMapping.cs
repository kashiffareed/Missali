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

    // mwra_clientMapping
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class MwraClientMapping : Entity
    {
        public int MwraClientId { get; set; } // mwra_client_id (Primary key)
        public string Foul { get; set; } // foul (length: 50)
        public string PainLower { get; set; } // pain_lower (length: 50)
        public string Referral { get; set; } // referral (length: 255)
        public string History { get; set; } // history (length: 50)
        public string Contraceptual { get; set; } // contraceptual (length: 50)
        public string DateOfStarting { get; set; } // date_of_starting (length: 50)
        public string Weight { get; set; } // weight (length: 255)
        public string LmpDate { get; set; } // lmp_date (length: 50)
        public string DateOfClientGeneration { get; set; } // date_of_client_generation (length: 50)
        public string Bp { get; set; } // bp (length: 255)
        public string Jaundice { get; set; } // jaundice (length: 50)
        public string Occasional { get; set; } // occasional (length: 50)
        public string PolarAnemia { get; set; } // polar_anemia (length: 50)
        public int? MwraId { get; set; } // mwra_id
        public int? ProductId { get; set; } // ProductId
        public int? Quantity { get; set; } // Quantity
        public System.DateTime? CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive (Primary key)
        public string Mwcode { get; set; } // mwcode (length: 500)
        public string Longitude { get; set; } // longitude (length: 20)
        public string Latitude { get; set; } // latitude (length: 20)
        public string Fpmethod { get; set; } // fpmethod (length: 255)
        public string Nextfollowupdate { get; set; } // nextfollowupdate (length: 100)
        public int? ProjectId { get; set; } // ProjectId

        public MwraClientMapping()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
