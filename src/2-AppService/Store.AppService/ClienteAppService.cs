using Store.AppService.Interfaces;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.AppService;

public class ClienteAppService : IClienteAppService
{
    private readonly IClienteRepository _clienteRepository;

    public ClienteAppService(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public void Add(Cliente cliente)
    {
        _clienteRepository.MapAddCliente(cliente);
    }

    public void Update(Cliente cliente)
    {
        _clienteRepository.MapUpdateCliente(cliente);
    }

    public void Delete(int id)
    {
        _clienteRepository.MapDeleteCliente(id);
    }

    public Cliente GetById(int id)
    {
        return _clienteRepository.MapGetCliente(id);
    }

    public IEnumerable<Cliente> GetAll()
    {
        return _clienteRepository.MapGetAllClientes();
    }
}
