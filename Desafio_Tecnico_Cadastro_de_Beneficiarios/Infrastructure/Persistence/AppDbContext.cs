using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Planos> Planos { get; set; }
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<LogExclusaoBeneficiario> LogsExclusaoBeneficiarios { get; set; }


    }
}
