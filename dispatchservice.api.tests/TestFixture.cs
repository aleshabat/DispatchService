using System.Data;
using dispatchservice.api.Database;
using dispatchservice.migrations;
using NUnit.Framework;

namespace dispatchservice.api.tests
{
    public abstract class TestFixture
    {
        public IDbConnection Connection;

        /// <summary>
        /// SetUp-метод для тестов
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            IDatabase db = new SqLiteDatabase(@"Data Source=test.sqlite; Version=3");
            //DapperExtensions.DapperExtensions.Configure(typeof(ProdAutoClassMapper<>), new List<Assembly>(), new SqliteDialect());
            Runner.MigrateToLatest();

            Connection = db.CreateOpenConnection();
            //Connection = new SQLiteConnection(@"Data Source=test.sqlite; Version=3");
            //Connection.Open();
        }


        /// <summary>
        /// Запускается после выполнения тестов
        /// </summary>
        [TearDown]
        public void TearDown()
        {
          Connection.Close();
        }
    }
}
