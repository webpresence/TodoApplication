using ToDoApplication.Data.Entities;
using ToDoApplication.Dto;
namespace ToDoApplication.Services.Interfaces
{
    public interface IToDoService
    {
        Task<ToDoListDto> GetToDoListByUser(string userId);
        Task<ToDoItemDto> GetToDoItem(int? id);
        Task<List<ToDoItemDto>> GetToDoItemsByListId(int listId);
        Task<ToDoItemDto>? InsertToDoItem(ToDoItemDto toDoItem);
        Task<ToDoItemDto> EditToDoItem(ToDoItemDto toDoItem);
        Task<bool> DeleteToDoItem(int? id);
        Task<ToDoListDto> EditToDoList(ToDoListDto toDoList);
    }
}
