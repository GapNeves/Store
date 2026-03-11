using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface IClienteService
{
    void AddCliente(Cliente cliente);
    void UpdateCliente(Cliente cliente);
    void DeleteCliente(Guid id);
    Cliente GetClienteById(Guid id);
    IEnumerable<Cliente> GetAllClientes();
    Cliente GetClienteByCpf(string cpf);
}
