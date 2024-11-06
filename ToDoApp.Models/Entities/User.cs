using Core.Entities;
using Microsoft.AspNetCore.Identity;


namespace ToDoApp.Models.Entities;

public sealed class User : IdentityUser
{

    public DateTime BirthDate { get; set; }
     
    public List<ToDo> ToDos { get; set; }
     

}
