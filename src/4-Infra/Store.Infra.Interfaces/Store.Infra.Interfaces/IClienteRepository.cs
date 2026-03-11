using Store.Domain.Models;

namespace Store.Infra.Interfaces;
public interface IClienteRepository
{
    void AddCliente(Cliente cliente);
    void UpdateCliente(Cliente cliente);
    void DeleteCliente(Guid id);
    Cliente GetClienteById(Guid id);
    IEnumerable<Cliente> GetAllClientes();
    Cliente GetClienteByCpf(string cpf);
}
