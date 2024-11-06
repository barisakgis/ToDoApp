using Core.Repository;
using ToDoApp.Models.Entities;

namespace ToDoApp.Repository.Repositories.Abstracts;

public interface IToDoRepository : IRepository<ToDo, Guid>
{
  
}
