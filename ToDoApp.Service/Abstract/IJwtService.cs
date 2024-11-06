using ToDoApp.Models.Entities;
using ToDoApp.Models.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.Abstract;

public interface IJwtService
{
    Task<TokenResponseDto> CreateToken(User user);
}
