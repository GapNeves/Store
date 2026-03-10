namespace Store.Domain.Models;
public class Venda
{
    public Guid Id { get; set; }
    public string CpfCliente { get; set; }
    public string NomeCliente { get; set; }
    public List<VendaProduto> Produtos { get; set; } = new ();
}

public class VendaProduto
{
    public Guid IdProduto { get; set; }
    public string NomeProduto { get; set; }
    public int QtdProduto { get; set; }
}
