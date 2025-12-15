using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Repositories
{
    public class BeneficiariosRepository : IBeneficiariosRepository
    {
        private readonly AppDbContext _context;

        public BeneficiariosRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Beneficiario?> ObterPorIdAsync(int id)
        {
            return await _context.Beneficiarios.FindAsync(id);
        }

        public async Task<List<Beneficiario>> ObterTodosAsync()
        {
            return await _context.Beneficiarios.ToListAsync();
        }

        public async Task<Beneficiario> CriarAsync(Beneficiario beneficiario)
        {
            _context.Beneficiarios.AddAsync(beneficiario);
            return beneficiario;
        }

        public void Atualizar(Beneficiario beneficiario)
        {
            _context.Beneficiarios.Update(beneficiario);
        }

        public void Remover(Beneficiario beneficiario)
        {
            _context.Beneficiarios.Remove(beneficiario);
        }

        public async Task<bool> CpfExisteAsync(string cpf)
        {
            return await _context.Beneficiarios.AnyAsync(b => b.Cpf == cpf);
        }

        public async Task<List<Beneficiario>> ObterPendentesExclusaoAsync()
        {
            return await _context.Beneficiarios
                .Where(b => b.PendenteExclusao)
                .OrderBy(b => b.PrioridadeExclusao)
                .ThenBy(b => b.DataSolicitacaoExclusao)
                .ToListAsync();
        }
    }
}
