using SerraLinhasAereas.Infra.Data.DAO;
using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Interface;
using System;
using System.Collections.Generic;

namespace SerraLinhasAereas.Infra.Data.Repository
{
    public class ViagensRepository : IViagensRepository
    {
        private readonly ViagensDAO _viagensDAO = new ViagensDAO();
        private readonly ClientesDAO _clientesDAO = new ClientesDAO();
        private readonly PassagensDAO _passagensDAO = new PassagensDAO();


        public List<Viagens> BuscarTodasViagensPorCliente(string cpf)
        {
            var clienteBuscado = _clientesDAO.BuscaCLientePorCPF(cpf);
            var listaViagens = _viagensDAO.BuscaViagensPorCliente(clienteBuscado);
            return listaViagens;
        }

        public List<Viagens> BuscarViagens()
        {
            var listaViagens = _viagensDAO.BuscaViagens();
            return listaViagens;
        }

        public void MarcarViagem(Viagens viagem)
        {

            var passagemExistenteIda = _passagensDAO.BuscarPassagensPorId(viagem.PassagemIda.Id);
            var passagemExistenteVolta = _passagensDAO.BuscarPassagensPorId(viagem.PassagemVolta.Id);

            if (passagemExistenteIda == null && passagemExistenteVolta == null)
                throw new Exception("A passagem informada é inválida.");

            else
            {
                var listaViagens = _viagensDAO.BuscaViagens();

                var codigoReservaVerificado = Viagens.CodigoValido(viagem.CodigoReserva);
                var codigoReservaExiste = listaViagens.Find(v => v.CodigoReserva == viagem.CodigoReserva);
                var passagemIdaVerificada = listaViagens.Find(viagemIda => viagemIda.PassagemIda.Id == viagem.PassagemIda.Id);
                var cliente = _clientesDAO.BuscaCLientePorCPF(viagem.Cliente.CPF);

                Viagens passagemVoltaVerificada = null;

                bool verificaDatasViagem = true;

                if (viagem.TemVolta)
                {
                    passagemVoltaVerificada = listaViagens.Find(viagemVolta => viagemVolta.PassagemVolta.Id == viagem.PassagemVolta.Id);
                    var passagemIda = _passagensDAO.BuscarPassagensPorId(viagem.PassagemIda.Id);
                    var passagemVolta = _passagensDAO.BuscarPassagensPorId(viagem.PassagemVolta.Id);
                    verificaDatasViagem = Viagens.DataViagemValida(passagemIda, passagemVolta);
                }

                if (!verificaDatasViagem)
                    throw new Exception($"As datas são incompatíveis. Utilize outras passagens.");

                else
                {
                    if (codigoReservaExiste == null && codigoReservaVerificado == true)
                    {
                        if (passagemIdaVerificada == null && passagemVoltaVerificada == null)
                        {
                            if (cliente != null)
                                _viagensDAO.MarcarViagem(viagem);

                            else
                                throw new Exception("Não existe cliente com este CPF");
                        }
                        else
                            throw new Exception("A passagem já foi previamente utilizada. Utilize outra para marcar a viagem.");
                    }
                    else
                        throw new Exception($"O código de reserva {viagem.CodigoReserva} é inválido ou já foi utilizado.");
                }                
            }
        }

        public void RemarcarViagemIda(int idViagem, DateTime dataOrigemIda, DateTime dataDestinoIda)
        {
            var viagemRemarcada = _viagensDAO.BuscaViagensPorId(idViagem);

            if (viagemRemarcada == null)
            {
                throw new Exception("Não é possível remarcar pois a viagem não encontrada.");
            }

            bool passagemValidaIda = Passagens.ValidaDatas(dataOrigemIda, dataDestinoIda);

            if (passagemValidaIda)
            {
                _viagensDAO.RemarcarIda(viagemRemarcada.Id, dataOrigemIda, dataDestinoIda);
            }
            else
            {
                throw new Exception("As datas da remarcação não são válidas.");
            }
        }
        public void RemarcarIdaEVolta(int idViagem, DateTime dataOrigemIda, DateTime dataDestinoIda, DateTime dataOrigemVolta, DateTime dataDestinoVolta)
        {
            var viagemRemarcada = _viagensDAO.BuscaViagensPorId(idViagem);

            if (viagemRemarcada == null)
            {
                throw new Exception("Não é possível remarcar pois a viagem não encontrada.");
            }

            bool validarPassagemIda = Passagens.ValidaDatas(dataOrigemIda, dataDestinoIda);

            bool validarPassagemVolta = Passagens.ValidaDatas(dataOrigemVolta, dataDestinoVolta);

            if (validarPassagemIda && validarPassagemVolta)
            {
                _viagensDAO.RemarcarIda(viagemRemarcada.Id, dataOrigemIda, dataDestinoIda);
                _viagensDAO.RemarcarViagemVolta(viagemRemarcada.Id, dataOrigemVolta, dataDestinoVolta);
            }
            else
            {
                throw new Exception("As datas da remarcação não são válidas.");
            }
        }
    }
}
