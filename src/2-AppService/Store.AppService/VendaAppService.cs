using Store.AppService.Interfaces;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.AppService;

public class VendaAppService : IVendaAppService
{
    private readonly IVendaRepository _vendaRepository;

    public VendaAppService(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public void Add(Venda venda)
    {
        _vendaRepository.IniciaVenda(venda);
    }

    public void Update(Venda venda)
    {
        _vendaRepository.UpdateVenda(venda);
    }

    public Venda GetById(int id)
    {
        return _vendaRepository.GetVendaById(id);
    }

    public IEnumerable<Venda> GetAll()
    {
        return _vendaRepository.GetAllVendas();
    }
}
