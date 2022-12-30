using System.Data;

namespace dispatchservice.api.Database
{
    public interface IDatabase
    {
        string ConnectionString { get; set; }

        IDbConnection CreateOpenConnection();
    }
}