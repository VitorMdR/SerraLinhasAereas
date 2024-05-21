using SerraLinhasAereas.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Interfaces
{
    public interface IClientesRepository
    {
        public void RegistrarCliente(Clientes cliente);

        Clientes BuscarClientePorCPF(string cpf);

        List<Clientes> BuscarTodosClientes();

        Clientes AtualizarCliente(Clientes cliente);

        public void DeletarCLiente(Clientes cliente);
    }
}
