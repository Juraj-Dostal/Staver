using Microsoft.EntityFrameworkCore;

namespace ApiStaver.Models
{
    public class StatServerContext : DbContext
    {
        public StatServerContext(DbContextOptions<StatServerContext> options) : base(options)
        {
        }

        public DbSet<ComputerStat> ComputerStats { get; set; } = null!;
        public DbSet<BitcoinStat> BitcoinStats { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComputerStat>().ToTable("ComputerStat");
            modelBuilder.Entity<BitcoinStat>().ToTable("BitcoinStat");
        }
    }
}
