using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegracaoCSV.Core.Entity;

namespace IntegracaoCSV.Infra.Repository
{
    public interface IIntegracaoCSVRepository
    {
        Task AdicionaFilmeIndicado(FilmesIndicados filme);
    }
}
