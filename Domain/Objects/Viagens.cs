using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Domain.Objects
{
    public class Viagens
    {
        public string CodigoReserva { get; set; }

        public DateTime DataCompra { get; set; }

        public decimal ValorTotal => CalculaValorTotal();

        public bool Trajeto { get; set; }

        public Passagens PassagemIda { get; set; }

        #nullable enable

        public Passagens? PassagemVolta { get; set; }

        public Clientes Cliente { get; set; }

        public string Resumo => GerarResumo();


        public Viagens(string codigoReserva, DateTime dataCompra, decimal valorTotal, bool trajeto, Passagens passagemIda, Passagens passagemVolta, Clientes cliente)
        {
            CodigoReserva = codigoReserva;
            DataCompra = dataCompra;
            Trajeto = trajeto;
            PassagemIda = passagemIda;
            PassagemVolta = passagemVolta;
            Cliente = cliente;
        }

        #nullable disable

        public Viagens()
        {

        }

        public string GerarResumo()
        {
            if (PassagemVolta == null)
                return $"Seu voo de {PassagemIda.Origem} a {PassagemIda.Destino} será dia {PassagemIda.DataOrigem.ToShortDateString()} às {PassagemIda.DataOrigem.ToShortTimeString()}.";
            else
                return $"Seu voo de {PassagemIda.Origem} a {PassagemIda.Destino} será dia {PassagemIda.DataOrigem.ToShortDateString()} às {PassagemIda.DataOrigem.ToShortTimeString()}."
                    + $"Seu voo de {PassagemVolta.Origem} a {PassagemVolta.Destino} será dia {PassagemVolta.DataOrigem.ToShortDateString()} às {PassagemVolta.DataOrigem.ToShortTimeString()}.";
        }

        public decimal CalculaValorTotal()
        {
            if (PassagemVolta == null)
                return PassagemIda.Valor;

            return PassagemIda.Valor + PassagemVolta.Valor;
        }
    }
}
