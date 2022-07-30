using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Enums;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ToDoApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToDoService _todoService;
        public HomeController(ILogger<HomeController> logger, IToDoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ToDoListViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var toDoList = await _todoService.GetToDoListByUser(userId);
                model.ToDoList = toDoList;
            }
            
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = new ToDoListViewModel();
            if (User.Identity?.IsAuthenticated == true)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var toDoList = await _todoService.GetToDoListByUser(userId);
                model.ToDoList = toDoList;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ToDoListViewModel viewModel)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                viewModel.ToDoList.Status = ToDoStatus.Active;
                viewModel.ToDoList.UserId = userId;
                var toDoList = await _todoService.EditToDoList(viewModel.ToDoList);
                viewModel.ToDoList = toDoList;
            }

            return RedirectToAction(nameof(Index), "Home", viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}