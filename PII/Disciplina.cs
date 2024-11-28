using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PII
{
    internal class Disciplina
    {

        private Conexao objetoConexao = new Conexao();
        private int idDisciplina;
        private string nomeDisciplina;

        public int IdDisciplina { get => idDisciplina; set => idDisciplina = value; }
        public string NomeDisciplina { get => nomeDisciplina; set => nomeDisciplina = value; }

        public void Alterar()
        {
            string sql = $"UPDATE Disciplina SET nomeDisciplina = '{NomeDisciplina}' WHERE idDisciplina = {IdDisciplina}";
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }

        public DataSet ListarDisciplinas()
        {
            string sql = "SELECT * FROM Disciplina";
            objetoConexao.Conectar();
            DataSet ds = objetoConexao.ListarDados(sql);
            objetoConexao.Desconectar();
            return ds;
        }

        public DataSet PesquisaDados()
        {
            string sql = "Select * from Disciplina";
            objetoConexao.Conectar();
            DataSet ds = objetoConexao.ListarDados(sql);
            objetoConexao.Desconectar();
            return ds;
        }

        /*public void Excluir()
        {
            string sql = "Delete from Categoria where Codigo_Categoria = " + Codigo_Categoria.ToString();
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }*/

        public DataSet ListarDisciplinasPorCurso(int idCurso)
        {
            string sql = $"SELECT * FROM Disciplina WHERE idCurso = {idCurso}";
            objetoConexao.Conectar();
            DataSet ds = objetoConexao.ListarDados(sql);
            objetoConexao.Desconectar();
            return ds;
        }
    }
}
