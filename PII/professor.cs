using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PII
{
    internal class professor
    {

        private Conexao objetoConexao = new Conexao();

        private int idProfessor;
        private string nomeProfessor;
        private string email;
        private string senha;
        private string telefone;
        private int idDisciplina;
        private int idCurso;
        private string Endereco;
        private DateTime date;
        private string cpf;
        private string rg;


        public int IdProfessor { get => idProfessor; set => idProfessor = value; }
        public string NomeProfessor { get => nomeProfessor; set => nomeProfessor = value; }
        public string Email { get => email; set => email = value; }
       
        public string Telefone { get => telefone; set => telefone = value; }
        public int IdDisciplina { get => idDisciplina; set => idDisciplina = value; }
        public int IdCurso { get => idCurso; set => idCurso = value; }
        public string Endereco1 { get => Endereco; set => Endereco = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Rg { get => rg; set => rg = value; }
        public string Senha { get => senha; set => senha = value; }

        public void Incluir()
        {
            string sql = "";
            sql += "INSERT INTO Curso (nomeProfessor, email, senha, telefone, idDisciplina, idCurso, endereco, dataNascimento, cpf, rg) VALUES (";
            sql += "'" + nomeProfessor + "', "; // Nome do professor
            sql += "'" + email + "', ";        // Email
            sql += "'" + Senha + "', ";     // Senha
            sql += "'" + Telefone + "', ";     // Telefone
            sql += IdDisciplina + ", ";        // Id da disciplina (sem aspas, pois é numérico)
            sql += IdCurso + ", ";             // Id do curso (sem aspas, pois é numérico)
            sql += "'" + Endereco + "', ";     // Endereço
            sql += "'" + date.ToString("yyyy-MM-dd") + "', "; // Data de nascimento (formato padrão de banco de dados)
            sql += "'" + cpf + "', ";          // CPF
            sql += "'" + rg + "');";           // RG

            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }


    }
}
