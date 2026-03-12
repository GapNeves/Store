using Store.AppService.Interfaces;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.AppService;
public class LoginAppService : ILoginAppService
{
    private readonly ILoginService _loginService;

    public LoginAppService(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public LoginResponse Login(LoginRequest request)
    {
        try
        {
            return _loginService.Login(request);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
