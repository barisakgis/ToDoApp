using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using NLog;
using ToDoApp.Models.Entities;
using ToDoApp.Models.ToDos;
using ToDoApp.Repository.Repositories.Abstracts;
using ToDoApp.Service.Abstract;
using ToDoApp.Service.CacheServices;
using ToDoApp.Service.Constants;
using ToDoApp.Service.Rules;

namespace ToDoApp.Service.Concretes;

public class LoggerManager : ILoggerService
{
    private static ILogger logger = LogManager.GetCurrentClassLogger();
    public void LogDebug(string message) => logger.Debug(message);

    public void LogError(string message) => logger.Error(message);

    public void LogInfo(string message) => logger.Info(message);

    public void LogWarning(string message) => logger.Warn(message);

}
