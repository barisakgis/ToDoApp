using ToDoApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models.ToDos;

public sealed record ToDoResponseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public Priority Priority { get; set; }
    public string CategoryName { get; set; }
    public bool Completed { get; set; } 
    public string UserId { get; set; }
    //public User User { get; set; }
}
