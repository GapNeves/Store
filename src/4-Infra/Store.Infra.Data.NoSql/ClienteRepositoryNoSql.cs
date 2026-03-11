using LiteDB;
using Store.Domain.Models;
using Store.Infra.Interfaces;

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
        try
        {
            _clienteCollection.Insert(cliente);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void DeleteCliente(Guid id)
    {
        try
        {
            _clienteCollection.Delete(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public IEnumerable<Cliente> GetAllClientes()
    {
        try
        {
            return _clienteCollection.FindAll();
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Cliente GetClienteById(Guid id)
    {
        try
        {
            return _clienteCollection.FindById(id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public void UpdateCliente(Cliente cliente)
    {
        try
        {
            _clienteCollection.Update(cliente);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }

    public Cliente GetClienteByCpf(string cpf)
    {
        try
        {
            return _clienteCollection.FindOne(c => c.Cpf == cpf);
        }
        catch (Exception ex)
        {
            throw new ApplicationException(ex.Message, ex);
        }
    }
}
