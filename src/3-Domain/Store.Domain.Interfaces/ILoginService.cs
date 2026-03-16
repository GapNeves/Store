using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface ILoginService
{
    LoginResponse Login(LoginRequest request);
}
