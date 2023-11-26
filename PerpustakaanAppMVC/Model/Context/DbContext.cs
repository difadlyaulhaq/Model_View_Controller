using System;
using System.Data;
using System.Data.SQLite;

namespace PerpustakaanAppMVC.Model.Context
{
    public class DbContext : IDisposable
    {
        private SQLiteConnection _conn;

        public SQLiteConnection Conn
        {
            get { return _conn ?? (_conn = GetOpenConnection()); }
        }

        private SQLiteConnection GetOpenConnection()
        {
            SQLiteConnection conn = null;
            try
            {
                String dbName = @"D:\Database\DbPerpustakaan.db";
                string connectionString = $"Data Source={dbName};FailIfMissing=True";
                conn = new SQLiteConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print($"Open Connection Error: {ex.Message}");
                return conn;
            }
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                try
                {
                    if (_conn.State != ConnectionState.Closed) _conn.Close();
                }
                finally
                {
                    _conn.Dispose();
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}
