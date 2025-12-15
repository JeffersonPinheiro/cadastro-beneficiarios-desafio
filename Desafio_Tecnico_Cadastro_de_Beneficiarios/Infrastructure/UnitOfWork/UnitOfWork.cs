using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IDbContextTransaction? _transaction;

        public IBeneficiariosRepository Beneficiarios { get; }
        public IPlanosRepository Planos { get; }
        public ILogExclusaoBeneficiarioRepository LogsExclusao { get; }

        public UnitOfWork(AppDbContext context, IBeneficiariosRepository beneficiarioRepository, 
            IPlanosRepository planoRepository, ILogExclusaoBeneficiarioRepository logsExclusao)
        {
            _context = context;
            Beneficiarios = beneficiarioRepository;
            Planos = planoRepository;
            LogsExclusao = logsExclusao;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
    }
}
