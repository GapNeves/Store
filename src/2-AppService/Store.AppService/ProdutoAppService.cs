using Store.AppService.Interfaces;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.AppService;
public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoService _produtoService;
    public ProdutoAppService(IProdutoService produtoRepository)
    {
        _produtoService = produtoRepository;
    }
    public void Add(Produto produto)
    {
        try
        {
            _produtoService.AddProduto(produto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
    public void Update(Produto produto)
    {
        try
        {
            _produtoService.UpdateProduto(produto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
    public void Delete(Guid id)
    {
        try
        {
            _produtoService.DeleteProduto(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
    public Produto GetById(Guid id)
    {
        try
        {
            return _produtoService.GetProduto(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
    public IEnumerable<Produto> GetAll()
    {
        try
        {
            return _produtoService.GetAllProdutos();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
