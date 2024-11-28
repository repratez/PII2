using System;
using System.Data.SqlClient;

namespace PII
{
    internal class Professor
    {
        private Conexao objetoConexao = new Conexao();

        public int IdProfessor { get; set; }
        public string NomeProfessor { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int IdDisciplina { get; set; }
        public int IdCurso { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }

        private string formacao;
        public string Formacao { get => formacao; set => formacao = value; }

        public void Incluir()
        {
            string sql = "INSERT INTO Professor (nomeProfessor, email, senha, telefone, idDisciplina, idCurso, endereco, dataNascimento, cpf, rg) " +
                         "VALUES (@nomeProfessor, @Email, @Senha, @Telefone, @IdDisciplina, @IdCurso, @Endereco, @DataNascimento, @Cpf, @Rg)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, objetoConexao.Conectar()))
                {
                    // Adicionar parâmetros
                    cmd.Parameters.AddWithValue("@nomeProfessor", NomeProfessor);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Senha", Senha);
                    cmd.Parameters.AddWithValue("@Telefone", Telefone);
                    cmd.Parameters.AddWithValue("@IdDisciplina", IdDisciplina);
                    cmd.Parameters.AddWithValue("@IdCurso", IdCurso);
                    cmd.Parameters.AddWithValue("@Endereco", Endereco);
                    cmd.Parameters.AddWithValue("@DataNascimento", DataNascimento);
                    cmd.Parameters.AddWithValue("@Cpf", Cpf);
                    cmd.Parameters.AddWithValue("@Rg", Rg);

                    // Executa o comando de inserção
                    cmd.ExecuteNonQuery();
                }
                // Fechar a conexão
                objetoConexao.Desconectar();
            }
            catch (Exception ex)
            {
                // Se houver erro, exibir a mensagem
                throw new Exception("Erro ao inserir professor: " + ex.Message);
            }
        }


    }
}
