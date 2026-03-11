using Store.Domain.Models;

namespace Store.AppService.Interfaces;
public interface IVendaAppService
{
    void Add(Venda venda);
    void Update(Venda venda);
    Venda GetById(Guid id);
    IEnumerable<Venda> GetAll();
    IEnumerable<Venda> GetByCpf(string cpf);
}
