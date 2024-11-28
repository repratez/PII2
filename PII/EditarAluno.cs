using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PII
{
    public partial class EditarAluno : Form
    {
        public EditarAluno()
        {
            InitializeComponent();
        }

        private void EditarAluno_Load(object sender, EventArgs e)
        {
            CarregarAlunos();


        }

        private void CarregarAlunos()
        {
            // Cria uma instância da classe Aluno
            Aluno aluno = new Aluno();

            // Obtém a lista de alunos
            List<KeyValuePair<int, string>> alunos = aluno.ObterAlunosParaComboBox();

            // Define os valores e as chaves para o ComboBox
            comboBoxAluno.DisplayMember = "Value"; // Nome do Aluno
            comboBoxAluno.ValueMember = "Key"; // Código do Aluno

            // Preenche o ComboBox com a lista de alunos
            comboBoxAluno.DataSource = alunos;
        }

        private void comboBoxAluno_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica se algum item foi selecionado no ComboBox
            if (comboBoxAluno.SelectedValue != null)
            {
                // Pega o código do aluno selecionado no ComboBox
                int codigoAluno = (int)comboBoxAluno.SelectedValue;

                // Cria uma instância da classe Aluno
                Aluno aluno = new Aluno();
                aluno.Codigo_Aluno = codigoAluno;

                // Obtém os dados do aluno do banco de dados
                DataSet ds = aluno.PesquisaDados();

                // Preenche os campos de texto com os dados do aluno
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    txtNome.Text = row["nomeAluno"].ToString();
                    txtData.Text = Convert.ToDateTime(row["dataNascimento"]).ToString("yyyy-MM-dd");
                    txtEndereco.Text = row["endereco"].ToString();
                    txtEmail.Text = row["email"].ToString();
                    txtTelefone.Text = row["telefone"].ToString();
                    txtRG.Text = row["rg"].ToString();
                    txtCpf.Text = row["cpf"].ToString();
                }
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Limpa os campos antes de preenchê-los
            txtNome.Clear();
            txtData.Clear();
            txtEndereco.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtRG.Clear();
            txtCpf.Clear();

            // Verifica se algum item foi selecionado no ComboBox
            if (comboBoxAluno.SelectedValue != null)
            {
                // Pega o código do aluno selecionado no ComboBox
                int codigoAluno = (int)comboBoxAluno.SelectedValue;

                // Cria uma instância da classe Aluno
                Aluno aluno = new Aluno();
                aluno.Codigo_Aluno = codigoAluno;

                // Obtém os dados do aluno do banco de dados
                DataSet ds = aluno.PesquisaDados();

                // Preenche os campos de texto com os dados do aluno
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    txtNome.Text = row["nomeAluno"].ToString();
                    txtData.Text = Convert.ToDateTime(row["dataNascimento"]).ToString("yyyy-MM-dd");
                    txtEndereco.Text = row["endereco"].ToString();
                    txtEmail.Text = row["email"].ToString();
                    txtTelefone.Text = row["telefone"].ToString();
                    txtRG.Text = row["rg"].ToString();
                    txtCpf.Text = row["cpf"].ToString();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um aluno no ComboBox.");
            }
        }
    }
}


