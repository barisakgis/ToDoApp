using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Service.Abstract;
using ToDoApp.Service.CacheServices;
using ToDoApp.Service.Concretes;

using ToDoApp.Service.Rules;

using System.Reflection;


namespace ToDoApp.Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependenies(this IServiceCollection services)
    {
       
        services.AddScoped<IToDoService, ToDoService>();
        services.AddScoped<ILoggerService, LoggerManager>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICategoryService, CategoryService>(); 
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtService, JwtService>(); 
        services.AddScoped<ToDoCacheService>(); 
        services.AddScoped<ToDoBusinessRules>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = "localhost:6379";
            opt.InstanceName = "ToDoApp";
        }); 


        return services;
    }
}
