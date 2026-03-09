using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.Domain;
public class ProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public void ValidarEAdicionar(Produto produto)
    {
        if (string.IsNullOrWhiteSpace(produto.Nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        if (produto.Preco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero.");

        _produtoRepository.AddProduto(produto);
    }
    public void ValidarEAtualizar(Produto produto)
    {
        if (string.IsNullOrWhiteSpace(produto.Nome))
            throw new ArgumentException("O nome do produto é obrigatório.");

        if (produto.Preco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero.");

        _produtoRepository.UpdateProduto(produto);
    }
}
