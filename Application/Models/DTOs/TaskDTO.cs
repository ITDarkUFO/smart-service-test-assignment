namespace Application.Models.DTOs
{
    public class TaskDTO
    {
        public int ID { get; set; }

        public int AssetID { get; set; }

        public int? ApprovalWith { get; set; }

        public int? EscalatedTo { get; set; }

        public int CreatedBy { get; set; }

        public int RequestedBy { get; set; }

        public short WorkTypeID { get; set; }

        public DateTime Archived { get; set; }

        public DateTime Deleted { get; set; }
    }
}
