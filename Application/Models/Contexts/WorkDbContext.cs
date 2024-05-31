using Microsoft.EntityFrameworkCore;

namespace Application.Models.Contexts
{
    public class WorkDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public WorkDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WorkDbContext
            (DbContextOptions<WorkDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<TaskOnlineAssigned> TasksOnlineAssigned { get; set; }

        public DbSet<TaskListCategory> TaskListCategories { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskOnlineAssigned>().ToTable("taskonlineassigned", "work")
                .HasKey(e => new { e.TenantID, e.TaskID }).HasName("taskonlineassigned_pkey");

            modelBuilder.Entity<TaskListCategory>().ToTable("tasklistcategory", "work")
                .HasKey(e => e.ID).HasName("tasklistcategory_pkey");

            modelBuilder.Entity<WorkType>().ToTable("worktype", "work")
                .HasKey(e => e.ID).HasName("worktype_pkey");
        }
    }
}
