using System.Text.RegularExpressions;
using Store.Domain.Interfaces;
using Store.Domain.Models;
using Store.Shared;

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
            throw new ArgumentException("Nome do cliente é obrigatório.");

        bool ehValido = Regex.IsMatch(cliente.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (string.IsNullOrWhiteSpace(cliente.Email) || !ehValido)
            throw new ArgumentException("Email inválido, adicione um email válido.");

        if (string.IsNullOrWhiteSpace(cliente.Cpf) || !ValidarCpf.IsCpf(cliente.Cpf))
            throw new ArgumentException("CPF inválido, informe um CPF válido.");

        Cliente clienteExistente = _clienteRepository.GetClienteByCpf(cliente.Cpf);
        if (clienteExistente != null)
            throw new ArgumentException("CPF já cadastrado no sistema.");

        _clienteRepository.AddCliente(cliente);
    }

    public void ValidarEAtualizar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nome))
            throw new ArgumentException("Nome do cliente é obrigatório");

        bool ehValido = Regex.IsMatch(cliente.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (string.IsNullOrWhiteSpace(cliente.Email) || !ehValido)
            throw new ArgumentException("Email inválido, adicione um email válido.");

        _clienteRepository.UpdateCliente(cliente);
    }
}
