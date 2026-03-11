using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface IVendaService
{
    void IniciaVenda(Venda venda);
    void UpdateVenda(Venda venda);
    Venda GetVendaById(Guid id);
    IEnumerable<Venda> GetAllVendas();
    IEnumerable<Venda> GetVendasByCpf(string cpf);
}
