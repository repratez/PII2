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
    public partial class BoletimProfessor : Form
    {
        public BoletimProfessor()
        {
            InitializeComponent();
        }

        private Aluno aluno = new Aluno();  // Instanciando a classe Aluno


        private void BoletimProfessor_Load(object sender, EventArgs e)
        {
            comboBoxAluno.DisplayMember = "nomeAluno";
            comboBoxAluno.ValueMember = "idAluno";
            comboBoxAluno.DataSource = aluno.PesquisaDados().Tables[0];

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            aluno.Codigo_Aluno = int.Parse(comboBoxAluno.SelectedValue.ToString());

        }



        private void button7_Click(object sender, EventArgs e)
        {

        }


        private void BoletimProfessor_Load_1(object sender, EventArgs e)
        {

        }

        private void sideBar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
