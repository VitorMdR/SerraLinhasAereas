using SerreLinhasAereas.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerreLinhasAereas.Domain.Interface
{
    public interface IPassagensRepository
    {
        void AdicionarPassagem(Passagens passagens);

        List<Passagens> BuscarTodasPassagens();

        List<Passagens> BuscarPassagensPorData(DateTime data);

        List<Passagens> BuscarPassagensPorOrigem(string origem);

        List<Passagens> BuscarPassagensPorDestino(string destino);

        void AtualizarPassagem(Passagens passagem);

        Passagens BuscaPassagensPorId(int id);
    }
}
