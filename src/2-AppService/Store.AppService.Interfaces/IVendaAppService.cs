
using Store.Domain.Models;

namespace Store.AppService.Interfaces;
public interface IVendaAppService
{
    void Add(Venda venda);
    void Update(Venda venda);
    Venda GetById(int id);
    IEnumerable<Venda> GetAll();
}
