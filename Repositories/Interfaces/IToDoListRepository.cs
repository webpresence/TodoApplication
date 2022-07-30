using ToDoApplication.Data.Entities;
namespace ToDoApplication.Repositories.Interfaces
{
    public interface IToDoListRepository : IRepository<ToDoList, int?>
    {
        Task<ToDoList> GetToDoListByUser(string userId);
    }
}
