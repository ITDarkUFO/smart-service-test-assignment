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

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<ListCategory> ListCategories { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskAssigned> TaskAssigneds { get; set; }

        public DbSet<TaskOnlineAssigned> TaskOnlineAssigneds { get; set; }

        public DbSet<TaskResponsibleUser> TaskResponsibleUsers { get; set; }

        public DbSet<TaskUserCache> TaskUserCache { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTaskListCategory> UserTasksListCategories { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<RolePermissionExt> RolePermissionExts { get; set; }

        public DbSet<UserDistrict> UserDistricts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ListCategory>()
                .HasKey(e => e.ID).HasName("listcategory_pkey");

            modelBuilder.Entity<Task>()
                .HasKey(e => e.ID).HasName("task_pkey");

            modelBuilder.Entity<TaskAssigned>()
                .HasKey(e => e.TaskID).HasName("taskassigned_pkey");

            modelBuilder.Entity<TaskOnlineAssigned>()
                .HasKey(e => new { e.TenantID, e.TaskID }).HasName("taskonlineassigned_pkey");

            modelBuilder.Entity<TaskResponsibleUser>()
                .HasKey(e => e.TaskID).HasName("taskresponcibleuser_pkey");

            modelBuilder.Entity<TaskUserCache>()
                .HasKey(e => e.TaskID).HasName("taskusercache_pkey");

            modelBuilder.Entity<User>()
                .HasKey(e => e.ID).HasName("user_pkey");

            modelBuilder.Entity<UserTaskListCategory>()
                .HasKey(e => e.UserID).HasName("usertasklistcategory_pkey");

            modelBuilder.Entity<UserRole>().ToTable("userrole", "adm")
                .HasKey(e => new { e.TenantID, e.UserID }).HasName("userrole_pkey");

            modelBuilder.Entity<RolePermissionExt>().ToTable("rolepermissionext", "adm")
                .HasKey(e => e.TenantID).HasName("rolepermissionext_pkey");

            modelBuilder.Entity<UserDistrict>().ToTable("userdistrict", "adm")
                .HasKey(e => new { e.DistrictID, e.UserID }).HasName("userdistrict_pkey");
        }
    }
}
