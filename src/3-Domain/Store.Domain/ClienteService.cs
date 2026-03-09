using System.Text.RegularExpressions;
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
            throw new ArgumentException("Nome é obrigatório.");

        bool ehValido = Regex.IsMatch(cliente.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        if (string.IsNullOrWhiteSpace(cliente.Email) || !ehValido)
            throw new ArgumentException("Email inválido, adicione um email válido.");

        if (string.IsNullOrWhiteSpace(cliente.Cpf) || cliente.Cpf.Length != 11)
            throw new ArgumentException("CPF do cliente é obrigatório e deve ter 11 dígitos.");

        Cliente clienteExistente = _clienteRepository.GetClienteByCpf(cliente.Cpf);
        if (clienteExistente != null)
            throw new ArgumentException("CPF já cadastrado no sistema.");

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
