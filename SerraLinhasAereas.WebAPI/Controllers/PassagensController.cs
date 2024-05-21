using Microsoft.AspNetCore.Mvc;
using SerraLinhasAereas.Infra.Data.Repository;
using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Interface;
using System;

namespace SerraLinhasAereas.WebApi.Controllers
{
    [ApiController]
    [Route("api/passagens")]
    public class PassagensController : Controller
    {
        private readonly IPassagensRepository _passagensRepository = new PassagensRepository();
        

        [HttpPost]
        public IActionResult PostPassagem(Passagens novaPassagem)
        {
            try
            {
                _passagensRepository.AdicionarPassagem(novaPassagem);
                return Ok(new Respostas(200, $"Passagem criada com sucesso."));

            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpGet]
        public IActionResult GetPassagens()
        {
            try
            {
                return Ok(_passagensRepository.BuscarTodasPassagens());
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpGet("data")]
        public IActionResult GetPassagemPorData([FromQuery] DateTime dataBuscada)
        {
            try
            {
                return Ok(_passagensRepository.BuscarPassagensPorData(dataBuscada));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpGet("origem")]
        public IActionResult GetPassagemPorOrigem([FromQuery] string origem)
        {
            try
            {
                return Ok(_passagensRepository.BuscarPassagensPorOrigem(origem));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpGet("destino")]
        public IActionResult GetPassagemPorDestino([FromQuery] string destino)
        {
            try
            {
                return Ok(_passagensRepository.BuscarPassagensPorDestino(destino));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpPut]
        public IActionResult PutPassagem([FromBody] Passagens passagemAtualizada)
        {
            try
            {
                var passagemBuscada = _passagensRepository.BuscaPassagensPorId(passagemAtualizada.Id);
                passagemBuscada.Origem = passagemAtualizada.Origem;
                passagemBuscada.Destino = passagemAtualizada.Destino;
                passagemBuscada.Destino = passagemAtualizada.Destino;
                passagemBuscada.Valor = passagemAtualizada.Valor;
                passagemBuscada.DataOrigem = passagemAtualizada.DataOrigem;
                passagemBuscada.DataDestino = passagemAtualizada.DataDestino;
                _passagensRepository.AtualizarPassagem(passagemBuscada);
                return Ok(new Respostas(200, $"Passagem {passagemBuscada.Id} atualizada com sucesso."));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }
    }
}
