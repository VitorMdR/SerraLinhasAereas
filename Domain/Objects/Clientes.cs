using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Objects
{
    public class Clientes
    {
        public string CPF { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public Endereco Endereco { get; set; }

        public string NomeCompleto
        {
            get
            {
                return $"{Nome} {Sobrenome}";
            }
        }

        public Clientes()
        {

        }

        public Clientes(string cpf, string nome, string sobrenome, Endereco endereco)
        {
            CPF = cpf;
            Nome = nome;
            Sobrenome = sobrenome;
            Endereco = endereco;
        }
    }
}
