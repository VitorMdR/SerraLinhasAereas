using SerraLinhasAereas.Infra.Data.DAO;
using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Interface;
using System;
using System.Collections.Generic;

namespace SerraLinhasAereas.Infra.Data.Repository
{
    public class PassagensRepository : IPassagensRepository
    {
        private readonly PassagensDAO _passagensDAO = new PassagensDAO();
    
        public void AdicionarPassagem(Passagens passagens)
        {
            bool passagemValida = Passagens.ValidaDatas(passagens.DataOrigem, passagens.DataDestino);
            if (passagemValida)
            {
                _passagensDAO.AdicionarPassagem(passagens);
            }
            else
            {
                throw new Exception("As datas da passagem não são válidas.");
            }
        }

        public void AtualizarPassagem(Passagens passagemAtualizada)
        {
            var passagemExistente = _passagensDAO.BuscarPassagensPorId(passagemAtualizada.Id);
            if (passagemExistente == null)
            {
                throw new Exception("Passagem não localizada.");
            }
            _passagensDAO.AtualizarPassagem(passagemAtualizada);
        }

        public List<Passagens> BuscarPassagensPorData(DateTime data)
        {
            var listaPassagens = _passagensDAO.BuscarPassagensPorData(data);
            if (listaPassagens.Count == 0)
            {
                throw new Exception($"Nenhuma passagem localizada {data}.");
            }
            return listaPassagens;
        }

        public List<Passagens> BuscarPassagensPorDestino(string destino)
        {
            var listaPassagens = _passagensDAO.BuscarPassagensPorDestino(destino);
            if (listaPassagens.Count == 0)
            {
                throw new Exception($"Nenhuma passagem localizada para {destino}.");
            }
            return listaPassagens;
        }

        public List<Passagens> BuscarPassagensPorOrigem(string origem)
        {
            var listaPassagens = _passagensDAO.BuscarPassagensPorOrigem(origem);
            if (listaPassagens.Count == 0)
            {
                throw new Exception($"Nenhuma passagem originária de {origem} localizada.");
            }
            return listaPassagens;
        }

        public List<Passagens> BuscarTodasPassagens()
        {
            var listaPassagens = _passagensDAO.BuscarTodasPassagens();
            if (listaPassagens.Count == 0)
            {
                throw new Exception("Nenhuma passagem localizada.");
            }
            return listaPassagens;
        }

        public Passagens BuscaPassagensPorId(int id)
        {
            var passagemPorId = _passagensDAO.BuscarPassagensPorId(id);
            return passagemPorId;
        }
    }
}
