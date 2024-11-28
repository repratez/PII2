using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PII
{
    internal class BoletimClasse
    {

        private Conexao objetoConexao = new Conexao();

        private int idDesempenho;
        private int idAluno;
        private int idDisciplina;
        private decimal nota;
        private int ano;

        // Propriedades (Getters e Setters)
        public int IdDesempenho { get => idDesempenho; set => idDesempenho = value; }
        public int IdAluno { get => idAluno; set => idAluno = value; }
        public int IdDisciplina { get => idDisciplina; set => idDisciplina = value; }
        public decimal Nota { get => nota; set => nota = value; }
       
        public int Ano { get => ano; set => ano = value; }

        // Método para incluir um novo desempenho
        public void Incluir()
        {
            string sql = "INSERT INTO Desempenho(idAluno, idDisciplina, nota, ano) " +
                         "VALUES (" + IdAluno + ", " + IdDisciplina + ", " + Nota + ", " + ", " + Ano + ")";
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }

        // Método para alterar os dados de desempenho
        public void Alterar()
        {
            string sql = $"UPDATE Desempenho SET idAluno = {IdAluno}, idDisciplina = {IdDisciplina}, " +
                         $"nota = {Nota}, ano = {Ano} WHERE idDesempenho = {IdDesempenho}";
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }

        // Método para excluir um desempenho
        public void Excluir()
        {
            string sql = "DELETE FROM Desempenho WHERE idDesempenho = " + IdDesempenho;
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }

        // Método para pesquisar os dados de desempenho
        public DataSet PesquisaDados()
        {
            string sql = "SELECT * FROM Desempenho";
            objetoConexao.Conectar();
            DataSet ds = objetoConexao.ListarDados(sql);
            objetoConexao.Desconectar();
            return ds;
        }

        // Método para pesquisar desempenho por aluno e disciplina
        public DataSet PesquisaPorAlunoEDisciplina(int alunoId, int disciplinaId)
        {
            string sql = $"SELECT * FROM Desempenho WHERE idAluno = {alunoId} AND idDisciplina = {disciplinaId}";
            objetoConexao.Conectar();
            DataSet ds = objetoConexao.ListarDados(sql);
            objetoConexao.Desconectar();
            return ds;
        }
    }



}

