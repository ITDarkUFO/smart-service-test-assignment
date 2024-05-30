namespace Application.Models
{
    public class UserWorkType
    {
        public int WorkTypeID { get; set; }

        public short TenantID { get; set; }

        public int UserID { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
