using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskAssigned> TaskAssigneds { get; set; }

        public DbSet<TaskOnlineAssigned> TaskOnlineAssigneds { get; set; }

        public DbSet<TaskResponsibleUser> TaskResponsibleUsers { get; set; }

        public DbSet<TaskUserCache> TaskUserCache { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTaskListCategory> UserTasksListCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Task>()
                .HasKey(e => e.ID).HasName("task_pkey");

            modelBuilder.Entity<TaskAssigned>()
                .HasKey(e => e.TaskID).HasName("taskassigned_pkey");

            modelBuilder.Entity<TaskOnlineAssigned>()
                .HasKey(e => new { e.TenantID, e.TaskID });

            modelBuilder.Entity<TaskResponsibleUser>()
                .HasKey(e => e.TaskID).HasName("taskresponcibleuser_pkey");

            modelBuilder.Entity<TaskUserCache>()
                .HasKey(e => e.TaskID).HasName("taskusercache_pkey");

            modelBuilder.Entity<User>()
                .HasKey(e => e.ID).HasName("user_pkey");

            modelBuilder.Entity<UserTaskListCategory>()
                .HasKey(e => e.UserID).HasName("usertasklistcategory_pkey");
        }
    }
}
