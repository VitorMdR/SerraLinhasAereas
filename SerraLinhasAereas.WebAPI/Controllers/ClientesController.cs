using Microsoft.AspNetCore.Mvc;
using SerraLinhasAereas.Infra.Data.Repository;
using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Interface;
using SerreLinhasAereas.Domain.Struct;
using System;

namespace SerraLinhasAereas.WebApi.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : Controller
    {
        private readonly IClientesRepository _clientesRepository = new ClientesRepository();

        [HttpPost]
        public IActionResult PostCliente(Clientes novoCliente)
        {
            try
            {
                _clientesRepository.RegistrarCLiente(novoCliente);
                return Ok(new Respostas(200, "Cliente incluído com sucesso."));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }
        [HttpGet]
        public IActionResult GetClientes()
        {
            try
            {
                return Ok(_clientesRepository.BuscarTodosClientes());

            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }
        }

        [HttpGet("cpf")]
        public IActionResult GetClientePorCpf([FromQuery] string cpf)
        {
            try
            {
                return Ok(_clientesRepository.BuscarClientePorCPF(cpf));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }

        }

        [HttpPut]
        public IActionResult PutCliente([FromBody] Clientes clienteEditado)
        {
            try
            {
                var clienteBuscado = _clientesRepository.BuscarClientePorCPF(clienteEditado.CPF);
                clienteBuscado.Nome = clienteEditado.Nome;
                clienteBuscado.Sobrenome = clienteEditado.Sobrenome;
                var cep = clienteEditado.Endereco.CEP;
                var rua = clienteEditado.Endereco.Rua;
                var bairro = clienteEditado.Endereco.Bairro;
                var numero = clienteEditado.Endereco.Numero;
                var complemento = clienteEditado.Endereco.Complemento;
                var endereco = new Endereco(cep, rua, bairro, numero, complemento);
                clienteBuscado.Endereco = endereco;
                _clientesRepository.AtualizarClientes(clienteBuscado);

                return Ok(new Respostas(200, "Cliente atualizado com sucesso."));
            }

            catch (Exception e)
            {
                return StatusCode(500, new Respostas(500, e.Message));
            }

        }

        [HttpDelete]
        public IActionResult DeleteCliente([FromQuery] string cpf)
        {
            try
            {
                var clienteBuscado = _clientesRepository.BuscarClientePorCPF(cpf);

                _clientesRepository.DeletarCliente(clienteBuscado.CPF);

                return Ok(new Respostas(200, "Cliente excluído com sucesso."));
            }

            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
