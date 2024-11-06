using Core.Repository;
using ToDoApp.Models.Entities;
using ToDoApp.Repository.Contexts;
using ToDoApp.Repository.Repositories.Abstracts;

namespace ToDoApp.Repository.Repositories.Concretes;

public class EfCategoryRepository : EfRepositoryBase<BaseDbContext,Category,int>, ICategoryRepository
{

    public EfCategoryRepository(BaseDbContext context) : base(context)
    {
        
    }
}
