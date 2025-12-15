using Desafio_Tecnico_Cadastro_de_Beneficiarios.Infrastructure.Repositories;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        Task RollbackAsync();
        IBeneficiariosRepository Beneficiarios { get; }
        IPlanosRepository Planos { get; }
        ILogExclusaoBeneficiarioRepository LogsExclusao { get; }
    }
}
