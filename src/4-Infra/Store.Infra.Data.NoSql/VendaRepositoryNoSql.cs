using LiteDB;
using Store.Domain.Models;
using Store.Infra.Interfaces;

namespace Store.Infra.Data.NoSql;
public class VendaRepositoryNoSql : IVendaRepository
{
    private readonly ILiteCollection<Venda> _vendaCollection;
    private readonly ILiteDatabase _database;

    public VendaRepositoryNoSql(ILiteDatabase database)
    {
        _database = database;
        _vendaCollection = _database.GetCollection<Venda>("vendas");
    }
    public void IniciaVenda(Venda venda)
    {
        try
        {
            _vendaCollection.Insert(venda);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
    public void UpdateVenda(Venda venda)
    {
        try
        {
            _vendaCollection.Update(venda);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
    public Venda GetVendaById(Guid id)
    {
        try
        {
            return _vendaCollection.FindById(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Venda> GetAllVendas()
    {
        try
        {
            return _vendaCollection.FindAll();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Venda> GetVendasByCpf(string cpf)
    {
        try
        {
            return _vendaCollection.Find(v => v.CpfCliente == cpf);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
