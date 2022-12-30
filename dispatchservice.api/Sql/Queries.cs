using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace dispatchservice.api.Sql
{
    internal static class Queries
    {
        internal class Houses
        {
            internal static string All { get { return GetQuery("dispatchservice.api.Sql.Houses.All.sql"); } }
            internal static string Get { get { return GetQuery("dispatchservice.api.Sql.Houses.Get.sql"); } }
            internal static string Find { get { return GetQuery("dispatchservice.api.Sql.Houses.Find.sql"); } }
            internal static string GetList { get { return GetQuery("dispatchservice.api.Sql.Houses.GetList.sql"); } }

        }

        internal class Street
        {
            internal static string GetList { get { return GetQuery("dispatchservice.api.Sql.Street.GetList.sql"); } }

        }

        internal class InjenerHouses
        {
            internal static string All { get { return GetQuery("dispatchservice.api.Sql.InjenerHouses.All.sql"); } }
            internal static string Get { get { return GetQuery("dispatchservice.api.Sql.InjenerHouses.Get.sql"); } }
            internal static string GetByHouse { get { return GetQuery("dispatchservice.api.Sql.InjenerHouses.GetByHouse.sql"); } }
            internal static string GetByInjener { get { return GetQuery("dispatchservice.api.Sql.InjenerHouses.GetByInjener.sql"); } }
            internal static string DeleteByInjener { get { return GetQuery("dispatchservice.api.Sql.InjenerHouses.DeleteByInjener.sql"); } }

        }

        internal class Tickets
        {
            internal static string All { get { return GetQuery("dispatchservice.api.Sql.Tickets.All.sql"); } }
            internal static string Get { get { return GetQuery("dispatchservice.api.Sql.Tickets.Get.sql"); } }
            internal static string GetByDateRange { get { return GetQuery("dispatchservice.api.Sql.Tickets.GetByDateRange.sql"); } }
            internal static string GetByHouse { get { return GetQuery("dispatchservice.api.Sql.Tickets.GetByHouse.sql"); } }

        }

        internal class Reports
        {
            internal static string GetReportQuery(string reportName)
            {
                return GetQuery(string.Format("dispatchservice.api.Sql.Reports.{0}.sql", reportName));
            } 

        }

        #region get query from resource file
        private static readonly Dictionary<string, string> Sql = new Dictionary<string, string>();

        private static string GetQuery(string name)
        {
            if (Sql.ContainsKey(name))
            {
                return Sql[name];
            }

            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(name))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                Sql[name] = result;

                return result;
            }

        }
        #endregion
    }
}

