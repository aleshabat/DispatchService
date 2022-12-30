using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using dispatchservice.api.Database;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using System.Data;
using Dapper;

namespace dispatchservice.migrations
{
    public static class Runner
    {
        public class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; set; }
        }

        public static void MigrateToLatest()
        {
            if(File.Exists("test.sqlite"))
                File.Delete("test.sqlite");
            
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var assembly = Assembly.GetAssembly(typeof(V0001));

            var migrationContext = new RunnerContext(announcer) { Namespace = "dispatchservice.migrations" };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new FluentMigrator.Runner.Processors.Sqlite.SqliteProcessorFactory();
            var processor = factory.Create("Data Source=test.sqlite;Version=3;New=True;", announcer, options);
            var runner = new MigrationRunner(assembly, migrationContext, processor);
            runner.MigrateUp(true);
        }

        public static void ImportGuidDB()
        {
            IDatabase db = new SqLiteDatabase(@"Data Source=c:\test.sqlite; Version=3");
            IDbConnection connection = db.CreateOpenConnection();
            IStreetRepository repStreet = new StreetRepository { Connection = connection };
            IHouseRepository repHouse = new HouseRpository { Connection = connection };
            var repEstate = new Repository<Estate> { Connection = connection };

            var streets = repStreet.All().ToList();
            var estates = repEstate.All().ToList();
            var houses = repHouse.All().ToList();
                                                //здесь старый GUID
                                                   // |
                                                   // V
            var compareEstates = new Dictionary<Guid, Estate>();
            var compareStreets = new Dictionary<Guid, Street>();

            connection.Execute("delete from House"); 
            connection.Execute("delete from Estate"); 
            connection.Execute("delete from Street"); 

            foreach (var estate in estates)
            {
                var oldGuid = new Guid(estate.Id.ToString());
                repEstate.Save(estate);

                //var dic = new Dictionary<Guid, Estate> { { oldGuid, estate } };
                compareEstates.Add(oldGuid, estate);
            }

            foreach (var street in streets)
            {
                var oldGuid = new Guid(street.Id.ToString());
                repStreet.Save(street);

                //var dic = new Dictionary<Guid, Street> { { oldGuid, street } };
                compareStreets.Add(oldGuid, street);
            }

            foreach (var house in houses)
            {
                house.Street = compareStreets[house.StreetId];

                if (house.EstateId != null)
                    house.Estate = compareEstates[(Guid)house.EstateId];

                repHouse.Save(house);
            }
            //connection.Execute("update h set h.StreetId = s.Id from House h join Street s on s.AOGUID = h.StreetId");

            connection.Close();
        }
    }
}