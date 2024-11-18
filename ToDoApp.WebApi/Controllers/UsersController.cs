using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models.Users;
using ToDoApp.Service.Abstract;
using ToDoApp.Service.Concretes;

namespace ToDoApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService _userService, IAuthenticationService _authenticationService, MailService _mailService) : ControllerBase
{


    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody]RegisterRequestDto dto)
    {
        var result = await _authenticationService.RegisterByTokenAsync(dto);

        return Ok(result);
    }


    [HttpGet("getbyemail")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var result = await _userService.GetByEmailAsync(email);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {

        var result = await _authenticationService.LoginByTokenAsync(dto);
        try
        { 
            await _mailService.SendEmailAsync(dto.Email, "Giriş yapıldı 22222222", "Başarıyla Giriş yapıldı"); 
            //return Ok(new { Message = "E-posta başarıyla gönderildi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "E-posta gönderimi sırasında bir hata oluştu.", Error = ex.Message });
        }
        return Ok(result); 
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery]string id)
    {
        var result = await _userService.DeleteAsync(id);
        return Ok(result);
    }


    [HttpPut("update")]
    public async Task<IActionResult> Update([FromQuery]string id, [FromBody] UpdateRequestDto dto)
    {
        var result = await _userService.UpdateAsync(id,dto);
        return Ok(result);
    }

    [HttpPut("changepassword")]
    public async Task<IActionResult> ChangePassword(string id, ChangePasswordRequestDto dto)
    {
        var result = await _userService.ChangePasswordAsync(id,dto);
        return Ok(result);
    }

}
