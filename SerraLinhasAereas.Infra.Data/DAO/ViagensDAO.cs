using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Struct;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Infra.Data.DAO
{
    public class ViagensDAO
    {
        private readonly string _connectionString = @"Data Source=.\SQLexpress;initial catalog=SerraLinhasAereasDB;uid=sa;pwd=bocaum24;";
        public ViagensDAO()
        {

        }

        public void MarcarViagem(Viagens novaViagem)
        {

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"INSERT VIAGENS(CODIGORESERVA, DATACOMPRA, TRAJETO, CLIENTE_ID, PASSAGEMIDA, PASSAGEMVOLTA) 
                                   VALUES (@CODIGORESERVA, @DATACOMPRA, @TRAJETO, @CLIENTE_ID, @PASSAGEMIDA, @PASSAGEMVOLTA);";
                    comando.Parameters.AddWithValue("@CODIGORESERVA", novaViagem.CodigoReserva);
                    comando.Parameters.AddWithValue("@DATACOMPRA", novaViagem.DataCompra);
                    comando.Parameters.AddWithValue("@TRAJETO", novaViagem.TemVolta);
                    comando.Parameters.AddWithValue("@CLIENTE_ID", novaViagem.Cliente.CPF);
                    comando.Parameters.AddWithValue("@PASSAGEMIDA", novaViagem.PassagemIda.Id);
                    comando.Parameters.AddWithValue("@PASSAGEMVOLTA", novaViagem.PassagemVolta == null ? DBNull.Value : novaViagem.PassagemVolta.Id);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Viagens> BuscaViagensPorCliente(Clientes clienteBuscado)
        {
            var listaDeViagens = new List<Viagens>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT V.*, C.*, PI.ID AS IDA_ID, PI.ORIGEM AS IDA_ORIGEM, PI.DESTINO AS IDA_DESTINO,
                                   PI.VALOR AS IDA_VALOR, PI.DATAORIGEM AS IDA_DATA_ORIGEM, PI.DATADESTINO AS IDA_DATA_DESTINO,  
                                   PV.ID AS VOLTA_ID, PV.ORIGEM AS VOLTA_ORIGEM, PV.DESTINO AS VOLTA_DESTINO,
                                   PV.VALOR AS VOLTA_VALOR, PV.DATAORIGEM AS VOLTA_DATA_ORIGEM, PV.DATADESTINO AS VOLTA_DATA_DESTINO                                   
                                   FROM VIAGENS V JOIN CLIENTES C ON V.CLIENTE_ID = C.CPF
                                   INNER JOIN PASSAGENS PI ON PI.ID = V.PASSAGEMIDA 
                                   LEFT JOIN PASSAGENS PV ON PV.ID = V.PASSAGEMVOLTA
                                   WHERE V.CLIENTE_ID = @CPF_DIGITADO;";

                    comando.Parameters.AddWithValue("@CPF_DIGITADO", clienteBuscado.CPF);

                    comando.CommandText = sql;

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var viagem = ConverterSqlParaObjeto(leitor);
                        listaDeViagens.Add(viagem);
                    }
                }
            }
            return listaDeViagens;
        }

        public Viagens BuscaViagensPorId(int idViagem)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"SELECT V.*, C.*, PI.ID AS IDA_ID, PI.ORIGEM AS IDA_ORIGEM, PI.DESTINO AS IDA_DESTINO,
                                   PI.VALOR AS IDA_VALOR, PI.DATAORIGEM AS IDA_DATA_ORIGEM, PI.DATADESTINO AS IDA_DATA_DESTINO,  
                                   PV.ID AS VOLTA_ID, PV.ORIGEM AS VOLTA_ORIGEM, PV.DESTINO AS VOLTA_DESTINO,
                                   PV.VALOR AS VOLTA_VALOR, PV.DATAORIGEM AS VOLTA_DATA_ORIGEM, PV.DATADESTINO AS VOLTA_DATA_DESTINO                                   
                                   FROM VIAGENS V JOIN CLIENTES C ON V.CLIENTE_ID = C.CPF
                                   INNER JOIN PASSAGENS PI ON PI.ID = V.PASSAGEMIDA 
                                   LEFT JOIN PASSAGENS PV ON PV.ID = V.PASSAGEMVOLTA
                                   WHERE V.ID = @ID_DIGITADO";

                    comando.Parameters.AddWithValue("@ID_DIGITADO", idViagem);

                    comando.CommandText = sql;

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var viagem = ConverterSqlParaObjeto(leitor);
                        return viagem;
                    }
                }
            }
            return null;
        }

        public void RemarcarIda(int idViagem, DateTime dataOrigem, DateTime dataDestino)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE PASSAGENS SET DATAORIGEM = @DATAORIGEM, DATADESTINO = @DATADESTINO
                                   FROM PASSAGENS P INNER JOIN VIAGENS V ON P.ID = V.PASSAGEMIDA
                                   WHERE V.ID = @ID_VIAGEM;";
                    comando.Parameters.AddWithValue("@DATAORIGEM", dataOrigem);
                    comando.Parameters.AddWithValue("@DATADESTINO", dataDestino);
                    comando.Parameters.AddWithValue("@ID_VIAGEM", idViagem);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void RemarcarViagemVolta(int idViagem, DateTime dataOrigem, DateTime dataDestino)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;
                    string sql = @"UPDATE PASSAGENS SET DATAORIGEM = @DATAORIGEM, DATADESTINO = @DATADESTINO
                                   FROM PASSAGENS P INNER JOIN VIAGENS V ON P.ID = V.PASSAGEMVOLTA
                                   WHERE V.ID = @ID_VIAGEM;";
                    comando.Parameters.AddWithValue("@DATAORIGEM", dataOrigem);
                    comando.Parameters.AddWithValue("@DATADESTINO", dataDestino);
                    comando.Parameters.AddWithValue("@ID_VIAGEM", idViagem);
                    comando.CommandText = sql;
                    comando.ExecuteNonQuery();
                }
            }
        }

        public List<Viagens> BuscaViagens()
        {
            var listaViagens = new List<Viagens>();
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"SELECT V.*, C.*, PI.ID AS IDA_ID, PI.ORIGEM AS IDA_ORIGEM, PI.DESTINO AS IDA_DESTINO,
                                   PI.VALOR AS IDA_VALOR, PI.DATAORIGEM AS IDA_DATA_ORIGEM, PI.DATADESTINO AS IDA_DATA_DESTINO,  
                                   PV.ID AS VOLTA_ID, PV.ORIGEM AS VOLTA_ORIGEM, PV.DESTINO AS VOLTA_DESTINO,
                                   PV.VALOR AS VOLTA_VALOR, PV.DATAORIGEM AS VOLTA_DATA_ORIGEM, PV.DATADESTINO AS VOLTA_DATA_DESTINO                                   
                                   FROM VIAGENS V JOIN CLIENTES C ON V.CLIENTE_ID = C.CPF
                                   INNER JOIN PASSAGENS PI ON PI.ID = V.PASSAGEMIDA 
                                   LEFT JOIN PASSAGENS PV ON PV.ID = V.PASSAGEMVOLTA";

                    comando.CommandText = sql;

                    var leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        var viagem = ConverterSqlParaObjeto(leitor);
                        listaViagens.Add(viagem);
                    }
                }
            }
            return listaViagens;
        }

        private Viagens ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            var id = Convert.ToInt32(leitor["ID"].ToString());
            var codigo = leitor["CODIGORESERVA"].ToString();
            var dataCompra = DateTime.Parse(leitor["DATACOMPRA"].ToString());
            var idaEVolta = Convert.ToBoolean(leitor["TRAJETO"].ToString());

            var cliente = new Clientes();
            cliente.CPF = leitor["CPF"].ToString();
            cliente.Nome = leitor["NOME"].ToString();
            cliente.Sobrenome = leitor["SOBRENOME"].ToString();
            var cep = leitor["CEP"].ToString();
            var rua = leitor["RUA"].ToString();
            var bairro = leitor["BAIRRO"].ToString();
            var numero = leitor["NUMERO"].ToString();
            var complemento = leitor["COMPLEMENTO"].ToString();
            cliente.Endereco = new Endereco(cep, rua, bairro, numero, complemento);

            var passagemIda = new Passagens();
            passagemIda.Id = Convert.ToInt32(leitor["IDA_ID"].ToString());
            passagemIda.Valor = Convert.ToDecimal(leitor["IDA_VALOR"].ToString());
            passagemIda.Origem = leitor["IDA_ORIGEM"].ToString();
            passagemIda.Destino = leitor["IDA_DESTINO"].ToString();
            passagemIda.DataOrigem = Convert.ToDateTime(leitor["IDA_DATA_ORIGEM"].ToString());
            passagemIda.DataDestino = Convert.ToDateTime(leitor["IDA_DATA_DESTINO"].ToString());

            var passagemVolta = new Passagens();
            if (idaEVolta == true)
            {
                passagemVolta.Id = Convert.ToInt32(leitor["VOLTA_ID"].ToString());
                passagemVolta.Valor = Convert.ToDecimal(leitor["VOLTA_VALOR"].ToString());
                passagemVolta.Origem = leitor["VOLTA_ORIGEM"].ToString();
                passagemVolta.Destino = leitor["VOLTA_DESTINO"].ToString();
                passagemVolta.DataOrigem = Convert.ToDateTime(leitor["VOLTA_DATA_ORIGEM"].ToString());
                passagemVolta.DataDestino = Convert.ToDateTime(leitor["VOLTA_DATA_DESTINO"].ToString());
            }

            return new Viagens(id, codigo, idaEVolta, dataCompra, cliente, passagemIda, passagemVolta);
        }
    }
}
