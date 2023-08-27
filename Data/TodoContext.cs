using Microsoft.EntityFrameworkCore;

namespace webapi.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options) { }

    public DbSet<Models.Todo>? Todos => Set<Models.Todo>();
}
