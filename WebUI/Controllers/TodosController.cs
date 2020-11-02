using Business.Abstract;
using Entities.Dto.Todo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : BaseController
    {
        private readonly ITodoService _todoService;
        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var result = await _todoService.GetListByFilter();

            if (!result.Success) return NotFound();
            return Ok(result.Data);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] TodoAddInput addInput)
        {
            var result = await _todoService.Add(addInput);
            if (!result.Success) return BadRequest(result.Message);

            return CreatedAtAction("GetTodo", new { todoId = result.Data.Id }, result.Data);
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] TodoUpdateInput updateInput)
        {
            var result = await _todoService.Update(updateInput);
            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Success);
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Remove([FromBody] TodoUpdateInput todoUpdateInput)
        {
            var result = await _todoService.Update(todoUpdateInput);
            if (!result.Success) return BadRequest(result.Message);

            return NoContent();
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTodoForUser(string userId)
        {
            Guid convertId;
            Guid.TryParse(userId, out convertId);
            var result = await _todoService.GetByFilter(t => t.UserId == convertId);

            if (!result.Success) return NotFound();
            if (result.Data?.TodoItems?.Count == 0) return NotFound();
            return Ok(result.Data);
        }
    }
}
