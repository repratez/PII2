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

        public void Dispose()
        {
            _driver?.Dispose();
        }

        //public async Task RegistrarFeedbackAsync(string tipo, string data, string matricula, string descricao)
        //{
        //    await using var session = _driver.AsyncSession();
        //    await session.ExecuteWriteAsync(
        //        async tx =>
        //        {
        //            var query = @"
        //        CREATE (f:Feedback {
        //            Tipo: $tipo,
        //            Data: $data,
        //            Matricula: $matricula,
        //            Descricao: $descricao
        //        })
        //        RETURN f";
        //            await tx.RunAsync(query, new
        //            {
        //                tipo,
        //                data,
        //                matricula,
        //                descricao
        //            });
        //        });
        //}

    }
}