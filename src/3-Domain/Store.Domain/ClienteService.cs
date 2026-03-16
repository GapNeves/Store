using System.Text.RegularExpressions;
using Store.Domain.Interfaces;
using Store.Domain.Models;
using Store.Domain.Models.Enums;
using Store.Infra.Interfaces;
using Store.Shared;

namespace Store.Domain;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepositoryNoSql;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepositoryNoSql = clienteRepository;
    }

    public void AddCliente(Cliente cliente)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new ArgumentException("Nome do cliente é obrigatório.");

            bool ehValido = Regex.IsMatch(cliente.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (string.IsNullOrWhiteSpace(cliente.Email) || !ehValido)
                throw new ArgumentException("Email inválido, adicione um email válido.");

            if (string.IsNullOrWhiteSpace(cliente.Cpf) || !ValidarCpf.IsCpf(cliente.Cpf))
                throw new ArgumentException("CPF inválido, informe um CPF válido.");

            Cliente clienteExistente = _clienteRepositoryNoSql.GetClienteByCpf(cliente.Cpf);
            if (clienteExistente != null)
                throw new ArgumentException("CPF já cadastrado no sistema.");

            if (cliente?.Tipo == null || cliente.Tipo == 0)
            {
                cliente.Tipo = TipoCliente.Cliente;
            }

            cliente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);

            _clienteRepositoryNoSql.AddCliente(cliente);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void DeleteCliente(Guid id)
    {
        try
        {
            _clienteRepositoryNoSql.DeleteCliente(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Cliente> GetAllClientes()
    {
        try
        {
            return _clienteRepositoryNoSql.GetAllClientes();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Cliente GetClienteByCpf(string cpf)
    {
        try
        {
            return _clienteRepositoryNoSql.GetClienteByCpf(cpf);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Cliente GetClienteById(Guid id)
    {
        try
        {
            return _clienteRepositoryNoSql.GetClienteById(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void UpdateCliente(Cliente cliente)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cliente.Nome))
                throw new ArgumentException("Nome do cliente é obrigatório");

            bool ehValido = Regex.IsMatch(cliente.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            if (string.IsNullOrWhiteSpace(cliente.Email) || !ehValido)
                throw new ArgumentException("Email inválido, adicione um email válido.");

            if (cliente?.Tipo == null || cliente.Tipo == 0)
                cliente.Tipo = TipoCliente.Cliente;


            Cliente clienteExistente = _clienteRepositoryNoSql.GetClienteById(cliente.Id);

            bool senhaAlterada = !BCrypt.Net.BCrypt.Verify(cliente.Senha, clienteExistente?.Senha);

            if (senhaAlterada)
                cliente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);
            else
                cliente.Senha = clienteExistente.Senha;

            _clienteRepositoryNoSql.UpdateCliente(cliente);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }   
    }
}
