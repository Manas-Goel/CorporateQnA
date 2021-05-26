using Microsoft.Extensions.Configuration;
using PetaPoco;
using System.Collections.Generic;

namespace Services.Helpers
{
    public class Database
    {
        private PetaPoco.IDatabase Connection;
        public static IConfiguration Config;

        public Database(string connectionString,string providerName) 
        {
            CreateSqlConnection(connectionString, providerName);
        }

        private void CreateSqlConnection(string connectionString, string providerName)
        {
            Connection = DatabaseConfiguration.Build()
                        .UsingConnectionString(Config.GetConnectionString(connectionString))
                        .UsingProviderName(providerName)
                        .Create();
        }

        public T SingleOrDefault<T>(string query, params object[] args)
        {
            return Connection.SingleOrDefault<T>(query, args);
        }

        public void Execute(string query, params object[] args)
        {
            Connection.Execute(query, args);
        }

        public IEnumerable<T> Query<T>(string query)
        {
            return Connection.Query<T>(query);
        }

        public IEnumerable<T> Query<T>(string query, params object[] args)
        {
            return Connection.Query<T>(query, args);
        }

        public void BeginTransaction() 
        {
            Connection.BeginTransaction();
        }

        public void CommitTransaction() 
        {
            Connection.CompleteTransaction();
        }

        public void RollbackTransaction()
        {
            Connection.AbortTransaction();
        }
    }
}