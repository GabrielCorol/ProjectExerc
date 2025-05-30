﻿using System.Data;
using MySql.Data.MySqlClient;
using ProjectExerc.Models;

namespace ProjectExerc.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _ConexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Produto(Nome,Descricao,Quantidade,Preco) values(@nome, @descricao, @quantidade, @preco)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                cmd.Parameters.Add("@quantidade", MySqlDbType.VarChar).Value = produto.Quantidade;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                cmd.ExecuteNonQuery();
            }
        }

        public bool Atualizar(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_ConexaoMySQL))
                {
                    conexao.Open();

                    MySqlCommand cmd = new MySqlCommand("Update Produto set Nome=@nome, Descricao=@descricao,Quantidade=@quantidade,Preco=@preco " + " WHERE Id=@Id", conexao);
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = produto.Id;
                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@Descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.Add("@Quantidade", MySqlDbType.VarChar).Value = produto.Quantidade;
                    cmd.Parameters.Add("@Preco", MySqlDbType.Decimal).Value = produto.Preco;
                    int linhasAfestadas = cmd.ExecuteNonQuery();
                    return linhasAfestadas > 0;

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar o produto: {ex.Message}");
                return false;
            }
        }
        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> ProdutoList = new List<Produto>();
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ProdutoList.Add
                        (new Produto
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Nome = ((string)dr["Nome"]),
                        Descricao = ((string)dr["Descricao"]),
                        Quantidade = ((string)dr["Quantidade"]),
                        Preco = ((decimal)dr["Preco"]),
                    });

                }
                return ProdutoList;
            }
        }
        public Produto ObterProduto(int Id)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from Produto where Id=@id ", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Produto produto = new Produto();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.Id = Convert.ToInt32(dr["Id"]);
                    produto.Nome = (string)(dr["Nome"]);
                    produto.Descricao = (string)(dr["Descricao"]);
                    produto.Quantidade = (string)(dr["Quantidade"]);
                    produto.Preco = (decimal)(dr["Preco"]);
                }
                return produto;
            }
        }
        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_ConexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from Produto where Id=@id", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }
    }
}