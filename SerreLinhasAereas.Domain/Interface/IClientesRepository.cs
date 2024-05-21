using SerreLinhasAereas.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerreLinhasAereas.Domain.Interface
{
    public interface IClientesRepository
    {
        void RegistrarCLiente(Clientes cliente);

        Clientes BuscarClientePorCPF(string cpf);

        List<Clientes> BuscarTodosClientes();

        void AtualizarClientes(Clientes cliente);

        void DeletarCliente(string cpf);
    }
}
