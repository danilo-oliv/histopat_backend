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
            "Server=DESKTOP-OQ1NRH7;Database=histopat;Integrated Security=True;TrustServerCertificate=True;");
            return new HistopatDbContext(optionsBuilder.Options);
        }
    }
}
