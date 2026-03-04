using LiteDB;
using Store.Domain.Interfaces;
using Store.Domain.Models;

namespace Store.Infra.Data.NoSql;
public class ClienteRepository : IClienteRepository
{
    private readonly ILiteCollection<Cliente> _clienteCollection;
    private readonly ILiteDatabase _database;

    public ClienteRepository(ILiteDatabase database)
    {
        _database = database;
        _clienteCollection = _database.GetCollection<Cliente>("clientes");
    }

    public void AddCliente(Cliente cliente)
    {
        throw new NotImplementedException();
    }

    public void DeleteCliente(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Cliente> GetAllClientes()
    {
        throw new NotImplementedException();
    }

    public Cliente GetCliente(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateCliente(Cliente cliente)
    {
        throw new NotImplementedException();
    }
}
