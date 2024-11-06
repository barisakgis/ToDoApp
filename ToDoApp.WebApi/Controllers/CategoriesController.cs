using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToDoApp.Models.Categories;
using ToDoApp.Service.Abstract;
using ToDoApp.WebApi.Middlewares;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService _categoryService, ILoggerService _loggerService) : ControllerBase
{
    


    [HttpPost("add")]
   // [Authorize(Roles ="Admin")]
    public IActionResult Add([FromBody] CategoryAddRequestDto dto)
    {
        var result = _categoryService.Add(dto);
        return Ok(result);
    }
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _categoryService.GetAllCategories();

        _loggerService.LogInfo("GetAllCategories called in controller");

        return Ok(result);
    }


    [HttpGet("getbyid/{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var result = _categoryService.GetById(id);

        return Ok(result);
    }


    [HttpDelete("delete/{id:int}")]
  //  [Authorize(Roles ="Admin")]
    public IActionResult Delete([FromRoute] int id)
    {
        var result = _categoryService.Delete(id);

        return Ok(result);
    }


    [HttpPut("update")]
  //  [Authorize(Roles ="Admin")]
    public IActionResult Update([FromBody] CategoryUpdateRequestDto dto)
    {
        var result = _categoryService.Update(dto);

        return Ok(result);
    }

}
