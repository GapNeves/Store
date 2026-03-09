using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface IProdutoRepository
{
    void AddProduto(Produto produto);
    void UpdateProduto(Produto produto);
    void DeleteProduto(Guid id);
    Produto GetProduto(Guid id);
    IEnumerable<Produto> GetAllProdutos();
}
