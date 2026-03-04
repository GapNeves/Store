using LiteDB;
using Store.Domain.Models;

namespace Store.Infra.Data.NoSql;

public static class LiteDbConfig
{
    public static BsonMapper ConfigureMapper()
    {
        var mapper = new BsonMapper();

        // Configurar Cliente
        mapper.Entity<Cliente>()
            .Id(x => x.Id);

        // Configurar Produto
        mapper.Entity<Produto>()
            .Id(x => x.Id);

        // Configurar Venda
        mapper.Entity<Venda>()
            .Id(x => x.Id)
            .DbRef(x => x.CpfCliente, "clientes");

        // Configurar VendaProduto (embedded document)
        mapper.Entity<VendaProduto>();

        return mapper;
    }
}
