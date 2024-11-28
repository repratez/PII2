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
    public partial class Editar : Form
    {
        public Editar()
        {
            InitializeComponent();
        }

        private Curso objetoCurso = new Curso();
        private Disciplina objetoDisciplina = new Disciplina();


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

        private void Editar_Load(object sender, EventArgs e)
        {
            cboCurso.DisplayMember = "nomeCurso"; // Exibe o nome do curso
            cboCurso.ValueMember = "idCurso";     // Usa o ID do curso como valor
            cboCurso.DataSource = objetoCurso.PesquisaDados().Tables[0]; // Preenche com os dados do banco de cursos

            // Carregar os cursos também no cboCurso2
            cboCurso2.DisplayMember = "nomeCurso";
            cboCurso2.ValueMember = "idCurso";
            cboCurso2.DataSource = objetoCurso.PesquisaDados().Tables[0];

            // Limpar ComboBox de disciplinas inicialmente
            cboDisciplina.DataSource = null;
        }

        public event AtualizarGridHandler OnCursoAlterado;
        public delegate void AtualizarGridHandler();

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // Atualiza o nome da disciplina selecionada
            objetoCurso.Nome_Curso = cboCurso.Text;
            objetoCurso.Alterar(); // Alteração no banco
            MessageBox.Show("Curso alterado com sucesso!");

            // Atualiza o ComboBox de cursos
            cboCurso.DataSource = objetoCurso.PesquisaDados().Tables[0];
            cboCurso.SelectedValue = objetoCurso.Codigo_Curso; // Retorna à seleção anterior, se necessário.

            // Dispara o evento para notificar a outra tela
            OnCursoAlterado?.Invoke();

            // Limpar ComboBox de Disciplinas após alterar o curso
            cboDisciplina.DataSource = null;
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            // Atualiza o nome da disciplina selecionada
            objetoDisciplina.NomeDisciplina = cboDisciplina.Text;

            // Chama o método para alterar a disciplina no banco
            objetoDisciplina.Alterar(); // Método que altera a disciplina no banco
            MessageBox.Show("Disciplina alterada com sucesso!");

            // Atualiza o ComboBox de disciplinas com os dados mais recentes, dependendo do curso selecionado
            cboDisciplina.DataSource = objetoDisciplina.ListarDisciplinasPorCurso(objetoCurso.Codigo_Curso).Tables[0];
            cboDisciplina.SelectedValue = objetoDisciplina.IdDisciplina; // Retorna à seleção anterior da disciplina, se necessário.telas


        }

        private void cboDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica se o valor selecionado no cboDisciplina é válido
            if (cboDisciplina.SelectedValue != null && int.TryParse(cboDisciplina.SelectedValue.ToString(), out int idDisciplina))
            {
                // Atualiza o ID da disciplina selecionada
                objetoDisciplina.IdDisciplina = idDisciplina;
            }
            else
            {
                // Caso a seleção da disciplina seja inválida
                MessageBox.Show("Seleção inválida ou valor incorreto.");
            }

        }

        private void cboCurso2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verificar se o valor selecionado no cboCurso2 é válido
            if (cboCurso2.SelectedValue != null && int.TryParse(cboCurso2.SelectedValue.ToString(), out int idCurso))
            {
                // Carregar as disciplinas com base no curso selecionado
                cboDisciplina.DisplayMember = "nomeDisciplina";  // Nome da disciplina a ser exibido
                cboDisciplina.ValueMember = "idDisciplina";      // ID da disciplina como valor

                // Obter as disciplinas para o curso selecionado
                DataSet dsDisciplinas = objetoDisciplina.ListarDisciplinasPorCurso(idCurso);

                // Verifique se o DataSet tem dados
                if (dsDisciplinas != null && dsDisciplinas.Tables.Count > 0 && dsDisciplinas.Tables[0].Rows.Count > 0)
                {
                    // Atualize o ComboBox com as disciplinas
                    cboDisciplina.DataSource = dsDisciplinas.Tables[0];
                }
                else
                {
                    // Caso não haja disciplinas para o curso, limpar o ComboBox
                    cboDisciplina.DataSource = null;
                    MessageBox.Show("Não há disciplinas para o curso selecionado.");
                }
            }
            else
            {
                // Caso o valor selecionado no cboCurso2 seja inválido
                cboDisciplina.DataSource = null;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistrarCurso reg = new RegistrarCurso();
            reg.ShowDialog();
        }
    }
}
