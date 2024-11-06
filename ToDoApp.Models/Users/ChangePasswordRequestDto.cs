using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Models.Users;

public sealed record ChangePasswordRequestDto(string OldPassword, string NewPassword);

