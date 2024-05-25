using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class TaskAssigned
    {
        public int TaskID { get; set; }

        public int AssignedTo { get; set; }
    }
}
