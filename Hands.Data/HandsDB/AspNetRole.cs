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

    // AspNetRoles
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class AspNetRole : Entity
    {
        public string Id { get; set; } // Id (Primary key) (length: 128)
        public string Name { get; set; } // Name (length: 256)
        public bool IsActive { get; set; } // IsActive
        public int? ProjectId { get; set; } // ProjectId

        // Reverse navigation

        /// <summary>
        /// Child AspNetUsers (Many-to-Many) mapped by table [AspNetUserRoles]
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AspNetUser> AspNetUsers { get; set; } // Many to many mapping
        /// <summary>
        /// Child AssignRoleToProjects where [AssignRoleToProject].[RoleId] point to this entity (FK_AssignRoleToProject_AspNetRoles)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AssignRoleToProject> AssignRoleToProjects { get; set; } // AssignRoleToProject.FK_AssignRoleToProject_AspNetRoles

        public AspNetRole()
        {
            IsActive = true;
            AssignRoleToProjects = new System.Collections.Generic.List<AssignRoleToProject>();
            AspNetUsers = new System.Collections.Generic.List<AspNetUser>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
