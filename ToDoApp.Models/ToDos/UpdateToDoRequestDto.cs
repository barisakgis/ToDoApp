using ToDoApp.Models.Entities;

namespace ToDoApp.Models.ToDos;

public sealed record UpdateToDoRequestDto(Guid Id, string Title, string Description, DateTime StartDate, DateTime EndDate, DateTime CreatedDate, Priority Priority, int CategoryId, bool Completed, string UserId);
