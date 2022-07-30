using ToDoApplication.Data.Entities;

namespace ToDoApplication.Repositories.Interfaces
{
    public interface IToDoItemRepository : IRepository<ToDoItem, int?>
    {
        Task<List<ToDoItem>> GetToDoItemsByListId(int listId);
    }
}