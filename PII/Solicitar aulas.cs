using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PII
{
    public partial class Solicitar_aulas : Form
    {
        public Solicitar_aulas()
        {
            InitializeComponent();
            SetupDataGrid();
            Conexao conexao = new Conexao();  // Instanciando a classe Conexao
            conexao.CarregarDisciplinasNoComboBox(comboBox1);

        }

       


        // Método para configurar as colunas do DataGridView
        private void SetupDataGrid()
        {
            dataGridAulas.ColumnCount = 5;
            dataGridAulas.AutoGenerateColumns = false;

            // Corrigir a ordem das colunas
            dataGridAulas.Columns[0].Name = "Nome";
            dataGridAulas.Columns[0].DataPropertyName = "Nome";
            dataGridAulas.Columns[1].Name = "Matrícula";
            dataGridAulas.Columns[1].DataPropertyName = "Matricula";
            dataGridAulas.Columns[2].Name = "Matéria";
            dataGridAulas.Columns[2].DataPropertyName = "Materia";
            dataGridAulas.Columns[3].Name = "Email";
            dataGridAulas.Columns[3].DataPropertyName = "Email";
            dataGridAulas.Columns[4].Name = "Data";
            dataGridAulas.Columns[4].DataPropertyName = "Data";

            // Ajustar as colunas para ocupar todo o espaço disponível
            dataGridAulas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridAulas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Customização de bordas e altura de células
            dataGridAulas.RowTemplate.Height = 35; // Altura das células
            dataGridAulas.ColumnHeadersHeight = 40; // Altura do cabeçalho

            // Alinhamento do conteúdo das células
            dataGridAulas.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridAulas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Estilização do cabeçalho e células
            dataGridAulas.EnableHeadersVisualStyles = false;
            dataGridAulas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 0);
            dataGridAulas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridAulas.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

            // Estilo das células
            dataGridAulas.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridAulas.DefaultCellStyle.BackColor = Color.White;
            dataGridAulas.DefaultCellStyle.ForeColor = Color.Black;
            dataGridAulas.DefaultCellStyle.SelectionBackColor = Color.LightBlue; // Cor de fundo quando selecionado
            dataGridAulas.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Bordas das células
            dataGridAulas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridAulas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Formatação da coluna "Data" para exibir somente a data (sem hora)
            dataGridAulas.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy"; // Formato de data (dia/mês/ano)
        }

        // Método para preencher o DataGridView com dados de exemplo
       

        // Definindo a classe que vai armazenar as informações da aula
        public class Aula
        {
            public string Nome { get; set; }
            public string Matricula { get; set; }
            public string Materia { get; set; }
            public string Email { get; set; }
            public DateTime Data { get; set; }
        }

        // Método para desenhar a linha vermelha abaixo do último registro
        private void dataGridAulas_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Verifica se a linha atual é a última linha
            if (e.RowIndex == dataGridAulas.Rows.Count - 1)
            {
                // Define a cor e a espessura da linha
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    // Posição inicial e final da linha
                    int xStart = e.RowBounds.Left;
                    int yPosition = e.RowBounds.Bottom - 1;
                    int xEnd = e.RowBounds.Right;

                    // Desenha a linha vermelha abaixo da última linha
                    e.Graphics.DrawLine(pen, xStart, yPosition, xEnd, yPosition);
                }
            }
        }



        // Outros manipuladores de eventos
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Aqui você pode implementar a lógica que desejar
        }

       




        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Fechar a janela ao clicar no PictureBox
            this.Close();
        }

        private void dataGridAulas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Aqui você pode implementar a lógica ao clicar em uma célula
        }

        private void button10_Click(object sender, EventArgs e)
        {

            string nome = txtNome.Text;  // Nome do aluno
            string materia = comboBox1.SelectedItem?.ToString() ?? "";  // Matéria selecionada
            string email = txtEmail.Text;  // Email do aluno
            DateTime dataAula = monthCalendar1.SelectionStart;  // Data da aula selecionada
            DateTime dataFim = monthCalendar1.SelectionEnd;  // Data de fim da aula

            // Cria a nova aula
            Aula novaAula = new Aula
            {
                Nome = nome,
                Matricula = "12345", // Supondo que você tenha um método para obter a matrícula
                Materia = materia,
                Email = email,
                Data = dataAula
            };

            // Conexão com o banco de dados para inserir a aula
            try
            {
                Conexao conexao = new Conexao();
                int idAluno = conexao.ObterIdAlunoPorNome(nome);  // Método para buscar o ID do aluno
                int idDisciplina = (int)comboBox1.SelectedValue; // ID da disciplina

                conexao.InserirAulaReforco(idAluno, idDisciplina, txtMotivo.Text, dataFim);

                MessageBox.Show("Aula de reforço solicitada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpa os campos
                txtNome.Clear();
                txtEmail.Clear();
                comboBox1.SelectedIndex = -1;
                monthCalendar1.SetDate(DateTime.Now);  // Reseta o calendário

                // Atualiza o DataGridView
                AtualizarDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao solicitar aula de reforço: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para atualizar o DataGridView
        private void AtualizarDataGrid()
        {
            // Recarregar as aulas no DataGridView
            List<Aula> aulas = new List<Aula>(); // Recarregar os dados da aula do banco de dados

            // Inserir o código necessário para buscar os dados de AulaReforco do banco de dados

            dataGridAulas.DataSource = null;
            dataGridAulas.DataSource = aulas;
        }
   










        private void button2_Click(object sender, EventArgs e)
        {
     
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeAluno aux = new HomeAluno();
            aux.ShowDialog();
        }

        bool menuExpand = false;
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
        bool sidebarExpand = false;

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

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void menu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ouvidoria ouvidoria = new Ouvidoria();
            ouvidoria.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

     

       
    }
}
