using Core.Repository;
using ToDoApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Repository.Repositories.Abstracts;

public interface ICategoryRepository : IRepository<Category,int>
{
}
