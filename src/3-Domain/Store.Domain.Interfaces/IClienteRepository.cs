using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface IClienteRepository
{
    void MapAddCliente(Cliente cliente);
    void MapUpdateCliente(Cliente cliente);
    void MapDeleteCliente(int id);
    Cliente MapGetCliente(int id);
    IEnumerable<Cliente> MapGetAllClientes();
}
