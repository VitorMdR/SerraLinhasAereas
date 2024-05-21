using SerreLinhasAereas.Domain.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerreLinhasAereas.Domain.Entity
{
    public class Clientes
    {
        public string CPF { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string NomeCompleto => CriaNomeCompleto();

        public Endereco Endereco { get; set; }

        public Clientes(string cpf, string nome, string sobrenome, Endereco endereco)
        {
            CPF = cpf;
            Nome = nome;
            Sobrenome = sobrenome;
            Endereco = endereco;
        }

        public Clientes()
        {

        }

        private string CriaNomeCompleto()
        {
            return $"{Nome} {Sobrenome}";
        }

        public static bool CPFValido(string cpf)
        {
            return cpf.Length == 11;
        }
    }
}
