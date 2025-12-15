using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Repositories
{
    public class PlanosRepository : IPlanosRepository
    {
        private readonly AppDbContext _context;

        public PlanosRepository(AppDbContext context)
        {
            _context = context;
        }

        public Planos CriarPlano(Planos plano)
        {
            _context.Planos.Add(plano);
            return plano;
        }

        public async Task<Planos?> ObterPorIdAsync(int id)
        {
            return await _context.Planos
                .Include(p => p.Beneficiarios)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Planos>> ObterTodosAsync()
        {
            return await _context.Planos
                .Include(p => p.Beneficiarios)
                .ToListAsync();
        }

        public async Task<bool> PlanoExisteAsync(int id)
        {
            return await _context.Planos.AnyAsync(p => p.Id == id);
        }

        public void Atualizar(Planos plano)
        {
            _context.Planos.Update(plano);
        }

        public void Remover(Planos plano)
        {
            _context.Planos.Remove(plano);
        }
    }
}

