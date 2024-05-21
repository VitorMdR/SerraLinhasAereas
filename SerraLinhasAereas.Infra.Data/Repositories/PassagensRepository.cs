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
    public class PassagensRepository : IPassagensRepository
    {
        private readonly PassagensDAO _passagensDAO = new PassagensDAO();

        public void AdicionarPassagem(Passagens passagem)
        {
            _passagensDAO.AdicionarPassagem(passagem);
        }

        public void AlterarStatusPassagem(Passagens passagem)
        {
            _passagensDAO.AlterarStatusPassagem(passagem);
        }

        public void AtualizarPassagem(Passagens passagem)
        {
            _passagensDAO.AtualizarPassagem(passagem);
        }

        public List<Passagens> BuscarPassagensPorData(DateTime data)
        {
            return _passagensDAO.BuscarPassagensPorData(data);
        }

        public List<Passagens> BuscarPassagensPorDestino(string destino)
        {
            return _passagensDAO.BuscarPassagensPorDestino(destino);
        }

        public List<Passagens> BuscarPassagensPorOrigem(string origem)
        {
            return _passagensDAO.BuscarPassagensPorOrigem(origem);
        }

        public List<Passagens> BuscarTodasPassagens()
        {
            return _passagensDAO.BuscarTodasPassagens();
        }
    }
}
