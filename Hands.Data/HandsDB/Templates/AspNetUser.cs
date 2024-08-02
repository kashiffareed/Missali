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

    // AspNetUsers
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class AspNetUser : Entity
    {
        public string Id { get; set; } // Id (Primary key) (length: 128)
        public string Email { get; set; } // Email (length: 256)
        public bool EmailConfirmed { get; set; } // EmailConfirmed
        public string PasswordHash { get; set; } // PasswordHash
        public string SecurityStamp { get; set; } // SecurityStamp
        public string PhoneNumber { get; set; } // PhoneNumber
        public bool PhoneNumberConfirmed { get; set; } // PhoneNumberConfirmed
        public bool TwoFactorEnabled { get; set; } // TwoFactorEnabled
        public System.DateTime? LockoutEndDateUtc { get; set; } // LockoutEndDateUtc
        public bool LockoutEnabled { get; set; } // LockoutEnabled
        public int AccessFailedCount { get; set; } // AccessFailedCount
        public string UserName { get; set; } // UserName (length: 256)
        public int? RegionId { get; set; } // RegionId
        public bool IsActive { get; set; } // IsActive
        public int? ProjectId { get; set; } // ProjectId

        // Reverse navigation

        /// <summary>
        /// Child AspNetRoles (Many-to-Many) mapped by table [AspNetUserRoles]
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AspNetRole> AspNetRoles { get; set; } // Many to many mapping
        /// <summary>
        /// Child UserMenuAccesses where [UserMenuAccess].[UserId] point to this entity (FK_UserMenuAccess_AspNetUsers)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserMenuAccess> UserMenuAccesses { get; set; } // UserMenuAccess.FK_UserMenuAccess_AspNetUsers

        public AspNetUser()
        {
            IsActive = true;
            UserMenuAccesses = new System.Collections.Generic.List<UserMenuAccess>();
            AspNetRoles = new System.Collections.Generic.List<AspNetRole>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
