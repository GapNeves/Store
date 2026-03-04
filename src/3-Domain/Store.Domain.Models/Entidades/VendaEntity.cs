namespace Store.Domain.Models.Entidades;
public class VendaEntity
{
    public Guid Id { get; set; }
    public ClienteEntity CpfCliente { get; set; } = new ClienteEntity();
    public List<VendaProdutoEntity> Produtos { get; set; } = new();
}

public class VendaProdutoEntity
{
    public int IdProduto { get; set; }
    public string NomeProduto { get; set; }
    public int QtdProduto { get; set; }
}
