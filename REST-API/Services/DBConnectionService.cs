using MySql.Data.MySqlClient;
using System.Data;

namespace REST_API.Services
{
    public class DBConnectionService
    {
        public DBConnectionService()
        {

        }

        // Change this with your database credentials
        private static string ConnectionString = "server=servername;user=username;database=databasename;port=3306;password=password";

        private MySqlConnection conn = new MySqlConnection(ConnectionString);

        public ConnectionState GetConnectionStatus { get { return conn.State; } }

        public void OpenConnection() { conn.Open(); }
        public void CloseConnection() { conn.Close(); }
        public MySqlConnection GetConnection { get { return conn; } }
    }
}
