using ToDoApplication.Data.Entities;
using ToDoApplication.Enums;

namespace ToDoApplication.Dto
{
    public class ToDoListDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; }
        public List<ToDoItemDto> ToDoItems { get; set; }
    }
}
