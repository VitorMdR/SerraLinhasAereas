using SerraLinhasAereas.Domain.Interfaces;
using SerraLinhasAereas.Domain.Objects;
using SerraLinhasAereas.Infra.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Infra.Data.Repositories
{
    public class ViagensRepository : IViagensRepository
    {
        private readonly ViagensDAO _viagensDAO = new ViagensDAO();

        public List<Viagens> BuscarTodasViagens()
        {
            return _viagensDAO.BuscaTodasViagens();
        }

        public List<Viagens> BuscarViagensPorCliente(Clientes cliente)
        {
            return _viagensDAO.BuscaViagensPorCliente(cliente);
        }

        public void MarcarViagem(Viagens viagem)
        {
            _viagensDAO.MarcarViagem(viagem);
        }

        public void RemarcarViagem(Viagens viagem)
        {
            _viagensDAO.RemarcarViagem();
        }
    }
}
