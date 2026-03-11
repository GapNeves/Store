using LiteDB;
using Store.Domain.Models;
using Store.Infra.Interfaces;

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
        try
        {
            _produtoCollection.Insert(produto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void DeleteProduto(Guid id)
    {
        try
        {
            _produtoCollection.Delete(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Produto> GetAllProdutos()
    {
        try
        {
            return _produtoCollection.FindAll();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Produto GetProduto(Guid id)
    {
        try
        {
            return _produtoCollection.FindById(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void UpdateProduto(Produto produto)
    {
        try
        {
            _produtoCollection.Update(produto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
