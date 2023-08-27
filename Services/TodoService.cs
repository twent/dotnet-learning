using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models;

namespace webapi.Services;

public class TodoService
{
    private readonly TodoContext _context;

    public TodoService(TodoContext context) 
    {
        _context = context;
    }

    public async Task<ActionResult<Todo?>> Get(long id) 
    {
        if(_context.Todos != null)
        {
            return await _context.Todos.FindAsync(id);
        }

        return new NoContentResult();
    }

    public async Task<IList<Todo>> GetAll()
    {
        if(_context.Todos != null)
        {
            return await _context.Todos.ToListAsync();
        }

        return new List<Todo>();
    }

    public async Task<Todo> Add(Todo todo)
    {
        if(_context.Todos != null)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        return todo;
    }

    public async Task<IActionResult> Update(long id, Todo todo)
    {
        if (id != todo.Id) {
            return new NotFoundResult();
        }
        
        _context.Entry(todo).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<IActionResult> Delete(long id)
    {
        if(_context.Todos != null)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return new NotFoundResult();
            }
        
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        return new NotFoundResult();
    }  
}
