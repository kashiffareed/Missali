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

    // regionsMapping
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class RegionsMappingConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<RegionsMapping>
    {
        public RegionsMappingConfiguration()
            : this("dbo")
        {
        }

        public RegionsMappingConfiguration(string schema)
        {
            ToTable("regionsMapping", schema);
            HasKey(x => new { x.UnionCouncilId, x.UnionCouncilName });

            Property(x => x.UnionCouncilId).HasColumnName(@"union_council_id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.UnionCouncilName).HasColumnName(@"union_council_name").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
            Property(x => x.TaluqaId).HasColumnName(@"taluqa_id").HasColumnType("int").IsOptional();
            Property(x => x.TaluqaName).HasColumnName(@"taluqa_name").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.RegionsId).HasColumnName(@"regions_id").HasColumnType("int").IsOptional();
            Property(x => x.RegionName).HasColumnName(@"region_name").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ClientId).HasColumnName(@"client_id").HasColumnType("int").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
