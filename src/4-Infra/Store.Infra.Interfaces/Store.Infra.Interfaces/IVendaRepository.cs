using Store.Domain.Models;

namespace Store.Infra.Interfaces;
public interface IVendaRepository
{
    void IniciaVenda(Venda venda);
    void UpdateVenda(Venda venda);
    Venda GetVendaById(Guid id);
    IEnumerable<Venda> GetAllVendas();
    IEnumerable<Venda> GetVendasByCpf(string cpf);
}
