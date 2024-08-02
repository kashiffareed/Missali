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

    // Api_Duplicate_Record_Logs
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class ApiDuplicateRecordLog : Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string RequestUrl { get; set; } // RequestUrl (length: 255)
        public string JsonString { get; set; } // JsonString
        public System.DateTime CreatedDate { get; set; } // CreatedDate
        public bool IsActive { get; set; } // IsActive

        public ApiDuplicateRecordLog()
        {
            CreatedDate = System.DateTime.Now;
            IsActive = true;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
