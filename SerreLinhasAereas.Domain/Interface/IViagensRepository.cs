using SerreLinhasAereas.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerreLinhasAereas.Domain.Interface
{
    public interface IViagensRepository
    {
        void MarcarViagem(Viagens viagem);

        List<Viagens> BuscarViagens();

        List<Viagens> BuscarTodasViagensPorCliente(string cpf);

        void RemarcarViagemIda(int idViagem, DateTime dataOrigem, DateTime dataDestino);

        void RemarcarIdaEVolta(int idViagem, DateTime dataOrigemIda, DateTime dataDestinoIda, DateTime dataOrigemVolta, DateTime dataDestinoVolta);
    }
}
