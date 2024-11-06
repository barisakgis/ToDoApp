using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using ToDoApp.Models.Categories;
using ToDoApp.Models.Entities;
using ToDoApp.Repository.Repositories.Abstracts;
using ToDoApp.Service.Abstract;

namespace ToDoApp.Service.Concretes;
 

public class CategoryService(ICategoryRepository _categoryRepository, IMapper _mapper, ILoggerService _logger) : ICategoryService
{
    public ReturnModel<NoData> Add(CategoryAddRequestDto dto)
    {
        Category category = _mapper.Map<Category>(dto);

        _categoryRepository.Add(category);

        return new ReturnModel<NoData>
        {
            Message = "Kategori Eklendi.",
            Status = 201,
            Success = true
        };

    }

    public ReturnModel<NoData> Delete(int id)
    {
        Category category = CheckGetById(id);

        _categoryRepository.Delete(category);

        return new ReturnModel<NoData>
        {
            Message = "Kategori silindi.",
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<List<CategoryResponseDto>> GetAllCategories()
    {
        List<Category> categories = _categoryRepository.GetAll();
        List<CategoryResponseDto> responses = _mapper.Map<List<CategoryResponseDto>>(categories);
         
        _logger.LogInfo("GetAllCategories metot called");

        return new ReturnModel<List<CategoryResponseDto>>
        {
            Data = responses,
            Status = 200,
            Success = true
        };
       
    }

    public ReturnModel<CategoryResponseDto> GetById(int id)
    {
        Category category = CheckGetById(id);
        CategoryResponseDto dto = _mapper.Map<CategoryResponseDto>(category);

        return new ReturnModel<CategoryResponseDto>
        {
            Data = dto,
            Status = 200,
            Success = true
        };

    }

    public ReturnModel<NoData> Update(CategoryUpdateRequestDto dto)
    {
        Category category = CheckGetById(dto.Id);
        category.Name = dto.Name;

        _categoryRepository.Update(category);

        return new ReturnModel<NoData>
        {
            Message = "Kategori Güncellendi.",
            Success = true,
            Status = 200
        };
    }
    // Private Methods
    private Category CheckGetById(int id)
    {
        var category = _categoryRepository.GetById(id);
        if(category is null)
        {
            throw new NotFoundException("İlgili id ye ait Kategori bulunamadı: "+id);
        }
        return category;
    }
}
