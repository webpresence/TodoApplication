using ToDoApplication.Repositories.Interfaces;
using ToDoApplication.Data;
using ToDoApplication.Data.Entities;
using ToDoApplication.Enums;
using Microsoft.EntityFrameworkCore;

namespace ToDoApplication.Repositories
{
    public class ToDoListRepository : EfCoreRepositoryBase<ApplicationDbContext, ToDoList, int?>, IToDoListRepository
    {
        public ToDoListRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ToDoList> GetToDoListByUser(string userId)
        {
            var toDoList =  await GetDbContext().ToDoLists.Include(i=>i.ToDoItems).Where(l => l.UserId == userId).FirstOrDefaultAsync();
            if (toDoList == null)
            {
               toDoList = new ToDoList { Name = "Default List", Description = "Automatically created list", UserId = userId, Status = ToDoStatus.Active };
               await  base.Insert(toDoList);
               base.SaveChanges();
            }
            return toDoList;
        }
    }
}
