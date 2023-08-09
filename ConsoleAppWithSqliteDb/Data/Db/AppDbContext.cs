using ConsoleAppWithSqliteDb.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleAppWithSqliteDb.Data.Db;

public class AppDbContext : DbContext
{
    public DbSet<TodoItemModel> TodoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("data source=TodoDb.db");

        base.OnConfiguring(optionsBuilder);
    }
}