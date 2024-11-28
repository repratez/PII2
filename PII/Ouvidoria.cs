using System;
using System.Drawing;
using System.Windows.Forms;

namespace PII
{
    public partial class Ouvidoria : Form
    {

        private ConexaoNeo4j _conexaoNeo4j;

        public Ouvidoria()
        {
            InitializeComponent();
            _conexaoNeo4j = new ConexaoNeo4j("neo4j+s://b91f0365.databases.neo4j.io:7687", "neo4j", "lVzihGZt8f77f_Puc3o7K0gBFAfrMZegd5jGq5pBNsM");
        }

        private void Ouvidoria_Load(object sender, EventArgs e)
        {
            // Configurações adicionais, se necessário, podem ser feitas aqui
        }

        private void menu_Click(object sender, EventArgs e)
        {
            // Lógica para o clique no menu (se necessário)
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Fecha a janela ao clicar no pictureBox2
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    

    private async void button10_Click(object sender, EventArgs e)
    {
            // Obtendo os valores dos campos no formulário
            string tipoFeedback = radioButton1.Checked ? "Positivo" :
                                  radioButton2.Checked ? "Negativo" :
                                  radioButton3.Checked ? "Sugestão" : "";

            string dataSelecionada = textBox3.Text;
            string matricula = textBox2.Text;
            string descricao = textBox1.Text;

            // Validação simples dos campos
            if (string.IsNullOrWhiteSpace(tipoFeedback) ||
                string.IsNullOrWhiteSpace(dataSelecionada) ||
                string.IsNullOrWhiteSpace(matricula) ||
                string.IsNullOrWhiteSpace(descricao))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Envia os dados ao banco Neo4j
                await _conexaoNeo4j.RegistrarFeedbackAsync(tipoFeedback, dataSelecionada, matricula, descricao);
                MessageBox.Show("Feedback enviado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpar os campos após o envio
                textBox3.Clear();
                textBox2.Clear();
                textBox1.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar feedback: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LimparCampos()
        {
            foreach (var control in this.Controls)
            {
                if (control is TextBox)
                {
                    (control as TextBox).Text = string.Empty;
                }
                else if (control is ComboBox)
                {
                    (control as ComboBox).SelectedIndex = -1; // Limpar ComboBox
                }
                else if (control is CheckBox)
                {
                    (control as CheckBox).Checked = false; // Desmarcar CheckBox
                }
                else if (control is RadioButton)
                {
                    (control as RadioButton).Checked = false; // Desmarcar RadioButton
                }
                else if (control is GroupBox)
                {
                    // Limpar os controles dentro do GroupBox
                    foreach (Control groupControl in (control as GroupBox).Controls)
                    {
                        if (groupControl is TextBox)
                        {
                            (groupControl as TextBox).Text = string.Empty; // Limpar TextBox dentro do GroupBox
                        }
                        else if (groupControl is ComboBox)
                        {
                            (groupControl as ComboBox).SelectedIndex = -1; // Limpar ComboBox dentro do GroupBox
                        }
                        else if (groupControl is CheckBox)
                        {
                            (groupControl as CheckBox).Checked = false; // Desmarcar CheckBox dentro do GroupBox
                        }
                        else if (groupControl is RadioButton)
                        {
                            (groupControl as RadioButton).Checked = false; // Desmarcar RadioButton dentro do GroupBox
                        }
                    }



                }

                // Limpar DataGridView (remover todas as linhas)
               // Isso remove todas as linhas de dados




            }
        }

        bool sidebarExpand = true;


        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }
        bool menuExpand = false;
        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if(menuExpand == false)
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

        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if(sidebarExpand)
            {
                sideBar.Width -= 5;
                if (sideBar.Width <= 43)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }else
            {
                sideBar.Width += 5;
                if (sideBar.Width >= 209)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Solicitar_aulas solicitar_Aulas = new Solicitar_aulas();
            solicitar_Aulas.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeAluno homeAluno = new HomeAluno();
            homeAluno.Show();
            
        }
    }
}

