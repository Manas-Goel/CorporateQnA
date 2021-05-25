using Microsoft.Extensions.Configuration;
using PetaPoco;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Services.Helpers
{
    public static class SqlHelper
    {
        private static PetaPoco.IDatabase Connection;
        public static IConfiguration Config;

        public static void CreateSqlConnection()
        {
            Connection = DatabaseConfiguration.Build()
                        .UsingConnectionString(Config.GetConnectionString("CorporateQnADatabase"))
                        .UsingProviderName("SqlServer")
                        .Create();
        }

        public static T SingleOrDefault<T>(string query, params object[] args)
        {
            CreateSqlConnection();
            return Connection.SingleOrDefault<T>(query, args);
        }

        public static void Execute(string query, params object[] args)
        {
            CreateSqlConnection();
            Connection.Execute(query, args);
        }

        public static IEnumerable<T> Query<T>(string query)
        {
            CreateSqlConnection();
            return Connection.Query<T>(query);
        }

        public static IEnumerable<T> Query<T>(string query, params object[] args)
        {
            CreateSqlConnection();
            return Connection.Query<T>(query, args);
        }


    }
}