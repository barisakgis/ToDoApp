using AutoMapper;
using ToDoApp.Models.Entities;
using ToDoApp.Models.ToDos;

namespace ToDoApp.Service.Mappings;

public class ToDoProfiles : Profile
{
    public ToDoProfiles()
    {
        CreateMap<CreateToDoRequestDto, ToDo>();
        CreateMap<ToDo, ToDoResponseDto>();
        CreateMap<UpdateToDoRequestDto, ToDo>();
    }
}
