using ToDoApp.Models.Entities;

namespace ToDoApp.Models.ToDos;

public sealed record CreateToDoRequestDto(string Title, string Description, DateTime StartDate, DateTime EndDate, DateTime CreatedDate, Priority Priority, int CategoryId, bool Completed, string UserId);


