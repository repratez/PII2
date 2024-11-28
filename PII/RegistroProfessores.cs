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
    }
}
