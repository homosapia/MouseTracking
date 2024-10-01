using Microsoft.AspNetCore.Mvc;
using MouseTracking.Models;
using System.Data.SQLite;

namespace MouseTracking.Services
{
    public class DataBaseServices
    {
        private readonly string _connectionString = "Data Source=mouseData.db;Version=3;";

        public DataBaseServices() 
        {
            using (var connection = new SQLiteConnection(_connectionString))
            using (var command = new SQLiteCommand(connection))
            {
                connection.Open();
                command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS MouseMovement (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            X INTEGER,
                            Y INTEGER,
                            Time INTEGER
                        )";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void SaveData(List<int[]> data)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var command = new SQLiteCommand(connection))
                    {
                        command.CommandText = "INSERT INTO MouseMovement (X, Y, Time) VALUES (@X, @Y, @Time)";
                        command.Parameters.Add(new SQLiteParameter("@X"));
                        command.Parameters.Add(new SQLiteParameter("@Y"));
                        command.Parameters.Add(new SQLiteParameter("@Time"));

                        foreach (var item in data)
                        {
                            command.Parameters["@X"].Value = item[0];
                            command.Parameters["@Y"].Value = item[1];
                            command.Parameters["@Time"].Value = item[2];
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }

                connection.Close();
            }
        }
    }
}
