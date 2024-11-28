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
    public partial class RegistroProfessores : Form
    {
        public RegistroProfessores()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registro reg = new registro();
            reg.ShowDialog();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
             
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void menu_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void menu_Click_1(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void RegistroProfessores_Load(object sender, EventArgs e)
        {
            Curso curso = new Curso();
            Disciplina disciplina = new Disciplina();
            comboBoxCurso.DisplayMember = "nomeCurso";
            comboBoxCurso.ValueMember = "idCurso";
            
            comboBoxCurso.DataSource = curso.PesquisaDados().Tables[0];

            cboDisciplina.DisplayMember = "nomeDisciplina";
            cboDisciplina.ValueMember = "idDisciplina";
            cboDisciplina.DataSource = disciplina.PesquisaDados().Tables[0];

           
        }

        bool menuExpand = false;

        bool sidebarExpand = true;

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            registro reg = new registro();
            reg.ShowDialog();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            RegistroProfessores reg = new RegistroProfessores();
            reg.ShowDialog();

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            RegistrarCurso reg = new RegistrarCurso();
            reg.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            HOME reg= new HOME();
            reg.ShowDialog();
        }
        private void registrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Criação do objeto Professor
                Professor professor = new Professor
                {
                    NomeProfessor = txtNome.Text,
                    Endereco = txtEndereco.Text,
                    Email = txtEmail.Text,
                    Telefone = txtTelefone.Text,
                    DataNascimento = DateTime.Parse(txtData.Text),
                    IdCurso = int.Parse(comboBoxCurso.SelectedValue.ToString()),
                    IdDisciplina = int.Parse(cboDisciplina.SelectedValue.ToString()),
                    Cpf = txtCpf.Text,
                    Rg = txtRg.Text,
                    Senha = "SenhaPadrão123", // Substitua com lógica para senha, se necessário
                    Formacao = textBox3.Text
                };

                // Inserir no banco de dados
                professor.Incluir();
                MessageBox.Show("Professor incluído com sucesso!");

                // Limpar os campos do formulário
                txtNome.Clear();
                txtEndereco.Clear();
                txtEmail.Clear();
                txtTelefone.Clear();
                txtData.Clear();
                comboBoxCurso.SelectedIndex = -1;
                cboDisciplina.SelectedIndex = -1;
                txtCpf.Clear();
                txtRg.Clear();
                textBox3.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao incluir professor: {ex.Message}");
            }


        }


       

    }
}
