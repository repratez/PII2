using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PII
{
    public partial class RegistrarCurso : Form
    {
        public RegistrarCurso()
        {
            InitializeComponent();

            // Inicialize a tabela com as colunas necessárias
            dataTable = new DataTable();
            dataTable.Columns.Add("Curso");
            dataTable.Columns.Add("Disciplina");

            // Defina a fonte de dados do DataGridView
            dataGridView1.DataSource = dataTable;

            // Preencher o datagridCadastrados com os registros existentes no banco
            AtualizarDataGridCadastrados();

            ConfigurarEstiloDataGrid();
            AjustarTamanhoColunas();
            datagridCadastrados.RowPostPaint += datagridCadastrados_RowPostPaint;
        }

        Conexao conexao = new Conexao();

        private DataTable dataTable;

      

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNome_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string nomeCurso = txtNome.Text.Trim();
            string nomeDisciplina = txtDisciplina.Text.Trim();

            if (string.IsNullOrEmpty(nomeCurso) || string.IsNullOrEmpty(nomeDisciplina))
            {
                MessageBox.Show("Por favor, preencha o nome do curso e da disciplina.");
                return;
            }

            // Adicionar os dados ao DataTable (e o DataGridView será atualizado automaticamente)
            dataTable.Rows.Add(nomeCurso, nomeDisciplina);

            // Limpar os campos de texto para o próximo cadastro
            txtNome.Clear();
            txtDisciplina.Clear();
        }

      



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    string nomeCurso = row["Curso"].ToString();
                    string nomeDisciplina = row["Disciplina"].ToString();

                    // Inserir o curso, caso não exista, e obter seu ID
                    int idCurso = conexao.InserirCursoSeNaoExistir(nomeCurso);

                    if (idCurso != -1)
                    {
                        // Inserir a disciplina no banco
                        conexao.InserirDisciplina(nomeDisciplina, idCurso);
                    }
                    else
                    {
                        MessageBox.Show($"Erro ao processar o curso: {nomeCurso}");
                        return;
                    }
                }

                MessageBox.Show("Cursos e disciplinas inseridos com sucesso!");

                // Limpar o DataGrid e o DataTable após o envio
                dataTable.Clear();

                // Atualizar o segundo DataGridView
                AtualizarDataGridCadastrados();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao enviar os dados para o banco: " + ex.Message);
            }
        }

        private void ConfigurarEstiloDataGrid()
        {

            datagridCadastrados.EnableHeadersVisualStyles = false; // Permite a customização dos cabeçalhos

            // Cor de fundo e fonte dos cabeçalhos
            datagridCadastrados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 0);
            datagridCadastrados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            datagridCadastrados.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

            // Estilo das células
            datagridCadastrados.DefaultCellStyle.Font = new Font("Arial", 12);
            datagridCadastrados.DefaultCellStyle.BackColor = Color.White;
            datagridCadastrados.DefaultCellStyle.ForeColor = Color.Black;
            datagridCadastrados.DefaultCellStyle.SelectionBackColor = Color.White;
            datagridCadastrados.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Bordas e alinhamento
            datagridCadastrados.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            datagridCadastrados.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            datagridCadastrados.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

        private void AjustarTamanhoColunas()
        {
            // Ajusta a largura das colunas para se adaptar ao conteúdo
            datagridCadastrados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridCadastrados.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            datagridCadastrados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            datagridCadastrados.ColumnHeadersHeight = 35; // Altura do cabeçalho
        }



        private void SalvarCursosDisciplina(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                string curso = row["Curso"].ToString();
                string disciplina = row["Disciplina"].ToString();
                // Aqui você pode fazer a inserção no banco de dados
                Console.WriteLine($"Curso: {curso}, Disciplina: {disciplina}");
            }
        }

        private void datagridCadastrados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AtualizarDataGridCadastrados()
        {
            try
            {
                // Obter os cursos e disciplinas do banco
                DataTable data = conexao.ObterCursosEDisciplinas();

                // Definir como fonte de dados do DataGridView
                datagridCadastrados.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o DataGrid de cursos e disciplinas: " + ex.Message);
            }
        }

        private void datagridCadastrados_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Verifica se a linha atual é a última linha
            if (e.RowIndex == datagridCadastrados.Rows.Count - 1)
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

        private void button1_Click(object sender, EventArgs e)
        {
            registro reg = new registro();
            reg.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegistroProfessores registro = new RegistroProfessores();
            registro.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RegistrarCurso registrar = new RegistrarCurso();
            registrar.ShowDialog();
        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            
        }

        private void EDITAR_Click(object sender, EventArgs e)
        {
            Editar aux = new Editar();
            aux.ShowDialog();
        }
    }
}
