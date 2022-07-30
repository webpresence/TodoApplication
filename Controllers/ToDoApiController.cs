using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Dto;
using ToDoApplication.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoApiController : ControllerBase
    {
        public readonly IToDoService _toDoService;

        public ToDoApiController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        // GET api/<ToDoApiController>/5
        [HttpGet("{id}")]
        public async Task<List<ToDoItemDto>> Get(int id)
        {
            return await _toDoService.GetToDoItemsByListId(id);
        }

        // POST api/<ToDoApiController>
        [HttpPost]
        public void Post([FromBody] ToDoItemDto value)
        {
            _toDoService.EditToDoItem(value);
        }
    }
}
