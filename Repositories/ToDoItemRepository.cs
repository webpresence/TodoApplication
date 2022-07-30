using Microsoft.EntityFrameworkCore;
using ToDoApplication.Data;
using ToDoApplication.Data.Entities;
using ToDoApplication.Repositories.Interfaces;

namespace ToDoApplication.Repositories
{
    public class ToDoItemRepository : EfCoreRepositoryBase<ApplicationDbContext, ToDoItem, int?>, IToDoItemRepository
    {
        public ToDoItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<ToDoItem>> GetToDoItemsByListId(int listId)
        {
            return await GetDbContext().ToDoItems.Where(i => i.ToDoListId == listId && i.IsDeleted != true).ToListAsync();
        }
    }
}
