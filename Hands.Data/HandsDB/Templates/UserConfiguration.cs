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

    // users
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class UserConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<User>
    {
        public UserConfiguration()
            : this("dbo")
        {
        }

        public UserConfiguration(string schema)
        {
            ToTable("users", schema);
            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnName(@"user_id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.FullName).HasColumnName(@"full_name").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.UserName).HasColumnName(@"user_name").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Pwd).HasColumnName(@"pwd").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.PlainPassword).HasColumnName(@"plain_password").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(500);
            Property(x => x.Email).HasColumnName(@"email").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.RoleId).HasColumnName(@"role_id").HasColumnType("int").IsRequired();
            Property(x => x.Designation).HasColumnName(@"designation").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(500);
            Property(x => x.CreatedAt).HasColumnName(@"created_at").HasColumnType("datetime2").IsRequired();
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            Property(x => x.ProjectId).HasColumnName(@"ProjectId").HasColumnType("int").IsOptional();

            // Foreign keys
            HasOptional(a => a.ProjectSolution).WithMany(b => b.Users).HasForeignKey(c => c.ProjectId).WillCascadeOnDelete(false); // FK_users_ProjectSolution
            HasRequired(a => a.Role).WithMany(b => b.Users).HasForeignKey(c => c.RoleId).WillCascadeOnDelete(false); // FK_users_roles
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
