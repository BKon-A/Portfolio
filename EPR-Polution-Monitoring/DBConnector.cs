﻿using System;
using System.Data;
using Npgsql;

namespace DB_Reports_TPD_Lab_6
{
    public class DBConnector
    {
        public NpgsqlConnection Connection { get; set; }
        string Host { get; } = "localhost";
        int Port { get; set; } = 5432;
        string Username { get; set; }
        string Password { get; set; }
        string Database { get; } = "BiospherePollutionResearch";

        public DBConnector(string username, string password)
        {
            Username = username;
            Password = password;

            Connection = new NpgsqlConnection(GetConnectionString());
        }

        public NpgsqlConnection ConnectionOpen()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Console.WriteLine("Connection opened");
                return Connection;
            }
            Connection.Open();
            return Connection;
        }
        public NpgsqlConnection ConnectionClose()
        { 
            if(Connection.State == ConnectionState.Closed)
            {
                Console.WriteLine("Connection closed");
                return Connection;
            }
            Connection.Close();
            return Connection;
        }

        public string GetConnectionString()
        {
            return $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database}";
        }
    }
}
