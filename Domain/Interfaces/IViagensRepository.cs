using SerraLinhasAereas.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Interfaces
{
    public interface IViagensRepository
    {
        public void MarcarViagem(Viagens viagem);

        public List<Viagens> BuscarTodasViagens();

        public List<Viagens> BuscarViagensPorCliente(Clientes cliente);

        public void RemarcarViagem(Viagens viagem);
    }
}
