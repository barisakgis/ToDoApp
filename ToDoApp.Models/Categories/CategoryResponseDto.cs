using ToDoApp.Models.Entities;

namespace ToDoApp.Models.Categories
{
    public sealed record CategoryResponseDto
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public List<ToDo> ToDos { get; set; }

    }

}
