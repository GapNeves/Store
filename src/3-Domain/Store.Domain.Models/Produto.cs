
namespace Store.Domain.Models;
public class Produto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Preco {  get; set; }
    public string QtdEstoque { get; set; }
}
