using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Repositories
{
    public class LogExclusaoBeneficiarioRepository : ILogExclusaoBeneficiarioRepository
    {
        private readonly AppDbContext _context;

        public LogExclusaoBeneficiarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(LogExclusaoBeneficiario log)
        {
            await _context.LogsExclusaoBeneficiarios.AddAsync(log);
        }
    }
}
