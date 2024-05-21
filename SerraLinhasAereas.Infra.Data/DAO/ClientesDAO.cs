using System.Collections.Generic;
using System.Data.SqlClient;
using SerreLinhasAereas.Domain.Entity;
using SerreLinhasAereas.Domain.Struct;

namespace SerraLinhasAereas.Infra.Data.DAO
{
    public class ClientesDAO
    {
        private readonly string _connectionString = @"Data Source=.\SQLexpress;initial catalog=SerraLinhasAereasDB;uid=sa;pwd=bocaum24;";

        public ClientesDAO()
        {

        }
        public void InserirCliente(Clientes cliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"INSERT CLIENTES 
                                    VALUES (@CPF, @NOME, @SOBRENOME, 
                                            @CEP, @RUA, @BAIRRO,
                                            @NUMERO, @COMPLEMENTO)";

                    ConverterObjetoParaParametrosSQL(cliente, comando);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public Clientes BuscaCLientePorCPF(string cpf)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); 

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; 

                    string sql = @"SELECT * FROM CLIENTES WHERE CPF = @CPF"; 

                    comando.Parameters.AddWithValue("@CPF", cpf);

                    
                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader(); 

                    while (leitor.Read())
                    {
                        var clienteBuscado = ConverterSqlParaObjeto(leitor); ;
                        return clienteBuscado;
                    }
                }
            }
            return null;
        }

        public List<Clientes> BuscarTodosClientes()
        {
            var listaDeClientes = new List<Clientes>();

            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open(); 

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao; 

                    string sql = @"SELECT * FROM CLIENTES"; 


                    comando.CommandText = sql;

                    SqlDataReader leitor = comando.ExecuteReader(); 

                    while (leitor.Read())
                    {
                        var clienteBuscado = ConverterSqlParaObjeto(leitor);
                        listaDeClientes.Add(clienteBuscado);
                    }

                }
                return listaDeClientes;
            }
            
        }

        public void DeletarCliente(Clientes cliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"DELETE CLIENTES WHERE CPF = @CPF";

                    comando.Parameters.AddWithValue("@CPF", cliente.CPF);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        public void AtualizarCliente(Clientes cliente)
        {
            using (var conexao = new SqlConnection(_connectionString))
            {
                conexao.Open();

                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexao;

                    string sql = @"UPDATE CLIENTES 
                                    SET CPF = @CPF, NOME = @NOME, SOBRENOME = @SOBRENOME, 
                                            CEP = @CEP, RUA = @RUA, BAIRRO = @BAIRRO,
                                           NUMERO = @NUMERO, COMPLEMENTO = @COMPLEMENTO WHERE CPF = @CPF";

                    ConverterObjetoParaParametrosSQL(cliente, comando);

                    comando.CommandText = sql;

                    comando.ExecuteNonQuery();
                }
            }
        }

        private Clientes ConverterSqlParaObjeto(SqlDataReader leitor)
        {
            var cpfcliente = (leitor["CPF"].ToString());
            var nome = leitor["NOME"].ToString();
            var sobrenome = leitor["SOBRENOME"].ToString();
            var cep = leitor["CEP"].ToString();
            var rua = leitor["RUA"].ToString();
            var bairro = leitor["BAIRRO"].ToString();
            var numero = leitor["NUMERO"].ToString();
            var complemento = leitor["COMPLEMENTO"].ToString();

            var endereco = new Endereco(cep, rua, bairro, numero, complemento);

            return new Clientes(cpfcliente, nome, sobrenome, endereco);
        }


        private void ConverterObjetoParaParametrosSQL(Clientes cliente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@NOME", cliente.Nome);
            comando.Parameters.AddWithValue("@SOBRENOME", cliente.Sobrenome);
            comando.Parameters.AddWithValue("@CEP", cliente.Endereco.CEP);
            comando.Parameters.AddWithValue("@RUA", cliente.Endereco.Rua);
            comando.Parameters.AddWithValue("@BAIRRO", cliente.Endereco.Bairro);
            comando.Parameters.AddWithValue("@NUMERO", cliente.Endereco.Numero);
            comando.Parameters.AddWithValue("@COMPLEMENTO", cliente.Endereco.Complemento);
        }
    }
}
