using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerreLinhasAereas.Domain.Entity
{
    public class Viagens
    {
        public int Id { get; set; }

        public string CodigoReserva { get; set; }

        public DateTime DataCompra { get; set; }

        public decimal ValorTotal => SomaValores();

        public Clientes Cliente { get; set; }

        public bool TemVolta { get; set; }

        public Passagens PassagemIda { get; set; }

#nullable enable
        public Passagens? PassagemVolta { get; set; }

        public string ResumoViagem => CriaResumoViagem();

        public Viagens(int id, string codigoReserva, bool temVolta, DateTime dataCompra, Clientes cliente, Passagens passagemIda, Passagens? passagemVolta)
        {
            Id = id;
            CodigoReserva = codigoReserva;
            TemVolta = temVolta;
            DataCompra = dataCompra;
            Cliente = cliente;
            PassagemIda = passagemIda;
            PassagemVolta = passagemVolta;
        }
#nullable disable

        public Viagens()
        {

        }

        internal string CriaResumoViagem()
        {
            if (TemVolta)
            {
                return $"Seu voo de {PassagemIda.Origem} a {PassagemIda.Destino} será dia {PassagemIda.DataDestino.Day} / {PassagemIda.DataDestino.Month} / {PassagemIda.DataDestino.Year} " +
                    $"às {PassagemIda.DataDestino.Hour}:{PassagemIda.DataDestino.Minute}h e seu voo de {PassagemVolta.Origem} a {PassagemVolta.Destino} " +
                    $"será dia {PassagemVolta.DataDestino.Day} / {PassagemVolta.DataDestino.Month} / {PassagemVolta.DataDestino.Year} às {PassagemVolta.DataDestino.Hour}:{PassagemVolta.DataDestino.Minute}h";
            }
            else
            {
                return $"Seu voo de {PassagemIda.Origem} a {PassagemIda.Destino} será dia {PassagemIda.DataDestino.Day} / {PassagemIda.DataDestino.Month} / {PassagemIda.DataDestino.Year} " +
                    $"às {PassagemIda.DataDestino.Hour}:{PassagemIda.DataDestino.Minute}h";
            }
        }

        internal decimal SomaValores()
        {
            return TemVolta ? PassagemIda.Valor + PassagemVolta.Valor : PassagemIda.Valor;
        }

        public static bool DataViagemValida(Passagens passagemIda, Passagens passagemVolta)
        {
            return passagemIda.DataDestino <= passagemVolta.DataOrigem ? true : false;
        }

        public static bool CodigoValido(string codigo)
        {
            return codigo.Length == 6 ? true : false;
        }

    }
}


