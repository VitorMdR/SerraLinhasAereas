using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Objects
{
    public struct Endereco
    {
        public string CEP { get; set; }

        public string Rua { get; set; }

        public string Bairro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public Endereco(string cep, string rua, string bairro, string numero, string complemento)
        {
            CEP = cep;
            Rua = rua;
            Bairro = bairro;
            Numero = numero;
            Complemento = complemento;
        }
    }
}
