using Store.Domain.Interfaces;
using Store.Domain.Models;
using Store.Infra.Interfaces;

namespace Store.Domain;
public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepositoryNoSql;
    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepositoryNoSql = produtoRepository;
    }

    public void AddProduto(Produto produto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new ArgumentException("O nome do produto é obrigatório.");

            if (produto.Preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");

            _produtoRepositoryNoSql.AddProduto(produto);
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
            _produtoRepositoryNoSql.DeleteProduto(id);
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
            return _produtoRepositoryNoSql.GetAllProdutos();
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
            return _produtoRepositoryNoSql.GetProduto(id);
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
            if (string.IsNullOrWhiteSpace(produto.Nome))
                throw new ArgumentException("O nome do produto é obrigatório.");

            if (produto.Preco <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.");

            _produtoRepositoryNoSql.UpdateProduto(produto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
