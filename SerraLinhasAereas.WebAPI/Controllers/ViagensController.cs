using Microsoft.AspNetCore.Mvc;
using SerraLinhasAereas.Infra.Data.Repository;
using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Interface;
using System;

namespace SerraLinhasAereas.WebApi.Controllers
{
    [ApiController]
    [Route("api/viagens")]
    public class ViagensController : Controller
    {
        private readonly IViagensRepository _viagensRepository = new ViagensRepository();

        [HttpPost]
        public IActionResult PostViagem([FromBody] Viagens novaViagem)
        {
            try
            {
                _viagensRepository.MarcarViagem(novaViagem);
                return Ok(new Respostas(200, $"Sua viagem foi marcada com sucesso, seu código de reserva é {novaViagem.CodigoReserva}."));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpGet("cliente")]
        public IActionResult GetViagensPorCliente([FromQuery] string cpf)
        {
            try
            {
                return Ok(_viagensRepository.BuscarTodasViagensPorCliente(cpf));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpPatch("ida")]
        public IActionResult PatchViagemIda([FromQuery] int idViagem, [FromQuery] DateTime dataOrigem, DateTime dataDestino)
        {
            try
            {
                _viagensRepository.RemarcarViagemIda(idViagem, dataOrigem, dataDestino);
                return Ok(new Respostas(200, $"Viagem {idViagem} remarcada, passagem de ida alterada com sucesso. Agora você sairá em {dataOrigem} e chegará em {dataDestino}."));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpPatch("idaEVolta")]
        public IActionResult PatchViagemIdaEVolta([FromQuery] int idViagem, [FromQuery] DateTime dataOrigemIda, DateTime dataDestinoIda, DateTime dataOrigemVolta, DateTime dataDestinoVolta)
        {
            try
            {
                _viagensRepository.RemarcarIdaEVolta(idViagem, dataOrigemIda, dataDestinoIda, dataOrigemVolta, dataDestinoVolta);
                return Ok(new Respostas(200, $"Viagem {idViagem} remarcada, passagem de ida alterada com sucesso. Agora você sairá em {dataOrigemIda} de e voltará em {dataOrigemVolta}."));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }
    }
}
