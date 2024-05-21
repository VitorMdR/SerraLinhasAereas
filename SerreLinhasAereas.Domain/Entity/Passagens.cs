using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerreLinhasAereas.Domain.Entity
{
    public class Passagens
    {
        public int Id { get; set; }

        public string Origem { get; set; }

        public string Destino { get; set; }

        public decimal Valor { get; set; } = 0;

        public DateTime DataOrigem { get; set; }

        public DateTime DataDestino { get; set; }

        public Passagens(int id, string origem, string destino, decimal valor, DateTime dataOrigem, DateTime dataDestino)
        {
            Id = id;
            Origem = origem;
            Destino = destino;
            Valor = valor;
            DataOrigem = dataOrigem;
            DataDestino = dataDestino;

            if(DataOrigem > DataDestino )
            {
                throw new Exception("A data de origem não pode ser menor que de a de destino!");
            }                                                                                                                                                                                                                             
        }

        public Passagens()
        {

        }

        public static bool ValidaDatas(DateTime dataOrigem, DateTime dataDestino)
        {
            return dataDestino >= dataOrigem;
        }

    }

}
