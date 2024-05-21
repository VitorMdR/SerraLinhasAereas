using SerraLinhasAereas.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Interfaces
{
    public interface IPassagensRepository
    {
        public void AdicionarPassagem(Passagens passagens);

        List<Passagens> BuscarTodasPassagens();

        List<Passagens> BuscarPassagensPorData(DateTime data);

        List<Passagens> BuscarPassagensPorOrigem(string origem);

        List<Passagens> BuscarPassagensPorDestino(string destino);

        public void AtualizarPassagem(Passagens passagem);

        public void AlterarStatusPassagem(Passagens passagem);
    }
}
