

namespace Store.Domain.Models;
public class Venda
{
    public Guid Id { get; set; }
    public Cliente CpfCliente { get; set; } = new Cliente();
    public List<VendaProduto> Produtos { get; set; } = new ();
}

public class VendaProduto
{
    public int IdProduto { get; set; }
    public string NomeProduto { get; set; }
    public int QtdProduto { get; set; }
}
