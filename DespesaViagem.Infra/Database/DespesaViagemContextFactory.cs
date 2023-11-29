using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Data.Common;

namespace DespesaViagem.Infra.Database
{
    public class DespesaViagemContextFactory : IDesignTimeDbContextFactory<DespesaViagemContext>
    {

        public DespesaViagemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DespesaViagemContext>();

            //String de conexão para usar o banco de dados local            

            //optionsBuilder.UseSqlServer("Data Source=GUSTAVO;Initial Catalog=DespesaViagem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");            
            optionsBuilder.UseSqlite("Data Source=..\\..\\DespesaViagem.Infra\\Database\\SQLite\\Expensify.sqlite3");
            
            //String de conexão para utilizar o docker (Inviável por questões de memória)
            //optionsBuilder.UseSqlServer("Data Source = localhost,1433; Database = DespesaViagem; Persist Security Info = True; Encrypt = False; User ID = sa; Password = teste@1234");

            optionsBuilder.EnableSensitiveDataLogging();

            return new DespesaViagemContext(optionsBuilder.Options);
        }
    }
}
