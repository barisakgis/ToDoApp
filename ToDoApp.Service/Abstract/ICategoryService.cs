using Core.Entities;
using ToDoApp.Models.Categories;
namespace ToDoApp.Service.Abstract;

public interface ICategoryService
{

    ReturnModel<List<CategoryResponseDto>> GetAllCategories();
    ReturnModel<CategoryResponseDto> GetById(int id);
    ReturnModel<NoData> Add(CategoryAddRequestDto dto);
    ReturnModel<NoData> Update(CategoryUpdateRequestDto dto);

    ReturnModel<NoData> Delete(int id);

}
