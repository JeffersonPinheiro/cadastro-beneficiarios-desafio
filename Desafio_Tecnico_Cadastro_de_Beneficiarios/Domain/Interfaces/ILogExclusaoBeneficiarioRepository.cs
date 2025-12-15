using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces
{
    public interface ILogExclusaoBeneficiarioRepository
    {
        Task AdicionarAsync(LogExclusaoBeneficiario log);
    }
}
