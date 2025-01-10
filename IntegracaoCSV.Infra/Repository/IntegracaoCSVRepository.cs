using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using IntegracaoCSV.Core.Entity;
using Microsoft.Data.Sqlite;

namespace IntegracaoCSV.Infra.Repository
{
    public class IntegracaoCSVRepository : IIntegracaoCSVRepository
    {
        private readonly string _connectionString = "Data Source=localdb.sqlite";

        public IntegracaoCSVRepository()
        {

        }

        public async Task AdicionaFilmeIndicado(FilmesIndicados filme)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = @"
                INSERT INTO FilmesIndicados (Id,
                                   Ano,
                                   Titulo,
                                   Estudio,
                                   Produtores,
                                   Vencedor)
                VALUES ($Id,
                        $Ano,
                        $Titulo,
                        $Estudio,
                        $Produtores,
                        $Vencedor);
            ";
                insertCommand.Parameters.AddWithValue("$Id", filme.Id.ToString());
                insertCommand.Parameters.AddWithValue("$Ano", filme.Ano);
                insertCommand.Parameters.AddWithValue("$Titulo", filme.Titulo);
                insertCommand.Parameters.AddWithValue("$Estudio", filme.Estudio); 
                insertCommand.Parameters.AddWithValue("$Produtores", filme.Produtores);
                insertCommand.Parameters.AddWithValue("$Vencedor", filme.Vencedor);

                await insertCommand.ExecuteNonQueryAsync();
            }
        }
    }
}
