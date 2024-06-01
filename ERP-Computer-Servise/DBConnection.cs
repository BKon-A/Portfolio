using Microsoft.Data.SqlClient;
using System.IO;

namespace DBO_AWP_Computer_Service_Director
{
    class DBConnection
    {
        SqlConnection sqlConnection;
        private string _connectionString;
        private string _connectionFileString;
        private string _login;
        private string _password;

        public string Login { get => _login; set => _login = value; }
        public string Password { get => _password; set => _password = value; }
        public string ConnectionFileString { get => _connectionFileString; set => _connectionFileString = value; }
        public string ConnectionString { get => _connectionString; set => _connectionString = value; }

        public DBConnection()
        {
            Login = "";
            Password = "";
            ConnectionFileString = "";
            ConnectionString = "";
        }

        public DBConnection(string login, string password, string connectionFileString, string connectionString)
        {
            Login = login;
            Password = password;
            ConnectionFileString = connectionFileString;
            ConnectionString = connectionString;
        }

        public static string getConnectionFile()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\DBConnectionString";
        }

        public static string getConnectionString(string connectionFile)
        {
            StreamReader streamReader = new StreamReader(connectionFile);

            string connectionString = streamReader.ReadToEnd();
            streamReader.Close();

            return connectionString;
        }

    }
}
