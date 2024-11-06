using AutoMapper;
using ToDoApp.Models.Categories;
using ToDoApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.Mappings
{
    public class CategoriesProfile : Profile
    {

        public CategoriesProfile()
        {
            CreateMap<CategoryAddRequestDto, Category>();
            CreateMap<CategoryUpdateRequestDto, Category>();
            CreateMap<Category, CategoryResponseDto>();
        }
    }
}
