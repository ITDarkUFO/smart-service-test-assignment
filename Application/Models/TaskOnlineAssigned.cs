namespace Application.Models
{
    public class TaskOnlineAssigned
    {
        public int TenantID { get; set; }
        
        public int TaskID { get; set; }

        public int AssignedTo { get; set; }
    }
}
