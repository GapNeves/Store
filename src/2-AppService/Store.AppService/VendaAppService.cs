
using Store.AppService.Interfaces;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.AppService;

public class VendaAppService : IVendaAppService
{
    private readonly IVendaService _vendaService;

    public VendaAppService(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    public void Add(Venda venda)
    {
        try
        {
            _vendaService.IniciaVenda(venda);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void Update(Venda venda)
    {
        try
        {
            _vendaService.UpdateVenda(venda);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Venda GetById(Guid id)
    {
        try
        {
            return _vendaService.GetVendaById(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Venda> GetAll()
    {
        try
        {
            return _vendaService.GetAllVendas();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Venda> GetByCpf(string cpf)
    {
        try
        {
            return _vendaService.GetAllVendas().Where(v => v.CpfCliente == cpf);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
