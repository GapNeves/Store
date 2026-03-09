using LiteDB;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.Infra.Data.NoSql;
public class ClienteRepositoryNoSql : IClienteRepository
{
    private readonly ILiteCollection<Cliente> _clienteCollection;
    private readonly ILiteDatabase _database;

    public ClienteRepositoryNoSql(ILiteDatabase database)
    {
        _database = database;
        _clienteCollection = _database.GetCollection<Cliente>("clientes");
    }

    public void AddCliente(Cliente cliente)
    {
        _clienteCollection.Insert(cliente);
    }

    public void DeleteCliente(Guid id)
    {
        _clienteCollection.Delete(id);
    }

    public IEnumerable<Cliente> GetAllClientes()
    {
        return _clienteCollection.FindAll();
    }

    public Cliente GetClienteById(Guid id)
    {
        return _clienteCollection.FindById(id);
    }

    public void UpdateCliente(Cliente cliente)
    {
        _clienteCollection.Update(cliente);
    }

    public Cliente GetClienteByCpf(string cpf)
    {
        return _clienteCollection.FindOne(c => c.Cpf == cpf);
    }
}
