using histopat_back.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace histopat_back.Infra.Context
{
    public class HistopatDbContextFactory : IDesignTimeDbContextFactory<HistopatDbContext>
    {
        public HistopatDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HistopatDbContext>();
            optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=histopat;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;Encrypt=False;");
            return new HistopatDbContext(optionsBuilder.Options);
        }
    }
}
