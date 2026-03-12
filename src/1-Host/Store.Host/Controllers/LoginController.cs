using Microsoft.AspNetCore.Mvc;
using Store.AppService.Interfaces;
using Store.Domain.Models;

namespace Store.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginAppService _loginAppService;

    public LoginController(ILoginAppService loginAppService)
    {
        _loginAppService = loginAppService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            LoginResponse response = _loginAppService.Login(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao processar a solicitação", details = ex.Message });
        }
    }
}