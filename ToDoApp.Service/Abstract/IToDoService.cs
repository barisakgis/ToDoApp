using Core.Entities;
using ToDoApp.Models.Entities;
using ToDoApp.Models.ToDos;

namespace ToDoApp.Service.Abstract;

public interface IToDoService
{
    Task<ReturnModel<ToDoResponseDto>> Add(CreateToDoRequestDto dto, string userId);
    ReturnModel<List<ToDoResponseDto>> GetAll();
    ReturnModel<ToDoResponseDto> GetById(Guid id);

    ReturnModel<ToDoResponseDto> Update(UpdateToDoRequestDto dto);

    ReturnModel<string> Delete(Guid id);

    ReturnModel<List<ToDoResponseDto>> GetAllByCategoryId(int id);
    ReturnModel<List<ToDoResponseDto>> GetAllByUserId(string userId);

    ReturnModel<List<ToDoResponseDto>> GetAllByTitleContains(string text);
     

}
