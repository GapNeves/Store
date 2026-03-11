using Store.Domain.Interfaces;
using Store.Domain.Models;
using Store.Infra.Interfaces;

namespace Store.Domain;
public class VendaService : IVendaService
{
    private readonly IVendaRepository _vendaRepositoryNoSql;
    private readonly IClienteRepository _clienteRepositoryNoSql;
    private readonly IProdutoRepository _produtoRepositoryNoSql;

    public VendaService(IVendaRepository vendaRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository)
    {
        _vendaRepositoryNoSql = vendaRepository;
        _clienteRepositoryNoSql = clienteRepository;
        _produtoRepositoryNoSql = produtoRepository;
    }

    public IEnumerable<Venda> GetAllVendas()
    {
        try
        {
            return _vendaRepositoryNoSql.GetAllVendas();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Venda GetVendaById(Guid id)
    {
        try
        {
            return _vendaRepositoryNoSql.GetVendaById(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void IniciaVenda(Venda venda)
    {
        try
        {
            if (venda.CpfCliente == null || string.IsNullOrWhiteSpace(venda.CpfCliente))
                throw new ArgumentException("Para iniciar uma venda, o CPF do cliente é obrigatório.");

            if (venda.Produtos == null || !venda.Produtos.Any())
                throw new ArgumentException("Para iniciar uma venda, é necessário adicionar pelo menos um produto.");

            foreach (VendaProduto vendaProduto in venda.Produtos)
            {
                Produto produtoEstoque = _produtoRepositoryNoSql.GetProduto(vendaProduto.IdProduto);

                if (produtoEstoque == null)
                    throw new ArgumentException($"Produto com ID {vendaProduto.IdProduto} não encontrado no estoque.");

                if (produtoEstoque.QtdEstoque <= 0)
                    throw new ArgumentException($"A quantidade do produto '{produtoEstoque.Nome}' deve ser maior que zero para realizar a compra.");

                if (vendaProduto.QtdProduto > produtoEstoque.QtdEstoque)
                    throw new ArgumentException(
                        $"Estoque insuficiente para o produto '{produtoEstoque.Nome}'." +
                        $"Quantidade solicitada: {vendaProduto.QtdProduto}, " +
                        $"Quantidade disponivel: {produtoEstoque.QtdEstoque}"
                    );
            }

            _vendaRepositoryNoSql.IniciaVenda(venda);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public void UpdateVenda(Venda venda)
    {
        try
        {
            _vendaRepositoryNoSql.UpdateVenda(venda);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Venda> GetVendasByCpf(string cpf)
    {
        try
        {
            return _vendaRepositoryNoSql.GetVendasByCpf(cpf);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
