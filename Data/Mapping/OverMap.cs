using Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping
{
    public class OverMap : EntityTypeConfiguration<Over>
    {
        public OverMap()
        {
            // Primary Key
            this.HasKey(t => t.OverID);

            // Properties
            // Table & Column Mappings
            this.ToTable("Over");
            this.Property(t => t.OverID).HasColumnName("OverID");
            this.Property(t => t.MatchID).HasColumnName("MatchID");
            this.Property(t => t.TeamID).HasColumnName("TeamID");
            this.Property(t => t.OverNumber).HasColumnName("OverNumber");

            // Relationships
            this.HasRequired(t => t.Match)
                .WithMany(t => t.Overs)
                .HasForeignKey(d => d.MatchID);
            this.HasRequired(t => t.Team)
                .WithMany(t => t.Overs)
                .HasForeignKey(d => d.TeamID);

        }
    }
}
