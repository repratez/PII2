using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PII
{
    public partial class Boletim : Form
    {
        private DataTable tabelaDados;

        public Boletim()
        {
            InitializeComponent();
        }

        private void Boletim_Load(object sender, EventArgs e)
        {
            ConfigurarEstiloDataGridView(dataGridView1); // Configura o estilo do DataGridView
            PopularDataGrid(); // Adiciona dados fictícios ao DataGridView
        }

        private void ConfigurarEstiloDataGridView(DataGridView dataGrid)
        {
            dataGrid.EnableHeadersVisualStyles = false;

            // Configuração do cabeçalho
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

            // Estilo das células
            dataGrid.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGrid.DefaultCellStyle.BackColor = Color.White;
            dataGrid.DefaultCellStyle.ForeColor = Color.Black;
            dataGrid.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            dataGrid.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Configuração de bordas e alinhamento
            dataGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Ajustes gerais
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGrid.RowTemplate.Height = 35;
            dataGrid.ColumnHeadersHeight = 40;
        }

        private void PopularDataGrid()
        {
            // Criação de uma tabela para armazenar dados
            tabelaDados = new DataTable();
            tabelaDados.Columns.Add("Matrícula", typeof(string));
            tabelaDados.Columns.Add("Nome", typeof(string));
            tabelaDados.Columns.Add("Matéria", typeof(string));
            tabelaDados.Columns.Add("Nota", typeof(double));

            // Adiciona dados fictícios
            tabelaDados.Rows.Add("12345", "João Silva", "Matemática", 9.5);
            tabelaDados.Rows.Add("67890", "Maria Souza", "História", 8.7);
            tabelaDados.Rows.Add("11223", "Carlos Oliveira", "Química", 7.8);
            tabelaDados.Rows.Add("33456", "Ana Clara", "Matemática", 8.9);

            // Exibe os dados no DataGridView
            dataGridView1.DataSource = tabelaDados;
        }

        private void FiltrarPorMateria(string materia)
        {
            if (tabelaDados != null)
            {
                DataView view = tabelaDados.DefaultView; // Cria uma visualização filtrável
                view.RowFilter = $"Matéria LIKE '%{materia}%'"; // Filtra pela coluna "Matéria"
                dataGridView1.DataSource = view; // Atualiza a exibição no DataGridView
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string materia = txtFiltroMateria.Text.Trim(); // Obtém o texto do TextBox
            FiltrarPorMateria(materia); // Aplica o filtro no DataGridView
        }
    }
}
