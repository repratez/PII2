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
            CarregarAlunos();  // Para carregar alunos no ComboBox de alunos
            CarregarCursos();

        }

        private void CarregarAlunos()
        {
            // Cria uma instância da classe Aluno
            Aluno aluno = new Aluno();

            // Obtém a lista de alunos
            List<KeyValuePair<int, string>> alunos = aluno.ObterAlunosParaComboBox();

            // Configura o ComboBox
            comboBoxAluno.DisplayMember = "Value"; // Nome do aluno
            comboBoxAluno.ValueMember = "Key"; // Código do aluno

            // Preenche o ComboBox com a lista de alunos
            comboBoxAluno.DataSource = alunos;

            // Desabilita temporariamente o evento SelectedIndexChanged
            comboBoxAluno.SelectedIndexChanged -= comboBoxAluno_SelectedIndexChanged;

            // Define o ComboBox para não ter nenhum item selecionado
            comboBoxAluno.SelectedIndex = -1;

            // Reabilita o evento SelectedIndexChanged
            comboBoxAluno.SelectedIndexChanged += comboBoxAluno_SelectedIndexChanged;
        }

        private void comboBoxAluno_SelectedIndexChanged(object sender, EventArgs e)
        {

            // Verifica se há uma seleção válida no ComboBox (não será nulo ou -1)
            // Verifica se há uma seleção válida no ComboBox (não será nulo ou -1)
            if (comboBoxAluno.SelectedIndex != -1)
            {
                // Aqui, você pode deixar em branco, já que não vai preencher os campos automaticamente.
                // Os campos de texto não serão preenchidos ao selecionar um aluno no ComboBox.

                // Agora apenas obtém o código do aluno selecionado
                int codigoAluno = (int)comboBoxAluno.SelectedValue;

                // Cria uma instância da classe Aluno
                Aluno aluno = new Aluno();
                aluno.Codigo_Aluno = codigoAluno;

                // Você pode adicionar qualquer outra lógica de verificação aqui, caso necessário.
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Verifica se algum aluno foi selecionado no ComboBox
            if (comboBoxAluno.SelectedIndex != -1)
            {
                // Pega o código do aluno selecionado
                int codigoAluno = (int)comboBoxAluno.SelectedValue;

                // Cria uma instância da classe Aluno
                Aluno aluno = new Aluno();
                aluno.Codigo_Aluno = codigoAluno;

                // Verifica os campos de texto e atualiza apenas os campos alterados
                if (!string.IsNullOrEmpty(txtNome.Text))
                {
                    aluno.Nome_Aluno = txtNome.Text;
                }

                if (!string.IsNullOrEmpty(txtData.Text))
                {
                    aluno.Data_Nascimento = DateTime.Parse(txtData.Text);
                }

                if (!string.IsNullOrEmpty(txtEndereco.Text))
                {
                    aluno.Endereco = txtEndereco.Text;
                }

                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    aluno.Email = txtEmail.Text;
                }

                if (!string.IsNullOrEmpty(txtTelefone.Text))
                {
                    aluno.Telefone = txtTelefone.Text;
                }

                if (!string.IsNullOrEmpty(txtRG.Text))
                {
                    aluno.RG = txtRG.Text;
                }

                if (!string.IsNullOrEmpty(txtCpf.Text))
                {
                    aluno.CPF = txtCpf.Text;
                }

                // Chama o método para atualizar os dados do aluno
                aluno.Alterar();

                // Exibe uma mensagem de sucesso
                MessageBox.Show("Aluno atualizado com sucesso!");

                // Limpa os campos após a atualização, ou apenas os mantém para novas edições
                // Aqui você pode escolher se quer limpar os campos ou não
                // txtNome.Clear();
                // txtData.Clear();
                // txtEndereco.Clear();
                // txtEmail.Clear();
                // txtTelefone.Clear();
                // txtRG.Clear();
                // txtCpf.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um aluno para atualizar.");
            }
        }
   




        private void PreencherCampos(int codigoAluno)
        {
            // Cria a instância da classe Aluno
            Aluno aluno = new Aluno();
            aluno.Codigo_Aluno = codigoAluno;

            // Obtém os dados do aluno
            DataSet ds = aluno.PesquisaDados();

            // Verifica se os dados foram retornados
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                // Preenche os campos de texto com os dados do aluno
                txtNome.Text = row["nomeAluno"].ToString();
                txtData.Text = Convert.ToDateTime(row["dataNascimento"]).ToString("yyyy-MM-dd");
                txtEndereco.Text = row["endereco"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtTelefone.Text = row["telefone"].ToString();
                txtRG.Text = row["rg"].ToString();
                txtCpf.Text = row["cpf"].ToString();
            }
            else
            {
                MessageBox.Show("Aluno não encontrado.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Verifica se algum aluno foi selecionado no ComboBox
            if (comboBoxAluno.SelectedIndex != -1)
            {
                // Pega o código do aluno selecionado
                int codigoAluno = (int)comboBoxAluno.SelectedValue;

                // Cria uma instância da classe Aluno
                Aluno aluno = new Aluno();
                aluno.Codigo_Aluno = codigoAluno;

                // Verifica os campos de texto e atualiza apenas os campos alterados
                if (!string.IsNullOrEmpty(txtNome.Text))
                {
                    aluno.Nome_Aluno = txtNome.Text;
                }

                if (!string.IsNullOrEmpty(txtData.Text))
                {
                    aluno.Data_Nascimento = DateTime.Parse(txtData.Text);
                }

                if (!string.IsNullOrEmpty(txtEndereco.Text))
                {
                    aluno.Endereco = txtEndereco.Text;
                }

                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    aluno.Email = txtEmail.Text;
                }

                if (!string.IsNullOrEmpty(txtTelefone.Text))
                {
                    aluno.Telefone = txtTelefone.Text;
                }

                if (!string.IsNullOrEmpty(txtRG.Text))
                {
                    aluno.RG = txtRG.Text;
                }

                if (!string.IsNullOrEmpty(txtCpf.Text))
                {
                    aluno.CPF = txtCpf.Text;
                }

                // Chama o método para atualizar os dados do aluno
                aluno.Alterar();

                // Exibe uma mensagem de sucesso
                MessageBox.Show("Aluno atualizado com sucesso!");

                // Limpa os campos após a atualização, ou apenas os mantém para novas edições
                // Aqui você pode escolher se quer limpar os campos ou não
                // txtNome.Clear();
                // txtData.Clear();
                // txtEndereco.Clear();
                // txtEmail.Clear();
                // txtTelefone.Clear();
                // txtRG.Clear();
                // txtCpf.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um aluno para atualizar.");
            }
        }

        private void comboBoxCurso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CarregarCursos()
        {
            // Cria uma instância da classe Curso
            Curso curso = new Curso();

            // Obtém a lista de cursos
            List<KeyValuePair<int, string>> cursos = curso.ObterCursosParaComboBox();

            // Configura o ComboBox de cursos
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            registro reg = new registro();
            reg.ShowDialog();
        }
    }
}

