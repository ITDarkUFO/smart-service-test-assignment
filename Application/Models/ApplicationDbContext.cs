using Application.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<RolePermissionExt> RolesPermissionExt { get; set; }

        public DbSet<UserDistrict> UserDistricts { get; set; }

        public DbSet<TenantMember> TenantMembers { get; set; }

        public DbSet<TaskOnlineAssigned> TasksOnlineAssigned { get; set; }

        public DbSet<TaskListCategory> TaskListCategories { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }
        
        public DbSet<UserWorkType> UserWorkTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>().ToTable("userrole", "adm")
                .HasKey(e => new { e.TenantID, e.UserID }).HasName("userrole_pkey");

            modelBuilder.Entity<RolePermissionExt>().ToTable("rolepermissionext", "adm")
                .HasKey(e => e.TenantID).HasName("rolepermissionext_pkey");

            modelBuilder.Entity<UserDistrict>().ToTable("userdistrict", "adm")
                .HasKey(e => new { e.DistrictID, e.UserID }).HasName("userdistrict_pkey");

            modelBuilder.Entity<TenantMember>().ToTable("tenantmember", "adm")
                .HasKey(e => e.ID).HasName("tenantmember_pkey");

            modelBuilder.Entity<TaskOnlineAssigned>().ToTable("taskonlineassigned", "work")
                .HasKey(e => new { e.TenantID, e.TaskID }).HasName("taskonlineassigned_pkey");

            modelBuilder.Entity<TaskListCategory>().ToTable("tasklistcategory", "work")
                .HasKey(e => e.ID).HasName("tasklistcategory_pkey");

            modelBuilder.Entity<WorkType>().ToTable("worktype", "work")
                .HasKey(e => e.ID).HasName("worktype_pkey");

            modelBuilder.Entity<UserWorkType>().ToTable("userworktype", "pa")
                .HasKey(e => e.WorkTypeID).HasName("userworktype_pkey");
        }
    }
}
