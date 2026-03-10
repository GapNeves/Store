using LiteDB;
using Store.Domain.Interfaces;
using Store.Domain.Models;

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
        _vendaCollection.Insert(venda);
    }
    public void UpdateVenda(Venda venda)
    {
        _vendaCollection.Update(venda);
    }
    public Venda GetVendaById(Guid id)
    {
        return _vendaCollection.FindById(id);
    }

    public IEnumerable<Venda> GetAllVendas()
    {
        return _vendaCollection.FindAll();
    }
}
