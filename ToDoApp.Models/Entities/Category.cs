using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models.Entities;

public class Category : Entity<int>
{

    public string Name { get; set; }
     
    public List<ToDo> ToDos { get; set; }
}
