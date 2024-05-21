using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Struct;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerraLinhasAereas.Infra.Data.DAO
{
    public class PassagensDAO
    {
        private readonly string _connectionString = @"Data Source=.\SQLexpress;initial catalog=SerraLinhasAereasDB;uid=sa;pwd=bocaum24;";

        public PassagensDAO()
        {

        }

        public void AdicionarPassagem(Passagens passagens)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT PASSAGENS 
                                    VALUES (@ORIGEM, @DESTINO, 
                                            @VALOR, @DATAORIGEM, @DATADESTINO)";

                    ConverterObjetoParaParametrosSQL(passagens, comando);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarPassagem(Passagens passagens)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE PASSAGENS 
                                    SET ORIGEM = @ORIGEM, DESTINO = @DESTINO, 
                                            VALOR = @VALOR, DATAORIGEM = @DATAORIGEM, DATADESTINO = @DATADESTINO
                                            WHERE ID = @ID";
                    
                    comando.Parameters.AddWithValue("@ID", passagens.Id);

                    ConverterObjetoParaParametrosSQL(passagens, comando);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public Passagens BuscarPassagensPorId(int id)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); 

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; 

                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATAORIGEM, DATADESTINO FROM PASSAGENS
                                   WHERE ID = @ID";

                    comando.Parameters.AddWithValue("@ID", id);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader(); 

                    while (leitor.Read())
                    {
                        var passagem = ConverterSqlParaObjeto(leitor);
                        return passagem;
                    }
                }
                return null;
            }
        }

        public List<Passagens> BuscarPassagensPorData(DateTime data)
        {
            var listaDePassagens = new List<Passagens>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); 

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; 

                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATAORIGEM, DATADESTINO FROM PASSAGENS
                                   WHERE CONVERT(DATE, DATAORIGEM) = CONVERT(DATE, @DATABUSCADA) OR
                                   CONVERT(DATE, DATADESTINO) = CONVERT(DATE, @DATABUSCADA)";

                    comando.Parameters.AddWithValue("@DATABUSCADA", data);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader(); 

                    while (leitor.Read())
                    {
                        var passagem = ConverterSqlParaObjeto(leitor);
                        listaDePassagens.Add(passagem);
                    }
                }
                return listaDePassagens;
            }
        }

        public List<Passagens> BuscarPassagensPorDestino(string destino)
        {
            var listaDePassagens = new List<Passagens>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); 

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; 

                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATAORIGEM, DATADESTINO FROM PASSAGENS
                                   WHERE DESTINO = @DESTINO";

                    comando.Parameters.AddWithValue("@DESTINO", destino);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader(); 

                    while (leitor.Read())
                    {
                        var passagem = ConverterSqlParaObjeto(leitor);
                        listaDePassagens.Add(passagem);
                    }
                }
                return listaDePassagens;
            }
        }

        public List<Passagens> BuscarPassagensPorOrigem(string origem)
        {
            var listaDePassagens = new List<Passagens>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); 

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; 

                    string sql = @"SELECT ID, ORIGEM, DESTINO, VALOR, DATAORIGEM, DATADESTINO FROM PASSAGENS
                                   WHERE ORIGEM = @ORIGEM";

                    comando.Parameters.AddWithValue("@ORIGEM", origem);

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader(); 

                    while (leitor.Read())
                    {
                        var passagem = ConverterSqlParaObjeto(leitor);
                        listaDePassagens.Add(passagem);
                    }
                }
                return listaDePassagens;
            }
        }

        public List<Passagens> BuscarTodasPassagens()
        {
            var listaDePassagens = new List<Passagens>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); 

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; 

                    string sql = @"SELECT * FROM PASSAGENS";

                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader(); 

                    while (leitor.Read())
                    {

                        var passagem = ConverterSqlParaObjeto(leitor);
                        listaDePassagens.Add(passagem);
                    }
                }
                return listaDePassagens;
            }
        }

        private Passagens ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            var id = int.Parse(leitor["ID"].ToString());
            var origem = leitor["ORIGEM"].ToString();
            var destino = leitor["DESTINO"].ToString();
            var valor = decimal.Parse(leitor["VALOR"].ToString());
            var dataOrigem = DateTime.Parse(leitor["DATAORIGEM"].ToString());
            var dataDestino = DateTime.Parse(leitor["DATADESTINO"].ToString());

            return new Passagens(id, origem, destino, valor, dataOrigem, dataDestino);
        }

        private void ConverterObjetoParaParametrosSQL(Passagens passagens, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@ORIGEM", passagens.Origem);
            comando.Parameters.AddWithValue("@DESTINO", passagens.Destino);
            comando.Parameters.AddWithValue("@VALOR", passagens.Valor);
            comando.Parameters.AddWithValue("@DATAORIGEM", passagens.DataOrigem);
            comando.Parameters.AddWithValue("@DATADESTINO", passagens.DataDestino);
        }
    }
}
