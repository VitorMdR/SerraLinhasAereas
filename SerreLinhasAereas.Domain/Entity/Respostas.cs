using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerreLinhasAereas.Domain.Entity
{
    public class Respostas
    {
        public int Status { get; set; }

        public string Mensagem { get; set; }

        public Respostas(int status, string mensagem)
        {
            this.Status = status;
            this.Mensagem = mensagem;
        }

        public Respostas()
        {

        }
    }
}
