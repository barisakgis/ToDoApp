using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models.ToDos;
using ToDoApp.Service.Abstract;
using System.Security.Claims;

namespace ToDoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ToDosController(IToDoService _toDoService) : ControllerBase
{
   
    [HttpGet("getall")]
    [Authorize(Roles = "User")]
    public IActionResult GetAll()
    {
        var result = _toDoService.GetAll();
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody]CreateToDoRequestDto dto)
    {

        // kullanıcının tokenden id alanının alınması.
        string authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = _toDoService.Add(dto,authorId);
        return Ok(result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById([FromRoute]Guid id)
    {

        var result = _toDoService.GetById(id);
        return Ok(result);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] UpdateToDoRequestDto dto)
    {
        var result = _toDoService.Update(dto);
        return Ok(result);
    }

    [HttpGet("getallbycategoryid")]
    public IActionResult GetAllByCategoryId(int id)
    {
        var result = _toDoService.GetAllByCategoryId(id);
        return Ok(result);
    }

    [HttpGet("getallbyuserid")]
    public IActionResult GetAllByUserId(string id)
    {
    
        var result = _toDoService.GetAllByUserId(id);
        return Ok(result);
    }

    [HttpGet("getallbytitlecontains")]
    public IActionResult GetAllByTitleContains(string text)
    {
        var result = _toDoService.GetAllByTitleContains(text);
        return Ok(result);
    }


    [HttpGet("owntoDo")]
    public IActionResult OwnToDos()
    {
        string userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = _toDoService.GetAllByUserId(userId);

        return Ok(result);

    }
}