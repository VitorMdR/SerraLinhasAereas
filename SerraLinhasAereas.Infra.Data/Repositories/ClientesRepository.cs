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

    public class ClientesRepository : IClientesRepository
    {
        private readonly ClientesDAO _clientesDAO = new ClientesDAO();

        public void RegistrarCliente(Clientes cliente)
        {
            _clientesDAO.RegistrarCliente(cliente);
        }

        public Clientes AtualizarCliente(Clientes cliente)
        {
            return _clientesDAO.AtualizarCLiente(cliente);
        }

        public Clientes BuscarClientePorCPF(string cpf)
        {
            return _clientesDAO.BuscarPorCPF(cpf);
        }

        public List<Clientes> BuscarTodosClientes()
        {
            return _clientesDAO.BuscarTodosClientes();
        }

        public void DeletarCLiente(Clientes cliente)
        {
            _clientesDAO.DeletarCliente(cliente);
        }
    }
}
