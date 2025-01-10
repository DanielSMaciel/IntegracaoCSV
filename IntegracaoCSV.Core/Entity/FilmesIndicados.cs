using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace IntegracaoCSV.Core.Entity
{
    public class FilmesIndicados
    {
        public FilmesIndicados()
        {
            
        }
        public FilmesIndicados(string ano, string titulo, string estudio, string produtores, int vencedor)
        {
            Id = Guid.NewGuid();
            Ano = ano;
            Titulo = titulo;
            Estudio = estudio;
            Produtores = produtores;
            Vencedor = vencedor;
        }

        public Guid Id { get; set; }
        public string Ano { get; set; }
        public string Titulo { get; set; }
        public string Estudio { get; set; }
        public string Produtores { get; set; }
        public int Vencedor { get; set; }
        
    }
}
