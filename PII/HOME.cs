using Microsoft.Win32;
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
    public partial class HOME : Form
    {
        public HOME()
        {
            InitializeComponent();
        }

        bool menuExpand = false;

        protected void HOME_Load(object sender, EventArgs e)
        {

        }

        protected void menuTransition_Tick(object sender, EventArgs e)
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

            else { 
                menuContainer.Height -= 10;
                if(menuContainer.Height <= 44)
                {
                    menuTransition.Stop();
                    menuExpand = false;
                }
            
            
            
            }

        }

        protected void menu_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        bool sidebarExpand= true;
        protected void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sideBar.Width -= 5;
                if(sideBar.Width <= 43)
                {
                    sidebarExpand = false;
                    sidebarTransition.Stop();
                }
            }else
            {
                sideBar.Width += 5;
                if(sideBar.Width >= 209)
                {
                    sidebarExpand = true;
                    sidebarTransition.Stop();
                }
            }
        }

        protected void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registro reg = new registro();
            reg.ShowDialog();



        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegistroProfessores ope = new RegistroProfessores();
            ope.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RegistrarCurso opne2 = new RegistrarCurso();
            opne2.ShowDialog();
          
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
