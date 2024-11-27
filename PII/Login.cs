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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
          

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox3.Text;
            string senha = textBox2.Text;

            Conexao conexao = new Conexao();

            // Verifica se é um aluno
            if (conexao.VerificarCredenciaisAluno(email, senha))
            {
                this.Hide();
                HomeAluno aux = new HomeAluno();
                aux.ShowDialog();
            }
            else if (email == "admin@gmail.com" && senha == "admin") // Credenciais de administrador fixas
            {
                this.Hide();
                HOME aux1 = new HOME();
                aux1.ShowDialog();
            }
            else
            {
                MessageBox.Show("E-mail ou senha inválidos. Tente novamente.");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0'; // Remove a máscara (exibe a senha)
            }
            else
            {
                textBox2.PasswordChar = '●'; // Adiciona a máscara (oculta a senha)
            }
        }
    }
}
