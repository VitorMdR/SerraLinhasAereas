using SerraLinhasAereas.Infra.Data.DAO;
using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Interface;
using System;
using System.Collections.Generic;


namespace SerraLinhasAereas.Infra.Data.Repository
{
    public class ClientesRepository : IClientesRepository
    {
        private ClientesDAO _clientesDAO = new ClientesDAO();

        public void AtualizarClientes(Clientes clienteAtualizado)
        {
            bool cpfValido = Clientes.CPFValido(clienteAtualizado.CPF);

            if (cpfValido)
            {
                if (clienteAtualizado == null)
                {
                    throw new Exception($"Cliente não encontrado.");
                }
                else
                {
                    _clientesDAO.AtualizarCliente(clienteAtualizado);
                }
            }
            else
            {
                throw new Exception($"O CPF {clienteAtualizado.CPF} é inválido.");
            }
            _clientesDAO.AtualizarCliente(clienteAtualizado);
        }

        public Clientes BuscarClientePorCPF(string cpf)
        {
            var clienteBuscado = _clientesDAO.BuscaCLientePorCPF(cpf);
            bool cpfValido = Clientes.CPFValido(cpf);

            if (cpfValido)
            {
                if (clienteBuscado == null)
                {
                    throw new Exception($"O cliente com o CPF {cpf} não foi encontrado.");
                }
                else
                {
                    return clienteBuscado;
                }
            }
            else
            {
                throw new Exception($"O CPF {cpf} é inválido.");
            }
        }

        public List<Clientes> BuscarTodosClientes()
        {
            var listaClientes = _clientesDAO.BuscarTodosClientes();

            if (listaClientes.Count == 0)
            {
                throw new Exception("Nenhum cliente encontrado.");
            }
            return listaClientes;
        }

        public void DeletarCliente(string cpf)
        {
            var clienteBuscado = _clientesDAO.BuscaCLientePorCPF(cpf);
            bool cpfValido = Clientes.CPFValido(cpf);

            if (cpfValido)
            {
                
                if (clienteBuscado == null)
                {
                    throw new Exception($"O cliente com o CPF {cpf} não foi encontrado.");
                }
                else
                {
                    _clientesDAO.DeletarCliente(clienteBuscado);
                }
            }
            else
            {
                throw new Exception($"O CPF {cpf} é inválido.");
            }
        }

        public void RegistrarCLiente(Clientes cliente)
        {
            var clienteBuscado = _clientesDAO.BuscaCLientePorCPF(cliente.CPF);
            bool cpfValido = Clientes.CPFValido(cliente.CPF);

            if (cpfValido)
            {
                if (clienteBuscado == null)
                {
                    _clientesDAO.InserirCliente(cliente);
                }
                else
                {
                    throw new Exception($"O cliente {cliente.Nome} {cliente.Sobrenome} já foi cadastrado.");
                }
            }
            else
            {
                throw new Exception($"O CPF {cliente.CPF} é inválido.");
            }
        }
    }
}
