
using LiteDB;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.Infra.Data.NoSql;
public class ProdutoRepositoryNoSql : IProdutoRepository
{
    private readonly ILiteCollection<Produto> _produtoCollection;
    private readonly ILiteDatabase _database;

    public ProdutoRepositoryNoSql(ILiteDatabase database)
    {
        _database = database;
        _produtoCollection = _database.GetCollection<Produto>("produtos");
    }

    public void AddProduto(Produto produto)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduto(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Produto> GetAllProdutos()
    {
        throw new NotImplementedException();
    }

    public Produto GetProduto(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduto(Produto produto)
    {
        throw new NotImplementedException();
    }
}
