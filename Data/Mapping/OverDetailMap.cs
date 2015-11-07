using Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping
{
    public class OverDetailMap : EntityTypeConfiguration<OverDetail>
    {
        public OverDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.OverDetailsID);

            // Properties
            this.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("OverDetails");
            this.Property(t => t.OverDetailsID).HasColumnName("OverDetailsID");
            this.Property(t => t.OverID).HasColumnName("OverID");
            this.Property(t => t.BallNumber).HasColumnName("BallNumber");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.RunTaken).HasColumnName("RunTaken");
            this.Property(t => t.IsWide).HasColumnName("IsWide");
            this.Property(t => t.BallIndex).HasColumnName("BallIndex");
            this.Ignore(item => item.OverNumber);
            
            // Relationships
            this.HasRequired(t => t.Over)
                .WithMany(t => t.OverDetails)
                .HasForeignKey(d => d.OverID);

        }
    }
}
