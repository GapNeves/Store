using LiteDB;

namespace Store.Infra.Data.NoSql
{
    public class LiteDbContext : IDisposable
    {
        private readonly LiteDatabase _database;

        public LiteDbContext(string connectionString)
        {
            _database = new LiteDatabase(connectionString);
        }

        public ILiteCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
