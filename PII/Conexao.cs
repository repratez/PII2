using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PII
{
    internal class Conexao
    {

        SqlConnection conn = new SqlConnection();









        public void Conectar()
        {
            string aux = "SERVER=.\\SQLEXPRESS;Database=GestaoEscolar;UID=sa;PWD=123";
            conn.ConnectionString = aux;
            conn.Open();
        }

        public void Desconectar()
        {
            conn.Close();
        }

        public void Executar(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }


        public DataSet ListarDados(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public int GetCursoId(string nomeCurso)
        {
            // Aqui você deve retornar o ID do curso inserido, talvez fazendo uma consulta
            // para pegar o ID do curso pelo nome.
            return 1; // Exemplo fictício
        }

        public void InserirRegistro(string nome, DateTime dataNascimento, int idCurso, string endereco, string email, string matricula)
        {
            // SQL para inserir os dados na tabela Alunos
            string sql = "INSERT INTO Aluno (nomeAluno, dataNascimento, idCurso, endereco, email, matricula) " +
                         "VALUES (@nome, @dataNascimento, @idCurso, @endereco, @email, @matricula)";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // Adiciona os parâmetros ao comando
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                cmd.Parameters.AddWithValue("@idCurso", idCurso);
                cmd.Parameters.AddWithValue("@endereco", endereco);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@matricula", matricula);

                Conectar();
                try
                {
                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao inserir registro: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }
        }

        public List<KeyValuePair<int, string>> ObterCursos()
        {
            string sql = "SELECT idCurso, nomeCurso FROM Curso";
            List<KeyValuePair<int, string>> cursos = new List<KeyValuePair<int, string>>();

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                Conectar();
                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCurso = reader.GetInt32(0);
                            string nomeCurso = reader.GetString(1);
                            cursos.Add(new KeyValuePair<int, string>(idCurso, nomeCurso));
                        }
                    }

                    if (cursos.Count == 0)
                    {
                        throw new Exception("Nenhum curso encontrado no banco de dados.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao obter cursos: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }
            return cursos;
        }

        public DataTable ObterAlunos()
        {

            string sql = "SELECT A.nomeAluno, A.dataNascimento, A.endereco, A.email, C.nomeCurso " +
                         "FROM Aluno A " +
                         "JOIN Matricula M ON A.idAluno = M.idAluno " +
                         "JOIN Curso C ON M.idCurso = C.idCurso";
            DataTable alunos = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                Conectar();
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(alunos);  // Preenche o DataTable com os dados
                    MessageBox.Show("Dados carregados: " + alunos.Rows.Count.ToString() + " registros.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao obter alunos: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }
            return alunos;
        }



        public void Limpar()
        {

        }

        public void InserirMatricula(int idAluno, int idCurso)
        {
            // SQL para inserir os dados na tabela Matricula
            string sql = "INSERT INTO Matricula (idAluno, idCurso, dataMatricula) " +
                         "VALUES (@idAluno, @idCurso, @dataMatricula)";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // Adiciona os parâmetros ao comando
                cmd.Parameters.AddWithValue("@idAluno", idAluno);
                cmd.Parameters.AddWithValue("@idCurso", idCurso);
                cmd.Parameters.AddWithValue("@dataMatricula", DateTime.Now); // Data da matrícula (agora)

                Conectar();
                try
                {
                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao inserir matrícula: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }
        }
        public int InserirCursoSeNaoExistir(string nomeCurso)
        {
            int idCurso = -1;

            try
            {
                Conectar();

                // Verificar se o curso já existe
                string sqlVerificar = "SELECT idCurso FROM Curso WHERE nomeCurso = @nomeCurso";
                using (SqlCommand cmdVerificar = new SqlCommand(sqlVerificar, conn))
                {
                    cmdVerificar.Parameters.AddWithValue("@nomeCurso", nomeCurso);
                    object result = cmdVerificar.ExecuteScalar();
                    if (result != null)
                    {
                        idCurso = Convert.ToInt32(result);
                        return idCurso; // Retorna o ID se o curso já existe
                    }
                }

                // Se não existir, insere o curso
                string sqlInserir = "INSERT INTO Curso (nomeCurso) VALUES (@nomeCurso); SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmdInserir = new SqlCommand(sqlInserir, conn))
                {
                    cmdInserir.Parameters.AddWithValue("@nomeCurso", nomeCurso);
                    idCurso = Convert.ToInt32(cmdInserir.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir curso: " + ex.Message);
            }
            finally
            {
                Desconectar();
            }

            return idCurso;
        }


        public void InserirDisciplina(string nomeDisciplina, int idCurso)
        {
            try
            {
                Conectar();

                string sql = "INSERT INTO Disciplina (nomeDisciplina, idCurso) VALUES (@nomeDisciplina, @idCurso)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nomeDisciplina", nomeDisciplina);
                    cmd.Parameters.AddWithValue("@idCurso", idCurso);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir disciplina: " + ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }



        public DataTable ListarCursosEDisciplinas()
        {
            string sql = "SELECT C.nomeCurso, D.nomeDisciplina " +
                         "FROM Curso C " +
                         "JOIN Disciplina D ON C.idCurso = D.idCurso";
            DataTable dataTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                Conectar();
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable); // Preenche o DataTable com os dados
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao listar cursos e disciplinas: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }
            return dataTable;
        }


        public void InserirAlunoEMatricula(string nome, DateTime dataNascimento, int idCurso, string endereco, string email, string telefone, string cpf, string rg, DateTime dataMatricula, string senha)
        {
            // SQL para inserir os dados na tabela Aluno, incluindo a senha
            string sqlAluno = "INSERT INTO Aluno (nomeAluno, dataNascimento, idCurso, endereco, email, telefone, cpf, rg, senha) " +
                              "VALUES (@nome, @dataNascimento, @idCurso, @endereco, @email, @telefone, @cpf, @rg, @senha); " +
                              "SELECT SCOPE_IDENTITY();"; // Isso vai pegar o ID do aluno inserido.

            using (SqlCommand cmd = new SqlCommand(sqlAluno, conn))
            {
                // Adiciona os parâmetros ao comando
                cmd.Parameters.AddWithValue("@nome", nome);
                cmd.Parameters.AddWithValue("@dataNascimento", dataNascimento);
                cmd.Parameters.AddWithValue("@idCurso", idCurso);
                cmd.Parameters.AddWithValue("@endereco", endereco);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@telefone", telefone); // Adiciona o telefone
                cmd.Parameters.AddWithValue("@cpf", cpf); // Adiciona o CPF
                cmd.Parameters.AddWithValue("@rg", rg); // Adiciona o RG
                cmd.Parameters.AddWithValue("@senha", senha);  // Adiciona a senha gerada

                Conectar();
                try
                {
                    // Executa o comando e obtém o ID do aluno inserido
                    int idAluno = Convert.ToInt32(cmd.ExecuteScalar());

                    // Agora, insira a matrícula na tabela Matricula
                    string sqlMatricula = "INSERT INTO Matricula (idAluno, idCurso, dataMatricula) " +
                                          "VALUES (@idAluno, @idCurso, @dataMatricula)";

                    using (SqlCommand cmdMatricula = new SqlCommand(sqlMatricula, conn))
                    {
                        cmdMatricula.Parameters.AddWithValue("@idAluno", idAluno);
                        cmdMatricula.Parameters.AddWithValue("@idCurso", idCurso);
                        cmdMatricula.Parameters.AddWithValue("@dataMatricula", dataMatricula);

                        // Executa o comando para inserir a matrícula
                        cmdMatricula.ExecuteNonQuery();
                    }

                    MessageBox.Show("Aluno e matrícula inseridos com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao inserir aluno e matrícula: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }
        }

        public DataTable ObterCursosEDisciplinas()
        {
            DataTable cursosDisciplinas = new DataTable();

            try
            {
                Conectar();

                string sql = @"SELECT 
                           C.nomeCurso AS 'Curso',
                           STRING_AGG(D.nomeDisciplina, ', ') AS 'Disciplinas'
                       FROM Curso C
                       LEFT JOIN Disciplina D ON C.idCurso = D.idCurso
                       GROUP BY C.nomeCurso";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(cursosDisciplinas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao obter cursos e disciplinas: " + ex.Message);
            }
            finally
            {
                Desconectar();
            }

            return cursosDisciplinas;
        }

        public bool VerificarCredenciaisAluno(string email, string senha)
        {
            string sql = "SELECT COUNT(1) FROM Aluno WHERE email = @Email AND senha = @Senha";
            bool credenciaisValidas = false;

            try
            {
                Conectar(); // Abre a conexão usando o método existente na sua classe

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    credenciaisValidas = count > 0; // Credenciais são válidas se o retorno for maior que 0
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar credenciais: " + ex.Message);
            }
            finally
            {
                Desconectar(); // Fecha a conexão usando o método existente na sua classe
            }

            return credenciaisValidas;
        }

        public DataTable ListarAulasReforcoPorCurso(int idCurso)
        {
            string sql = "SELECT AR.dataAula, A.nomeAluno, D.nomeDisciplina " +
                         "FROM AulaReforco AR " +
                         "JOIN Aluno A ON AR.idAluno = A.idAluno " +
                         "JOIN Disciplina D ON AR.idDisciplina = D.idDisciplina " +
                         "WHERE A.idCurso = @idCurso";

            DataTable aulasReforco = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@idCurso", idCurso);

                Conectar();
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(aulasReforco);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao listar aulas de reforço por curso: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }

            return aulasReforco;
        }



        public void InserirAulaReforco(int idAluno, int idDisciplina, string motivo,  DateTime dataFim)
        {
            try
            {
                Conectar();
                string query = "INSERT INTO AulaReforco (idAluno, idDisciplina, motivo, dataFim) " +
                               "VALUES (@idAluno, @idDisciplina, @motivo, @dataFim)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idAluno", idAluno);
                    cmd.Parameters.AddWithValue("@idDisciplina", idDisciplina);
                    
                    cmd.Parameters.AddWithValue("@motivo", motivo);
                    cmd.Parameters.AddWithValue("@dataFim", dataFim); // Adicionando a dataFim

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir dados na tabela AulaReforco: " + ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        public void CarregarDisciplinasNoComboBox(ComboBox comboBox)
        {
            string sql = "SELECT idDisciplina, nomeDisciplina FROM Disciplina";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            try
            {
                Conectar();
                da.Fill(dt);
                comboBox.DataSource = dt;
                comboBox.DisplayMember = "nomeDisciplina"; // Nome da disciplina que será exibido
                comboBox.ValueMember = "idDisciplina";   // ID da disciplina que será usado internamente
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar disciplinas: " + ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        public int ObterIdAlunoPorNome(string nomeAluno)
        {
            int idAluno = -1; // Valor padrão indicando que não foi encontrado
            string sql = "SELECT idAluno FROM Aluno WHERE nomeAluno = @nomeAluno";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@nomeAluno", nomeAluno);

                Conectar();
                try
                {
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        idAluno = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Aluno não encontrado com o nome informado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao obter ID do aluno: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }

            return idAluno;
        }


        public DataTable ListarAulasReforcoPorAluno(int idAluno)
        {
            string sql = "SELECT AR.dataAula, D.nomeDisciplina " +
                         "FROM AulaReforco AR " +
                         "JOIN Disciplina D ON AR.idDisciplina = D.idDisciplina " +
                         "WHERE AR.idAluno = @idAluno";

            DataTable aulasReforco = new DataTable();

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@idAluno", idAluno);

                Conectar();
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(aulasReforco);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao listar aulas de reforço: " + ex.Message);
                }
                finally
                {
                    Desconectar();
                }
            }

            return aulasReforco;
        }

      



    }
}
 
    

 

    

       





 





