using System;
using System.Data.OleDb;

namespace FitnessCenterApp.DataAccess
{
    public static class DatabaseConnection
    {
        private static readonly string _connectionString =
    $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={AppDomain.CurrentDomain.BaseDirectory}db.mdb;";
        public static OleDbConnection GetConnection()
        {
            return new OleDbConnection(_connectionString);
        }
    }
}