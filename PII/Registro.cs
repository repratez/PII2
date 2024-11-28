using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PII
{
    public partial class registro : Form
    {

        Conexao conexao = new Conexao();
        public registro()
        {

            InitializeComponent();
            dataGridAlunos.RowPostPaint += dataGridAlunos_RowPostPaint;
        }

        bool menuExpand = false;

        bool sidebarExpand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {

            if (sidebarExpand)
            {
                sideBar.Width -= 5;
                if (sideBar.Width <= 43)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }
            else
            {
                sideBar.Width += 5;
                if (sideBar.Width >= 209)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }

        }

        private void dataGridAlunos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Verifica se a linha atual é a última linha
            if (e.RowIndex == dataGridAlunos.Rows.Count - 1)
            {
                // Define a cor e a espessura da linha
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    // Posição inicial e final da linha vermelha
                    int xStart = e.RowBounds.Left;
                    int yPosition = e.RowBounds.Bottom - 1;
                    int xEnd = e.RowBounds.Right;

                    // Desenha a linha vermelha abaixo da última linha
                    e.Graphics.DrawLine(pen, xStart, yPosition, xEnd, yPosition);
                }
            }
        }


        private void menuTransition_Tick(object sender, EventArgs e)
        {

            if (menuExpand == false)
            {
                menuContainer.Height += 10;

                if (menuContainer.Height >= 179)
                {
                    menuTransition.Stop();
                    menuExpand = true;

                }

            }

            else
            {
                menuContainer.Height -= 10;
                if (menuContainer.Height <= 44)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }



            }

        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void registro_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<int, string>> cursos = conexao.ObterCursos();
            comboBoxCurso.DisplayMember = "Value";
            comboBoxCurso.ValueMember = "Key";
            comboBoxCurso.DataSource = cursos;

            // Carrega os dados dos alunos
            CarregarDadosAlunos();

            // Configura o estilo do DataGridView
            ConfigurarEstiloDataGrid();
            AjustarTamanhoColunas();
        }

        private void menu_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        private string GerarSenha()
        {
            var random = new Random();
            var senha = new StringBuilder();
            const string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < 8; i++)  // Senha com 8 caracteres
            {
                senha.Append(caracteres[random.Next(caracteres.Length)]);
            }

            return senha.ToString();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Coleta os dados do formulário
                string nome = txtNome.Text.Trim();
                string dataNascimentoStr = txtData.Text.Trim();
                DateTime dataNascimento;

                if (!DateTime.TryParse(dataNascimentoStr, out dataNascimento))
                {
                    MessageBox.Show("Data de nascimento inválida. Por favor, insira uma data válida.");
                    return;
                }

                int idCurso = (int)comboBoxCurso.SelectedValue;
                string endereco = txtEndereco.Text.Trim();
                string email = txtEmail.Text.Trim();
                string telefone = txtTelefone.Text.Trim();  // Pegando o telefone
                string rg = txtRG.Text.Trim();  // Pegando o RG

                // Pega a data da matrícula
                string dataMatriculaStr = txtDataMatricula.Text.Trim();
                DateTime dataMatricula;

                if (!DateTime.TryParse(dataMatriculaStr, out dataMatricula))
                {
                    MessageBox.Show("Data de matrícula inválida. Por favor, insira uma data válida.");
                    return;
                }

                // Pega o CPF do campo de entrada
                string cpf = txtCpf.Text.Trim();

                // Gerar a senha aleatória
                string senhaGerada = GerarSenha();

                // Exibe a senha no popup
                MessageBox.Show($"Senha gerada com sucesso! A senha para o aluno {nome} é: {senhaGerada}",
                                 "Senha Gerada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Agora, insere o aluno e matrícula no banco com a senha gerada
                conexao.InserirAlunoEMatricula(nome, dataNascimento, idCurso, endereco, email, telefone, rg, cpf, dataMatricula, senhaGerada);

                // Limpa os campos após a inserção
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir aluno e matrícula: " + ex.Message);
            }
        }

        

        
        // Método para limpar os campos
        private void LimparCampos()
        {
            txtNome.Clear();
            txtData.Clear();
            comboBoxCurso.SelectedIndex = -1;  // Limpa o ComboBox
            txtEndereco.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtCpf.Clear();
            txtDataMatricula.Clear();
        }
    

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CarregarDadosAlunos()
        {
            DataTable alunos = conexao.ObterAlunos();
            if (alunos.Rows.Count == 0)
            {
                MessageBox.Show("Nenhum aluno encontrado.");
            }
            else
            {
                dataGridAlunos.DataSource = alunos; // Exibe os dados no DataGridView
            }
        }

        private void AjustarTamanhoColunas()
        {
            // Ajusta a largura das colunas para se adaptar ao conteúdo
            dataGridAlunos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridAlunos.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridAlunos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridAlunos.ColumnHeadersHeight = 35; // Altura do cabeçalho
        }

        private void ConfigurarEstiloDataGrid()
        {

            dataGridAlunos.EnableHeadersVisualStyles = false; // Permite a customização dos cabeçalhos

            // Cor de fundo e fonte dos cabeçalhos
            dataGridAlunos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 0);
            dataGridAlunos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridAlunos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

            // Estilo das células
            dataGridAlunos.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridAlunos.DefaultCellStyle.BackColor = Color.White;
            dataGridAlunos.DefaultCellStyle.ForeColor = Color.Black;
            dataGridAlunos.DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridAlunos.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Bordas e alinhamento
            dataGridAlunos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridAlunos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridAlunos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }



        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sideBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UNIFENAS_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtEndereco_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtMatricula_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtData_TextChanged(object sender, EventArgs e)
        {

        }

        private void Anexar_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxCurso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            registro reg = new registro();
            reg.ShowDialog();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridAlunos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNome_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtEndereco_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtRG_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCpf_TextChanged(object sender, EventArgs e)
        {

        }

        private void menu_Click_1(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            registro registro = new registro();
            registro.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistroProfessores registro = new RegistroProfessores();
            registro.ShowDialog();

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            RegistrarCurso registrar = new RegistrarCurso();
            registrar.ShowDialog();
        }
    }
}

