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

    // temp_mwra
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class TempMwraConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TempMwra>
    {
        public TempMwraConfiguration()
            : this("dbo")
        {
        }

        public TempMwraConfiguration(string schema)
        {
            ToTable("temp_mwra", schema);
            HasKey(x => x.TempMwraId);

            Property(x => x.TempMwraId).HasColumnName(@"temp_mwra_id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.MwraId).HasColumnName(@"mwra_id").HasColumnType("int").IsRequired();
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
