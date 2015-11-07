using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Data.Mapping;
using Repository.Pattern.Ef6;
using Model;

namespace Data
{
    public partial class ApplicationContext : DataContext
    {
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(null);
        }

        public ApplicationContext()
             : base("Name=SeliseCricCardContext")
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Over> Overs { get; set; }
        public DbSet<OverDetail> OverDetails { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new MatchMap());
            modelBuilder.Configurations.Add(new OverMap());
            modelBuilder.Configurations.Add(new OverDetailMap());
            modelBuilder.Configurations.Add(new TeamMap());

        }
    }
}
