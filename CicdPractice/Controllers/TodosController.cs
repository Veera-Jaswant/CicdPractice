using CicdPractice.Models;
using CicdPractice.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CicdPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodosController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var item = _service.GetById(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public ActionResult<TodoItem> Create(CreateTodoRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest("Title is required.");
            }

            var item = _service.Add(request.Title);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }
    }
}
