using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PII
{
    internal class Aula
    {
        private Conexao objetoConexao = new Conexao();

        private int idAulaReforco;
        private int idDisciplina;
        private string motivo;
        private DateTime dataFim;

        // Propriedade
        public int IdAulaReforco { get => idAulaReforco; set => idAulaReforco = value; }
        public int IdDisciplina { get => idDisciplina; set => idDisciplina = value; }
        public string Motivo { get => motivo; set => motivo = value; }
        public DateTime DataFim { get => dataFim; set => dataFim = value; }

        // Método Alterar
        public void Alterar()
        {
            // Comando SQL para atualizar a tabela Aula
            string sql = $"UPDATE Aula SET idDisciplina = {IdDisciplina}, motivo = '{Motivo}', dataFim = '{DataFim:yyyy-MM-dd HH:mm:ss}' WHERE idAulaReforco = {IdAulaReforco}";

            // Conecta ao banco, executa o comando e desconecta
            objetoConexao.Conectar();
            objetoConexao.Executar(sql);
            objetoConexao.Desconectar();
        }
    }

}
 



