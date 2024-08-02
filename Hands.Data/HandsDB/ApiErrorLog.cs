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

    // Api_Error_Log
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class ApiErrorLog : Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string ObjectName { get; set; } // ObjectName (length: 255)
        public string ObjectContent { get; set; } // ObjectContent
        public System.DateTime? CreatedDate { get; set; } // created_date
        public bool IsActive { get; set; } // isActive

        public ApiErrorLog()
        {
            IsActive = true;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
