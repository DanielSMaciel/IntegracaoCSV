using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace IntegracaoCSV.Infra
{
    public class DatabaseInitializer
    {
        public static void InitializeDatabase()
        {
            var connectionString = "Data Source=localdb.sqlite";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var createTableCommand = connection.CreateCommand();
                createTableCommand.CommandText = @"
                    CREATE TABLE IF NOT EXISTS FilmesIndicados (
                        Id TEXT PRIMARY KEY,
                        Ano TEXT NOT NULL,
                        Titulo TEXT NOT NULL,
                        Estudio TEXT NOT NULL,
                        Produtores TEXT NOT NULL,
                        Vencedor BOOLEAN NOT NULL
                    );
                ";
                createTableCommand.ExecuteNonQuery();
            }
        }
    }
}
