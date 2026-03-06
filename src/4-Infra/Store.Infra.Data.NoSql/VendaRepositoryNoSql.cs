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

    public void GetVenda(int id)
    {
        throw new NotImplementedException();
    }

    public void IniciaVenda(Venda venda)
    {
        throw new NotImplementedException();
    }

    public void UpdateVenda(int id)
    {
        throw new NotImplementedException();
    }
}
