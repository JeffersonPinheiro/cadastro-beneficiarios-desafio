using Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Entities;

namespace Desafio_Tecnico_Cadastro_de_Beneficiarios.Domain.Interfaces
{
    public interface IBeneficiariosRepository
    {
        Task<Beneficiario?> ObterPorIdAsync(int id);
        Task<List<Beneficiario>> ObterTodosAsync();
        Task<Beneficiario> CriarAsync(Beneficiario beneficiario);
        void Atualizar(Beneficiario beneficiario);
        void Remover(Beneficiario beneficiario);
        Task<bool> CpfExisteAsync(string cpf);
        Task<List<Beneficiario>> ObterPendentesExclusaoAsync();
    }
}
