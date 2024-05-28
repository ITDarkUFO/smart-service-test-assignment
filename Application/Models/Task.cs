namespace Application.Models
{
    public class Task
    {
        public int ID { get; set; }

        public int ApprovalWith { get; set; }

        public int EscalatedTo { get; set; }
    }
}
