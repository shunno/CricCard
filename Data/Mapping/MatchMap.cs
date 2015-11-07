using Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping
{
    public class MatchMap : EntityTypeConfiguration<Match>
    {
        public MatchMap()
        {
            // Primary Key
            this.HasKey(t => t.MatchID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Match");
            this.Property(t => t.MatchID).HasColumnName("MatchID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Team1ID).HasColumnName("Team1ID");
            this.Property(t => t.Team2ID).HasColumnName("Team2ID");
            this.Property(t => t.IsTeam1Bowl).HasColumnName("IsTeam1Bowl");
            this.Property(t => t.IsTeam2Bowl).HasColumnName("IsTeam2Bowl");
            this.Ignore(item => item.FirstTeamName);
            this.Ignore(item => item.SecondTeamName);
            this.Ignore(item => item.TotalRun);
            
            // Relationships
            this.HasRequired(t => t.Team1)
                .WithMany(t => t.Matches)
                .HasForeignKey(d => d.Team1ID);
            this.HasRequired(t => t.Team2)
                .WithMany(t => t.Matches1)
                .HasForeignKey(d => d.Team2ID);

        }
    }
}
