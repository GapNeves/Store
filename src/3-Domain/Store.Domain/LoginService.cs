using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Interfaces;
using Store.Domain.Models;
using Store.Domain.Models.Enums;
using Store.Infra.Interfaces;

namespace Store.Domain;
public class LoginService : ILoginService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IConfiguration _configuration;

    public LoginService(IClienteRepository clienteRepository, IConfiguration configuration)
    {
        _clienteRepository = clienteRepository;
        _configuration = configuration;
    }

    public LoginResponse Login(LoginRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Cpf))
                throw new ArgumentException("Email é obrigatório.");

            if (string.IsNullOrWhiteSpace(request.Senha))
                throw new ArgumentException("Senha é obrigatória.");

            Cliente cliente = _clienteRepository.GetClienteByCpf(request.Cpf);

            if (cliente == null || !BCrypt.Net.BCrypt.Verify(request.Senha, cliente.Senha))
                throw new UnauthorizedAccessException("CPF ou senha inválidos.");

            string token = GerarToken(cliente);

            return new LoginResponse
            {
                Token = token,
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Tipo = cliente.Tipo
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    private string GerarToken(Cliente cliente)
    {
        byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        string role = Enum.GetName(typeof(TipoCliente), cliente.Tipo) ?? TipoCliente.Cliente.ToString();

        Claim[] claims = 
        [
            new Claim(ClaimTypes.NameIdentifier, cliente.Id.ToString()),
            new Claim(ClaimTypes.Name, cliente.Nome),
            new Claim(ClaimTypes.Email, cliente.Cpf),
            new Claim("role", role)
        ];

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler handler = new();
        SecurityToken token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }
}
