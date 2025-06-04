using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiStaver.Models
{
    public class StatServerContext : DbContext
    {
        public StatServerContext(DbContextOptions<StatServerContext> options) : base(options)
        {
        }

        public DbSet<ComputerStat> ComputerStats { get; set; } = null!;
        public DbSet<BitcoinStat> BitcoinStats { get; set; } = null!;
        public DbSet<TempHumSensor> TempHumSensors { get; set; } = null!;
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ComputerStat>().ToTable("ComputerStat");
            modelBuilder.Entity<BitcoinStat>().ToTable("BitcoinStat");
            modelBuilder.Entity<TempHumSensor>().ToTable("TempHumSensor");

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            var conventer = new EnumToStringConverter<RoleEnum>();
            modelBuilder.Entity<Role>().Property(u => u.RoleName).HasConversion(conventer);

            // Define the User entity and its table mapping
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity("UserRole",
                    u => u.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId"),
                    r => r.HasOne(typeof(User)).WithMany().HasForeignKey("UserId"),
                    ur => ur.HasKey("UserId", "RoleId")
                );

            
        }

    }
}
