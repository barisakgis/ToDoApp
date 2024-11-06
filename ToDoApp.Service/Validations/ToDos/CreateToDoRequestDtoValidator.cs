using FluentValidation;
using ToDoApp.Models.ToDos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.Validations.ToDos;

public class CreateToDoRequestDtoValidator : AbstractValidator<CreateToDoRequestDto>
{
    public CreateToDoRequestDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("ToDo Başlığı boş olamaz.")
            .Length(2, 50).WithMessage("ToDo Başlığı Minimum 2 max 50 karakterli olmalıdır.");
         
        RuleFor(x => x.Description).NotEmpty().WithMessage("ToDo İçeriği boş olamaz.");
    }
}
