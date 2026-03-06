using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.Domain;
public class VendaService
{
    private readonly IVendaRepository _vendaRepository;

    public VendaService(IVendaRepository vendaRepository)
    {
        _vendaRepository = vendaRepository;
    }

    public void ValidarEAdicionar(Venda venda)
    {
        if (venda.CpfCliente == null)
            throw new ArgumentException("Uma venda precisa de um CPF de cliente válido");

        _vendaRepository.IniciaVenda(venda);
    }

    public void ValidarEAtualizar(Venda venda)
    {
        if (venda.CpfCliente == null)
            throw new ArgumentException("Uma venda precisa de um CPF de cliente válido");
    
        _vendaRepository.UpdateVenda(venda);
    }
}
