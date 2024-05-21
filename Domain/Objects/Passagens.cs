using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Objects
{
    public class Passagens
    {
        public int IdPassagem { get; set; }

        public string Origem { get; set; }

        public string Destino { get; set; }

        public decimal Valor { get; set; } = 0;

        public DateTime DataOrigem { get; set; }

        public DateTime DataDestino { get; set; }

        public bool Reservada { get; set; }

        public Passagens()
        {

        }

        public Passagens(int idPassagem, string origem, string destino, decimal valor, DateTime dataOrigem, DateTime dataDestino)
        {
            IdPassagem = idPassagem;
            Origem = origem;
            Destino = destino;
            Valor = valor;
            DataOrigem = dataOrigem;
            DataDestino = dataDestino;
        }
    }
}
