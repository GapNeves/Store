namespace Store.Domain.Models.Entidades;
public class VendaEntity
{
    public Guid Id { get; set; }
    public string CpfCliente { get; set; }
    public string NomeCliente { get; set; }
    public List<VendaProdutoEntity> Produtos { get; set; } = new();
    public decimal TotalDaVenda { get; set; }
}

public class VendaProdutoEntity
{
    public Guid IdProduto { get; set; }
    public string NomeProduto { get; set; }
    public int QtdProduto { get; set; }
    public decimal ValorTotalProduto { get; set; }
}
