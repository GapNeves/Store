using Store.Domain.Models.Enums;

namespace Store.Domain.Models;
public class LoginResponse
{
    public string Token { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public TipoCliente Tipo { get; set; }
}
