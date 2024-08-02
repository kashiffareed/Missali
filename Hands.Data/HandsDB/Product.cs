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

    // products
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class Product : Entity
    {
        public int ProductId { get; set; } // product_id (Primary key)
        public string ProductName { get; set; } // product_name (length: 255)
        public string Generic { get; set; } // generic (length: 255)
        public string RegNo { get; set; } // reg_no (length: 50)
        public string PackSize { get; set; } // pack_size (length: 100)
        public string Path { get; set; } // path
        public decimal TP { get; set; } // t_p
        public decimal RP { get; set; } // r_p
        public int ClientId { get; set; } // client_id
        public string Producttype { get; set; } // Producttype (length: 150)
        public string ProductCategory { get; set; } // ProductCategory (length: 250)
        public string BrandName { get; set; } // BrandName (length: 250)
        public string MeasurementUnit { get; set; } // MeasurementUnit (length: 150)
        public System.DateTime CreatedAt { get; set; } // created_at
        public bool IsActive { get; set; } // IsActive
        public int? ProjectId { get; set; } // ProjectId

        public Product()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
