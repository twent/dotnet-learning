using webapi.Models;
using webapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoService _service;

    public TodoController(TodoService service)
    {
        _service = service;
    }

    // GET all action
    [HttpGet]
    public async Task<IList<Todo>> GetAll() =>
        await _service.GetAll();

    // GET by id action
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo?>> Get(long id)
    {
        return await _service.Get(id);
    }

    // POST action
    [HttpPost]
    public async Task<ActionResult<Todo>> Create(Todo todo)
    {            
        var createdTodo = await _service.Add(todo);

        return CreatedAtAction(nameof(Get), new { id = createdTodo.Id }, createdTodo);
    }

    // PUT action
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, Todo todo)
    {
        return await _service.Update(id, todo);           
    }

    // DELETE action
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        return await _service.Delete(id);
    }
}
