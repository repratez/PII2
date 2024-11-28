using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PII
{
    public partial class BoletimProfessor : Form
    {

        private Conexao conexao = new Conexao();
        public BoletimProfessor()
        {
            InitializeComponent();
            comboBoxAluno.DisplayMember = "Text";  // Exibe o nome do aluno
            comboBoxAluno.ValueMember = "Value";  // Utiliza o id do aluno
            CarregarDados();

        }

        private Aluno aluno = new Aluno();  // Instanciando a classe Aluno
        private Disciplina disciplina = new Disciplina();

        private void CarregarDados()
        {
            try
            {
                // Carregar alunos
                string sqlAluno = "SELECT idAluno, nomeAluno FROM Aluno";
                SqlConnection conAluno = conexao.Conectar();
                SqlCommand cmdAluno = new SqlCommand(sqlAluno, conAluno);
                SqlDataReader readerAluno = cmdAluno.ExecuteReader();
                while (readerAluno.Read())
                {
                    comboBoxAluno.Items.Add(new { Text = readerAluno["nomeAluno"].ToString(), Value = readerAluno["idAluno"] });
                }
                readerAluno.Close();

                // Carregar disciplinas usando a classe Disciplina
                DataSet dsDisciplinas = disciplina.ListarDisciplinas(); // Usando a classe Disciplina
                foreach (DataRow row in dsDisciplinas.Tables[0].Rows)
                {
                    comboBoxDisciplina.Items.Add(new { Text = row["nomeDisciplina"].ToString(), Value = row["idDisciplina"] });
                }

                conexao.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
            }
        }

        private void BoletimProfessor_Load(object sender, EventArgs e)
        {
            comboBoxAluno.DisplayMember = "nomeAluno";
            comboBoxAluno.ValueMember = "idAluno";
            comboBoxAluno.DataSource = aluno.PesquisaDados().Tables[0];

        }


        private void comboBoxAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAluno.SelectedIndex != -1)  // Verifica se há uma seleção
            {
                try
                {
                    int idAluno = Convert.ToInt32(comboBoxAluno.SelectedValue);
                    aluno.Codigo_Aluno = idAluno;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao obter o id do aluno: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um aluno.");
            }

        }




        private void BoletimProfessor_Load_1(object sender, EventArgs e)
        {

        }

        private void sideBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLancar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se os campos estão preenchidos corretamente
                if (comboBoxAluno.SelectedIndex == -1 || comboBoxDisciplina.SelectedIndex == -1 || string.IsNullOrEmpty(txtNota.Text))
                {
                    MessageBox.Show("Por favor, preencha todos os campos.");
                    return;
                }

                // Recuperar os valores selecionados nos ComboBox e a nota
                int idAluno = (int)comboBoxAluno.SelectedValue;
                int idDisciplina = (int)comboBoxDisciplina.SelectedValue;
                decimal nota = decimal.Parse(txtNota.Text); // Se for numérico, ajustar conforme necessário

                // Criar o comando SQL para inserir ou atualizar o desempenho do aluno
                string sql = "IF EXISTS (SELECT 1 FROM Desempenho WHERE idAluno = @idAluno AND idDisciplina = @idDisciplina) " +
                             "UPDATE Desempenho SET nota = @nota WHERE idAluno = @idAluno AND idDisciplina = @idDisciplina " +
                             "ELSE " +
                             "INSERT INTO Desempenho (idAluno, idDisciplina, nota) VALUES (@idAluno, @idDisciplina, @nota)";

                // Conectar ao banco de dados e executar o comando SQL
                using (SqlConnection conn = conexao.Conectar())
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        // Adicionar os parâmetros ao comando SQL
                        cmd.Parameters.AddWithValue("@idAluno", idAluno);
                        cmd.Parameters.AddWithValue("@idDisciplina", idDisciplina);
                        cmd.Parameters.AddWithValue("@nota", nota);

                        // Executar o comando
                        cmd.ExecuteNonQuery();
                    }
                }

                // Mensagem de sucesso
                MessageBox.Show("Nota registrada com sucesso!");

                // Limpar os campos
                comboBoxAluno.SelectedIndex = -1;
                comboBoxDisciplina.SelectedIndex = -1;
                txtNota.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar a nota: {ex.Message}");
            }
        }
    }
}
