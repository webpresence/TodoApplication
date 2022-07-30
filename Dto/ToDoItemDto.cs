using ToDoApplication.Enums;
namespace ToDoApplication.Dto
{
    public class ToDoItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ToDoListId { get; set; }
    }
}
