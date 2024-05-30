namespace Application.Models.DTOs
{
    //TODO Убрать nullable (см. WorkTask)
    public class ListCategoryDTO
    {
        public byte ID { get; set; }

        public short? Permissionextid { get; set; }
    }
}
