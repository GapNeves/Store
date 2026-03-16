using Store.Domain.Models;

namespace Store.AppService.Interfaces;
public interface ILoginAppService
{
    LoginResponse Login(LoginRequest request);
}
