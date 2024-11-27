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





    }
}
