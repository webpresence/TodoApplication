using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.Dto;
using ToDoApplication.Enums;
using ToDoApplication.Models;
using ToDoApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ToDoApplication.Controllers
{
    [Authorize]
    public class ToDoItemsController : Controller
    {
        private readonly IToDoService _toDoService;

        public ToDoItemsController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        // GET: ToDoItems/Create/1
        public IActionResult Create(int? id)
        {
            var todoItem = new ToDoItemDto
            {
                ToDoListId = (int)id,
                Status = ToDoStatus.Draft
            };
            var model = new CreateOrEditItemViewModel
            {
                ToDoItemDto = todoItem
            };
            return View(model);
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Status, ToDoListId")] ToDoItemDto toDoItemDto)
        {
            if (ModelState.IsValid)
            {
                var saved = await _toDoService.InsertToDoItem(toDoItemDto);
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(toDoItemDto);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ToDoItemDto toDoItemDto)
        {
            if (id != toDoItemDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _toDoService.EditToDoItem(toDoItemDto);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItemDto);
        }

        // GET: ToDoItemDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoItemDto = await _toDoService.GetToDoItem(id);
            if (toDoItemDto == null)
            {
                return NotFound();
            }

            return View(toDoItemDto);
        }

        // POST: ToDoItemDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoItemDto =  await _toDoService.DeleteToDoItem(id); 
           
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
