using LiteDB;
using Store.Domain.Models;

namespace Store.Infra.Data.NoSql;

public static class LiteDbConfig
{
    public static BsonMapper ConfigureMapper()
    {
        var mapper = new BsonMapper();

        mapper.RegisterType<decimal>(
            serialize: value => new BsonValue((double)value),
            deserialize: bson => bson.RawValue switch
            {
                double d => (decimal)d,
                int i => (decimal)i,
                long l => (decimal)l,
                decimal dec => dec,
                _ => Convert.ToDecimal(bson.RawValue)
            }
        );

        // Configurar Cliente
        mapper.Entity<Cliente>()
            .Id(x => x.Id);

        // Configurar Produto
        mapper.Entity<Produto>()
            .Id(x => x.Id);

        // Configurar Venda
        mapper.Entity<Venda>()
            .Id(x => x.Id);

        // Configurar VendaProduto (embedded document)
        mapper.Entity<VendaProduto>();

        return mapper;
    }
}
