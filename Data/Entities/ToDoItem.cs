using System.ComponentModel.DataAnnotations;
using ToDoApplication.Enums;
namespace ToDoApplication.Data.Entities
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ToDoStatus Status { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int ToDoListId { get; set; }
    }
}
