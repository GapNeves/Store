using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface IVendaRepository
{
    void IniciaVenda(Venda venda);
    void GetVenda(int id);
    void UpdateVenda(int id);
}
