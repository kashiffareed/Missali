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

    // The table 'stocks_ppif' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    // stocks_ppif
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class StocksPpif : Entity
    {
        public double? StockId { get; set; } // stock_id
        public double? ProductId { get; set; } // product_id
        public double? Quantity { get; set; } // quantity
        public string Notes { get; set; } // notes (length: 255)
        public System.DateTime? CreatedAt { get; set; } // created_at

        public StocksPpif()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
