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

    public void MapAddCliente(Cliente cliente)
    {
        throw new NotImplementedException();
    }

    public void MapDeleteCliente(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Cliente> MapGetAllClientes()
    {
        throw new NotImplementedException();
    }

    public Cliente MapGetCliente(int id)
    {
        throw new NotImplementedException();
    }

    public void MapUpdateCliente(Cliente cliente)
    {
        throw new NotImplementedException();
    }
}
