using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.Domain;

public class ClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public void ValidarEAdicionar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome))
            throw new ArgumentException("Nome é obrigatório");

        if (string.IsNullOrWhiteSpace(cliente.Email))
            throw new ArgumentException("Email é obrigatório");

        if (string.IsNullOrWhiteSpace(cliente.Cpf) || cliente.Cpf.Length != 11)
            throw new ArgumentException("CPF do cliente é obrigatório e deve ter 11 dígitos");

        _clienteRepository.AddCliente(cliente);
    }

    public void ValidarEAtualizar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome))
            throw new ArgumentException("Nome é obrigatório");

        if (string.IsNullOrWhiteSpace(cliente.Email))
            throw new ArgumentException("Email é obrigatório");

        _clienteRepository.UpdateCliente(cliente);
    }
}
