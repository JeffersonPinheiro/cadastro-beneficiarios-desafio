using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory
        : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=FaceBug;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
