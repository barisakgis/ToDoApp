using Core.Repository;
using ToDoApp.Models.Entities;
using ToDoApp.Repository.Contexts;
using ToDoApp.Repository.Repositories.Abstracts;
namespace ToDoApp.Repository.Repositories.Concretes;

public class EfToDoRepository : EfRepositoryBase<BaseDbContext, ToDo, Guid>, IToDoRepository
{
    public EfToDoRepository(BaseDbContext context) : base(context)
    {
    }

   
}
