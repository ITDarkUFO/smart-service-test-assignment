namespace Application.Models
{
    public class UserRole
    {
        public short TenantID { get; set; }

        public int UserID { get; set; }

        public int RoleID { get; set; }

        public bool? Deleted { get; set; }
    }
}
