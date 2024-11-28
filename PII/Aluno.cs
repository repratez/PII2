using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PII
{
    internal class Aluno
    {

        private Conexao objetoConexao = new Conexao();

        private int codigo_Aluno;
        private string nome_Aluno;
        private DateTime data_Nascimento;
        private string endereco;
        private string email;
        private string telefone;
        private string rg;
        private string cpf;

        // Propriedades (Getters e Setters)
        public int Codigo_Aluno { get => codigo_Aluno; set => codigo_Aluno = value; }
        public string Nome_Aluno { get => nome_Aluno; set => nome_Aluno = value; }
        public DateTime Data_Nascimento { get => data_Nascimento; set => data_Nascimento = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string RG { get => rg; set => rg = value; }
        public string CPF { get => cpf; set => cpf = value; }

        // Método para incluir um novo aluno
        public void Incluir()
        {
            string sql = "Insert into Aluno (Nome_Aluno, Data_Nascimento, Endereco, Email, Telefone, RG, CPF) values ('"
                + Nome_Aluno + "', '" + Data_Nascimento.ToString("yyyy-MM-dd") + "', '" + Endereco + "', '"
                + Email + "', '" + Telefone + "', '" + RG + "', '" + CPF + "')";
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }

        // Método para alterar os dados de um aluno
        public void Alterar()
        {
            string sql = "";
            sql = $"Update Aluno set Nome_Aluno = '{Nome_Aluno}', Data_Nascimento = '{Data_Nascimento.ToString("yyyy-MM-dd")}', Endereco = '{Endereco}', Email = '{Email}', Telefone = '{Telefone}', RG = '{RG}', CPF = '{CPF}' where Codigo_Aluno = " + Codigo_Aluno.ToString();

            // Conecta ao banco e executa o comando
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }

        // Método para excluir um aluno
        public void Excluir()
        {
            string sql = "Delete from Aluno where Codigo_Aluno = " + Codigo_Aluno.ToString();
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }



        // Método para pesquisar os dados dos alunos
        public DataSet PesquisaDados()
        {
            string sql = "Select * from Aluno"; // Adaptado para buscar dados de alunos
            objetoConexao.Conectar();
            DataSet ds = objetoConexao.ListarDados(sql);
            objetoConexao.Desconectar();
            return ds;
        }

        // Método para obter os alunos para o ComboBox
        public List<KeyValuePair<int, string>> ObterAlunosParaComboBox()
        {
            // Consulta corrigida com as colunas corretas do banco de dados
            string sql = "SELECT idAluno, nomeAluno FROM Aluno";

            // Conecta ao banco e executa a consulta
            objetoConexao.Conectar();
            DataTable dt = objetoConexao.ListarDados(sql).Tables[0]; // Obtém o DataTable com os alunos
            objetoConexao.Desconectar();

            // Verifica se o DataTable possui dados
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Nenhum aluno encontrado no banco de dados.");
            }

            // Cria a lista de KeyValuePair para armazenar o Codigo_Aluno e Nome_Aluno
            List<KeyValuePair<int, string>> listaAlunos = new List<KeyValuePair<int, string>>();

            // Preenche a lista com os alunos
            foreach (DataRow row in dt.Rows)
            {
                // Utiliza as colunas corretas: idAluno e nomeAluno
                listaAlunos.Add(new KeyValuePair<int, string>((int)row["idAluno"], (string)row["nomeAluno"]));
            }

            return listaAlunos;
        }

    }
}

