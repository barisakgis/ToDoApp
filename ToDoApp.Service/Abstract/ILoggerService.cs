using Core.Entities;
using ToDoApp.Models.Entities;
using ToDoApp.Models.ToDos;

namespace ToDoApp.Service.Abstract;

public interface ILoggerService
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogDebug(string message);
}

