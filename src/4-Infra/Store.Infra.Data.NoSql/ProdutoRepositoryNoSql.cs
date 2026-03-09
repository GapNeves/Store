
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
        _produtoCollection.Insert(produto);
    }

    public void DeleteProduto(int id)
    {
        _produtoCollection.Delete(id);
    }

    public IEnumerable<Produto> GetAllProdutos()
    {
        return _produtoCollection.FindAll();
    }

    public Produto GetProduto(int id)
    {
        return _produtoCollection.FindById(id);
    }

    public void UpdateProduto(Produto produto)
    {
        _produtoCollection.Update(produto);
    }
}
