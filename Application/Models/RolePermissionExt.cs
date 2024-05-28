namespace Application.Models
{
    public class RolePermissionExt
    {
        public short TenantID { get; set; }

        public int RoleID { get; set; }

        public short? Permissionextid { get; set; }

        public bool? Deleted { get; set; }
    }
}
