using Microsoft.EntityFrameworkCore;

namespace Application.Models.Contexts
{
    public class AdminDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AdminDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AdminDbContext
            (DbContextOptions<AdminDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<RolePermissionExt> RolesPermissionExt { get; set; }

        public DbSet<UserDistrict> UserDistricts { get; set; }

        public DbSet<TenantMember> TenantMembers { get; set; }

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
        }
    }
}
