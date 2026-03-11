using Store.Domain.Models;

namespace Store.AppService.Interfaces;

public interface IClienteAppService
{
    void Add(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(Guid id);
    Cliente GetById(Guid id);
    IEnumerable<Cliente> GetAll();
    Cliente GetByCpf(string cpf);
}
