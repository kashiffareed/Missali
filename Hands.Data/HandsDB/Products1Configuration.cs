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

    // products1
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.36.1.0")]
    public partial class Products1Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Products1>
    {
        public Products1Configuration()
            : this("dbo")
        {
        }

        public Products1Configuration(string schema)
        {
            ToTable("products1", schema);
            HasKey(x => x.ProductId);

            Property(x => x.ProductId).HasColumnName(@"product_id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.ProductName).HasColumnName(@"product_name").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.Generic).HasColumnName(@"generic").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(255);
            Property(x => x.RegNo).HasColumnName(@"reg_no").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.PackSize).HasColumnName(@"pack_size").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.Path).HasColumnName(@"path").HasColumnType("varchar(max)").IsRequired().IsUnicode(false);
            Property(x => x.TP).HasColumnName(@"t_p").HasColumnType("decimal").IsRequired().HasPrecision(10,2);
            Property(x => x.RP).HasColumnName(@"r_p").HasColumnType("decimal").IsRequired().HasPrecision(10,2);
            Property(x => x.ClientId).HasColumnName(@"client_id").HasColumnType("int").IsRequired();
            Property(x => x.Producttype).HasColumnName(@"Producttype").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(150);
            Property(x => x.ProductCategory).HasColumnName(@"ProductCategory").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(250);
            Property(x => x.BrandName).HasColumnName(@"BrandName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(250);
            Property(x => x.MeasurementUnit).HasColumnName(@"MeasurementUnit").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(150);
            Property(x => x.CreatedAt).HasColumnName(@"created_at").HasColumnType("datetime2").IsRequired();
            Property(x => x.IsActive).HasColumnName(@"IsActive").HasColumnType("bit").IsRequired();
            Property(x => x.ProjectId).HasColumnName(@"ProjectId").HasColumnType("int").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
