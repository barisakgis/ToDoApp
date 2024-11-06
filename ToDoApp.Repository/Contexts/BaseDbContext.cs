using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models.Entities;
using ToDoApp.Repository.Configurations;
using System.Reflection;

namespace ToDoApp.Repository.Contexts;

public class BaseDbContext : IdentityDbContext<User, IdentityRole, string>
{

    public BaseDbContext(DbContextOptions opt) : base(opt)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<ToDo> ToDos { get; set; }

    public DbSet<Category> Categories { get; set; }
     
}
