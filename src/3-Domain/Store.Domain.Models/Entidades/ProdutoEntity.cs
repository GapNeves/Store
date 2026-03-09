namespace Store.Domain.Models.Entidades;
public class ProdutoEntity
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int QtdEstoque { get; set; }
}
