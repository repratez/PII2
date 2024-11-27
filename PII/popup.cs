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
    public partial class popup : Form
    {

        private Ouvidoria formOuvidoria;
        private Solicitar_aulas form;
        public popup(Ouvidoria ouvidoriaForm )
        {
            InitializeComponent();
            formOuvidoria = ouvidoriaForm;
        }

        // Construtor para o tipo Solicitar_aulas
        public popup(Solicitar_aulas form2)
        {
            InitializeComponent();
            form = form2;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(formOuvidoria != null) 
            {

                formOuvidoria.LimparCampos();

                this.Hide();
                
            }

            else
            {
                this.Hide();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
