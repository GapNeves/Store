using Store.AppService.Interfaces;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.AppService;

public class ClienteAppService : IClienteAppService
{
    private readonly IClienteService _clienteService;

    public ClienteAppService(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    public void Add(Cliente cliente)
    {
        try
        {
            _clienteService.AddCliente(cliente);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void Update(Cliente cliente)
    {
        try
        {
            _clienteService.UpdateCliente(cliente);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void Delete(Guid id)
    {
        try
        {
            _clienteService.DeleteCliente(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Cliente GetById(Guid id)
    {
        try
        {
            return _clienteService.GetClienteById(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Cliente> GetAll()
    {
        try
        {
            return _clienteService.GetAllClientes();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Cliente GetByCpf(string cpf)
    {
        try
        {
            return _clienteService.GetClienteByCpf(cpf);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
