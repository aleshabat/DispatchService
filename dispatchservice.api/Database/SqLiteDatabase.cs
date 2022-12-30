using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using DapperExtensions.Sql;
using dispatchservice.api.Domain.Maps;

namespace dispatchservice.api.Database
{
    public class SqLiteDatabase : IDatabase
    {
        public SqLiteDatabase(string connectionString)
        {
            ConnectionString = connectionString;
            DapperExtensions.DapperExtensions.Configure(typeof(ProdAutoClassMapper<>), new List<Assembly>(), new SqliteDialect());
        }

        public string ConnectionString { get; set; }

        public IDbConnection CreateOpenConnection()
        {
            var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
