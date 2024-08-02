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

    // The table 'products_ppif' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // products_ppif
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class ProductsPpif : Entity
    {
        public double? ProductId { get; set; } // product_id
        public string ProductName { get; set; } // product_name (length: 255)
        public string Generic { get; set; } // generic (length: 255)
        public double? RegNo { get; set; } // reg_no
        public double? PackSize { get; set; } // pack_size
        public string Path { get; set; } // path (length: 255)
        public double? TP { get; set; } // t_p
        public double? RP { get; set; } // r_p
        public string UnitOfMeasurement { get; set; } // unit_of_measurement (length: 255)
        public double? ClientId { get; set; } // client_id
        public string ProductType { get; set; } // product_type (length: 255)
        public System.DateTime? CreatedAt { get; set; } // created_at
        public string CategoryName { get; set; } // category_name (length: 255)
        public string CompanyName { get; set; } // company_name (length: 255)

        public ProductsPpif()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
