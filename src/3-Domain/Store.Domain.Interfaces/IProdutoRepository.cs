using Store.Domain.Models;

namespace Store.Domain.Interfaces;
public interface IProdutoRepository
{
    void AddProduto(Produto produto);
    void UpdateProduto(Produto produto);
    void DeleteProduto(int id);
    Produto GetProduto(int id);
    IEnumerable<Produto> GetAllProdutos();
}
