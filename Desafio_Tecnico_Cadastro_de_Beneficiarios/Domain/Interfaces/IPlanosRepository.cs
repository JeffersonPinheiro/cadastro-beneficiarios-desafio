using System.Numerics;
using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces
{
    public interface IPlanosRepository
    {
        Task<Planos?> ObterPorIdAsync(int id);
        Task<IEnumerable<Planos>> ObterTodosAsync();
        Task<bool> PlanoExisteAsync(int id);
        Planos CriarPlano(Planos plano);
        void Atualizar(Planos plano);
        void Remover(Planos plano);
    }
}
