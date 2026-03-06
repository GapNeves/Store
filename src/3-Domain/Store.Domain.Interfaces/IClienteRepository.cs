using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface IClienteRepository
{
    void AddCliente(Cliente cliente);
    void UpdateCliente(Cliente cliente);
    void DeleteCliente(int id);
    Cliente GetClienteById(int id);
    IEnumerable<Cliente> GetAllClientes();
}
