using Store.AppService.Interfaces;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.AppService;
public class ProdutoAppService : IProdutoAppService
{
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoAppService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }
    public void Add(Produto produto)
    {
        _produtoRepository.AddProduto(produto);
    }
    public void Update(Produto produto)
    {
        _produtoRepository.UpdateProduto(produto);
    }
    public void Delete(Guid id)
    {
        _produtoRepository.DeleteProduto(id);
    }
    public Produto GetById(Guid id)
    {
        return _produtoRepository.GetProduto(id);
    }
    public IEnumerable<Produto> GetAll()
    {
        return _produtoRepository.GetAllProdutos();
    }
}
