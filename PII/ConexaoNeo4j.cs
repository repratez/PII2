using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neo4j.Driver;

namespace PII
{
    public class ConexaoNeo4j : IDisposable
    {
        private readonly IDriver _driver;

        public ConexaoNeo4j(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public async Task RegistrarProfessorAsync(string nome, string endereco, string email, string formacao, string dataNascimento, string cpf, string registroGeral)
        {
            await using var session = _driver.AsyncSession();
            await session.ExecuteWriteAsync(
                async tx =>
                {
                    var query = @"
                CREATE (p:Professor {
                    Nome: $nome,
                    Endereco: $endereco,
                    Email: $email,
                    Formacao: $formacao,
                    DataNascimento: $dataNascimento,
                    CPF: $cpf,
                    RegistroGeral: $registroGeral
                })
                RETURN p";
                    await tx.RunAsync(query, new
                    {
                        nome,
                        endereco,
                        email,
                        formacao,
                        dataNascimento,
                        cpf,
                        registroGeral,
                    });
                });
        }


        public async Task PrintGreetingAsync(string message)
        {
            await using var session = _driver.AsyncSession();
            var greeting = await session.ExecuteWriteAsync(
                async tx =>
                {
                    var result = await tx.RunAsync(
                        "CREATE (a:Greeting) " +
                        "SET a.message = $message " +
                        "RETURN a.message + ', from node ' + id(a)",
                        new { message });

                    var record = await result.SingleAsync();
                    return record[0].As<string>();
                });

            Console.WriteLine(greeting);
        }

        public async Task RegistrarFeedbackAsync(string tipo, string data, string matricula, string descricao)
        {
            await using var session = _driver.AsyncSession();
            await session.ExecuteWriteAsync(
                async tx =>
                {
                    var query = @"
                CREATE (f:Feedback {
                    Tipo: $tipo,
                    Data: $data,
                    Matricula: $matricula,
                    Descricao: $descricao
                })
                RETURN f";
                    await tx.RunAsync(query, new
                    {
                        tipo,
                        data,
                        matricula,
                        descricao
                    });
                });
        }

        public async Task AtualizarFeedbackAsync(string tipoFeedback, string data, string matricula, string descricao)
        {
            await using var session = _driver.AsyncSession();
            await session.ExecuteWriteAsync(async tx =>
            {
                var query = @"
            MATCH (f:Feedback {Matricula: $matricula})
            SET f.Tipo = $tipoFeedback,
                f.Data = $data,
                f.Descricao = $descricao
            RETURN f";
                await tx.RunAsync(query, new
                {
                    tipoFeedback,
                    data,
                    matricula,
                    descricao
                });
            });
        }


        public async Task AtualizarProfessorAsync(string matricula, string nome, string endereco, string email, string curso, string dataNascimento, string cpf, string registroGeral)
        {
            await using var session = _driver.AsyncSession();
            await session.ExecuteWriteAsync(async tx =>
            {
                var query = @"
            MATCH (p:Professor {CPF: $cpf})
            SET p.Nome = $nome,
                p.Endereco = $endereco,
                p.Email = $email,
                p.Formacao = $curso,
                p.DataNascimento = $dataNascimento,
                p.CPF = $cpf,
                p.RegistroGeral = $registroGeral
            RETURN p";
                await tx.RunAsync(query, new
                {
                    matricula,
                    nome,
                    endereco,
                    email,
                    curso,
                    dataNascimento,
                    cpf,
                    registroGeral
                });
            });
        }
        public async Task<Dictionary<string, string>> BuscarProfessorPorCpfAsync(string cpf)
        {
            await using var session = _driver.AsyncSession();
            return await session.ExecuteReadAsync(async tx =>
            {
                var query = @"
            MATCH (p:Professor {CPF: $cpf})
            RETURN p.Nome AS Nome, 
                   p.Endereco AS Endereco, 
                   p.Email AS Email, 
                   p.Formacao AS Formacao, 
                   p.DataNascimento AS DataNascimento, 
                   p.CPF AS CPF, 
                   p.RegistroGeral AS RegistroGeral";
                var result = await tx.RunAsync(query, new { cpf });

                if (await result.FetchAsync())
                {
                    var record = result.Current;
                    return new Dictionary<string, string>
                    {
                        ["Nome"] = record["Nome"].As<string>(),
                        ["Endereco"] = record["Endereco"].As<string>(),
                        ["Email"] = record["Email"].As<string>(),
                        ["Formacao"] = record["Formacao"].As<string>(),
                        ["DataNascimento"] = record["DataNascimento"].As<string>(),
                        ["CPF"] = record["CPF"].As<string>(),
                        ["RegistroGeral"] = record["RegistroGeral"].As<string>()
                    };
                }

                return null; // Retorna null se o professor não for encontrado
            });
        }


        public async Task<List<Dictionary<string, string>>> ListarProfessoresAsync()
        {
            await using var session = _driver.AsyncSession();
            return await session.ExecuteReadAsync(async tx =>
            {
                var query = @"
            MATCH (p:Professor)
            RETURN p.Nome AS Nome, 
                   p.Endereco AS Endereco, 
                   p.Email AS Email, 
                   p.Formacao AS Formacao, 
                   p.DataNascimento AS DataNascimento, 
                   p.CPF AS CPF, 
                   p.RegistroGeral AS RegistroGeral";
                var result = await tx.RunAsync(query);

                var professores = new List<Dictionary<string, string>>();

                while (await result.FetchAsync())
                {
                    var record = result.Current;
                    professores.Add(new Dictionary<string, string>
                    {
                        ["Nome"] = record["Nome"].As<string>(),
                        ["Endereco"] = record["Endereco"].As<string>(),
                        ["Email"] = record["Email"].As<string>(),
                        ["Formacao"] = record["Formacao"].As<string>(),
                        ["DataNascimento"] = record["DataNascimento"].As<string>(),
                        ["CPF"] = record["CPF"].As<string>(),
                        ["RegistroGeral"] = record["RegistroGeral"].As<string>()
                    });
                }

                return professores;
            });
        }

        public async Task ExcluirProfessorAsync(string cpf)
        {
            await using var session = _driver.AsyncSession();
            await session.ExecuteWriteAsync(
                async tx =>
                {
                    var query = @"
                MATCH (p:Professor {CPF: $cpf})
                DELETE p";
                    await tx.RunAsync(query, new { cpf });
                });
        }
        public void Dispose()
        {
            _driver?.Dispose();
        }
    }
}