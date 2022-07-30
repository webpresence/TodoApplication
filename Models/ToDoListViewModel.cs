using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoApplication.Dto;
using ToDoApplication.Enums;
namespace ToDoApplication.Models
{
    public class ToDoListViewModel
    {
        public ToDoListViewModel()
        {
            var selects = from s in ToDoStatus.List select new SelectListItem { Value = s.Value, Text = s.Name };
            Statuses = new SelectList(selects, "Value", "Text");
        }
        public ToDoListDto ToDoList { get; set; }
        public SelectList Statuses { get; set; }
    }
}
