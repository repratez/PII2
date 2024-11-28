using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PII
{
    internal class Curso
    {
        private Conexao objetoConexao = new Conexao();

        private int codigo_Curso;
        private string nome_Curso;

        public int Codigo_Curso { get => codigo_Curso; set => codigo_Curso = value; }
        public string Nome_Curso { get => nome_Curso; set => nome_Curso = value; }

        public void Incluir()
        {
            string sql = "";
            sql += "Insert into Curso(nomeCurso) values ('" + Nome_Curso + "' )";
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }

        public void Alterar()
        {
            string sql = "";
            sql = $"Update Curso set nomeCurso = '{Nome_Curso}' where idCurso = " + Codigo_Curso.ToString();
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }

        public void Excluir()
        {
            // Atualizar os registros na tabela AulaReforco para que não referenciem mais a disciplina ou curso
            string sqlAtualizar = "UPDATE AulaReforco SET idDisciplina = NULL WHERE idDisciplina = " + Codigo_Curso.ToString();
            objetoConexao.Conectar();
            objetoConexao.Executar(sqlAtualizar);

            // Agora, podemos excluir a disciplina ou curso
            string sql = "DELETE FROM Disciplina WHERE idDisciplina = " + Codigo_Curso.ToString();
            objetoConexao.Executar(sql);

            objetoConexao.Desconectar();
        }


        public DataSet PesquisaDados()
        {
            string sql = "Select * from Curso";
            objetoConexao.Conectar();
            DataSet ds = objetoConexao.ListarDados(sql);
            objetoConexao.Desconectar();
            return ds;
        }

        public List<KeyValuePair<int, string>> ObterCursosParaComboBox()
        {
            // Consulta para buscar todos os cursos
            string sql = "SELECT idCurso, nomeCurso FROM Curso";

            // Conectar e executar a consulta
            objetoConexao.Conectar();
            DataTable dt = objetoConexao.ListarDados(sql).Tables[0]; // Obtém o DataTable com os cursos
            objetoConexao.Desconectar();

            // Verificar se a consulta retornou resultados
            if (dt.Rows.Count == 0)
            {
               
            }

            // Criar a lista de cursos (idCurso, nomeCurso)
            List<KeyValuePair<int, string>> listaCursos = new List<KeyValuePair<int, string>>();

            // Preencher a lista com os cursos
            foreach (DataRow row in dt.Rows)
            {
                listaCursos.Add(new KeyValuePair<int, string>((int)row["idCurso"], (string)row["nomeCurso"]));
            }

            return listaCursos;
        }







    }
}
