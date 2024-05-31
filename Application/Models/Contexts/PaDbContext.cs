using Microsoft.EntityFrameworkCore;

namespace Application.Models.Contexts
{
    public class PaDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PaDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PaDbContext
            (DbContextOptions<PaDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }
        public DbSet<UserWorkType> UserWorkTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserWorkType>().ToTable("userworktype", "pa")
                .HasKey(e => e.WorkTypeID).HasName("userworktype_pkey");
        }
    }
}
