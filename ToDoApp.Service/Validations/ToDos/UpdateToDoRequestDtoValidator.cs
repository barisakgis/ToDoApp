using FluentValidation;
using ToDoApp.Models.ToDos;
using ToDoApp.Models.ToDos;

namespace ToDoApp.Service.Validations.ToDos;

public class UpdateToDoRequestDtoValidator : AbstractValidator<UpdateToDoRequestDto>
{
    public UpdateToDoRequestDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("ToDo Başlığı boş olamaz.")
    .Length(2, 50).WithMessage("ToDo Başlığı Minimum 2 max 50 karakterli olmalıdır.");


        RuleFor(x => x.Description).NotEmpty().WithMessage("ToDo İçeriği boş olamaz.");
    }
}
