using Store.Domain.Models;

namespace Store.AppService.Interfaces;

public interface IClienteAppService
{
    void Add(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(int id);
    Cliente GetById(int id);
    IEnumerable<Cliente> GetAll();
}
