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

        if (cliente.Cpf <= 0)
            throw new ArgumentException("CPF inválido");

        _clienteRepository.MapAddCliente(cliente);
    }

    public void ValidarEAtualizar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome))
            throw new ArgumentException("Nome é obrigatório");

        if (string.IsNullOrWhiteSpace(cliente.Email))
            throw new ArgumentException("Email é obrigatório");

        _clienteRepository.MapUpdateCliente(cliente);
    }
}
