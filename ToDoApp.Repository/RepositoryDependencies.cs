using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Repository.Contexts;
using ToDoApp.Repository.Repositories.Abstracts;
using ToDoApp.Repository.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Repository;

public static class RepositoryDependencies
{

    public static IServiceCollection AddRepositoryDepencdencies(this IServiceCollection services,IConfiguration configuration)
    {
        
        services.AddScoped<IToDoRepository, EfToDoRepository>();
        services.AddScoped<ICategoryRepository, EfCategoryRepository>(); 
        services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));
        return services;
    }
}
