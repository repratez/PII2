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
    public partial class Excluir : Form
    {
        public Excluir()
        {
            InitializeComponent();
        }

        private Curso objetoCurso = new Curso();

        private void btnExcluirDisciplina_Click(object sender, EventArgs e)
        {

        }

        private void cboDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboCurso2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void cboCurso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCurso.SelectedValue != null && int.TryParse(cboCurso.SelectedValue.ToString(), out int idCurso))
            {
                objetoCurso.Codigo_Curso = idCurso; // Atualiza a propriedade com o ID do curso selecionado
            }
            else
            {
                // Opcional: Mensagem de erro, mas somente se a seleção for inválida
                // Não é necessário mostrar isso no início, só quando o usuário interagir
                MessageBox.Show("Seleção inválida ou valor incorreto.");
            }
        }

        private void Excluir_Load(object sender, EventArgs e)
        {
            cboCurso.DisplayMember = "nomeCurso"; // Exibe o nome do curso
            cboCurso.ValueMember = "idCurso";     // Usa o ID do curso como valor
            cboCurso.DataSource = objetoCurso.PesquisaDados().Tables[0];
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Verificar se algum curso foi selecionado
            if (cboCurso.SelectedValue != null && int.TryParse(cboCurso.SelectedValue.ToString(), out int idCurso))
            {
                // Atualiza o ID do curso para o objetoCurso
                objetoCurso.Codigo_Curso = idCurso;  // Troque idCurso por Codigo_Curso

                // Chama o método de exclusão
                objetoCurso.Excluir();
                MessageBox.Show("Curso excluído com sucesso!");

                // Atualiza o ComboBox de cursos com a lista mais recente após a exclusão
                cboCurso.DataSource = objetoCurso.PesquisaDados().Tables[0];

                // Opcional: Se você quiser, pode limpar a seleção do ComboBox
                cboCurso.SelectedValue =null;
            }
            else
            {
                // Caso não haja curso selecionado
                MessageBox.Show("Selecione um curso para excluir.");
            }
        }
    }
}
