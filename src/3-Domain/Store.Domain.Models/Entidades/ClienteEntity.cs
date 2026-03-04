namespace Store.Domain.Models.Entidades;
public class ClienteEntity
{
    public Guid Id { get; set; }
    public int Cpf { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}
