using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.Data;
using ToDoApplication.Data.Entities;
using ToDoApplication.Repositories;
using ToDoApplication.Repositories.Interfaces;
using ToDoApplication.Services;
using ToDoApplication.Services.Interfaces;

namespace ToDoApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<IRepository<ToDoItem, int?>, EfCoreRepositoryBase<ApplicationDbContext, ToDoItem, int?>>();
            builder.Services.AddTransient<IRepository<ToDoList, int?>, EfCoreRepositoryBase<ApplicationDbContext, ToDoList, int?>>();
            builder.Services.AddTransient<IToDoListRepository, ToDoListRepository>();
            builder.Services.AddTransient<IToDoItemRepository, ToDoItemRepository>();
            builder.Services.AddTransient<IToDoService, ToDoService>();
            builder.Services.AddAutoMapper(typeof(Program));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}