using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface IVendaRepository
{
    void IniciaVenda(Venda venda);
    void UpdateVenda(Venda venda);
    Venda GetVendaById(int id);
    IEnumerable<Venda> GetAllVendas();
}
